/*!	@file
	@brief PluggableVR: VR操作元 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PluggableVR
{
	//! VR操作元 
	internal class VRPlayer : PlugCommon
	{
		private Transform _rig;
		private Transform _cam;
		private VRAvatar _targetView;
		private AvatarControl _targetCtrl;
		private Input _input;
		private Loc _offset;
		private bool _sticking = false;
		private bool _elevating = false;
		private RelativeBool _push_pstk = new RelativeBool();

		internal VRPlayer(Loc loc, VRAvatar target)
		{
			_rig = CreateRootObject("VRPlayer", loc).transform;
			_cam = CreateChildObject("VRCamera", _rig, Loc.Identity, false).transform;
			GameObject.DontDestroyOnLoad(_rig.gameObject);

			_cam.gameObject.AddComponent<Camera>();
			_cam.gameObject.AddComponent<AudioListener>();
			_rig.gameObject.AddComponent<OVRManager>();

			_targetView = target;
			_targetCtrl = _targetView.CreateControl();
			_input = Oculus.Input.Setup();
			ResetRig();

			// 初期状態では頭を非表示とする 
			// 俯瞰操作の間だけ表示 
			_targetView.Head.gameObject.SetActive(false);

			// xは常に非表示 
			// y,zは _elevating で切り替える 
			_targetView.AxisY.SetActive(false);
		}

		private void _showElevating()
		{
			_targetView.AxisY.SetActive(_elevating);
			_targetView.AxisZ.SetActive(!_elevating);
		}

		internal void Update(){

			// スティックの指載せで操作の主観/俯瞰を切り替える 
			var stk1 = _input.HandPrimary.IsStickTouching();
			var stk2 = _input.HandSecondary.IsStickTouching();
			if (!_sticking)
			{
				if (stk1 || stk2)
				{
					_sticking = true;
					_targetView.Head.gameObject.SetActive(true);
					_showElevating();
				}
			}
			else
			{
				if (!(stk1 || stk2))
				{
					_sticking = false;
					ResetRig();
					_targetView.Head.gameObject.SetActive(false);
				}
			}

			// 上下/前後 移動切り替え 
			_push_pstk.Update(_input.HandPrimary.IsStickPushing());
			if (_sticking)
			{
				if (_push_pstk.Delta > 0)
				{
					_elevating = !_elevating;
					_showElevating();
				}
			}
			else _elevating = false;

			// スティック回転 
			if (stk2)
			{
				var tilt = _input.HandSecondary.GetStickTilting();
				_targetCtrl.Origin.Rot *= PluggableVR.RotUt.RotY(90.0f * Mathf.Deg2Rad * tilt.x * Time.deltaTime);
			}

			// スティック移動 
			if (stk1)
			{
				var tilt = _input.HandPrimary.GetStickTilting();
				if (_elevating)
				{
					// xy平面上のz軸2D回転 
					var dir = PluggableVR.RotUt.PlaneXY(_cam.rotation);
					// スティック方向を回転 ついでに移動スピードも掛ける 
					var mzx = dir * new Vector2(tilt.x, tilt.y) * Time.deltaTime;
					_targetCtrl.Origin.Pos += new Vector3(mzx.x, mzx.y, 0);
				}
				else
				{
					// zx平面上のy軸2D回転 
					var dir = PluggableVR.RotUt.PlaneZX(_cam.rotation);
					// スティック方向を回転 ついでに移動スピードも掛ける 
					var mzx = dir * new Vector2(tilt.y, tilt.x) * Time.deltaTime;
					_targetCtrl.Origin.Pos += new Vector3(mzx.y, 0, mzx.x);
				}
			}

			// アバター位置反映 
			var ofs = _targetCtrl.Origin * _offset;
			_targetCtrl.WorldEye = ofs * _input.Head.GetEyeTracking();
			_targetCtrl.WorldLeftHand = ofs * _input.HandLeft.GetHandTracking();
			_targetCtrl.WorldRightHand = ofs * _input.HandRight.GetHandTracking();
			_targetView.UpdateControl(_targetCtrl);
		}

		//! VRカメラを所定の位置に戻す 
		internal void ResetRig()
		{
			// カメラ位置 
			var ce = Loc.FromWorldTransform(_cam);
			// トラッキング位置 
			var re = _input.Head.GetEyeTracking();
			// 操作対象の目位置 
			var ve = _targetCtrl.WorldEye;

			// 回転Y軸を真上に戻した状態で判定 
			ce.Rot = PluggableVR.RotUt.ReturnY(ce.Rot);
			re.Rot = PluggableVR.RotUt.ReturnY(re.Rot);
			ve.Rot = PluggableVR.RotUt.ReturnY(ve.Rot);

			// リグを操作対象の目位置に合わせる 
			ve.ToWorldTransform(_rig);
			// カメラリセット 
			_input.Reset();

			// 入力オフセット 
			_offset = ve / _targetCtrl.Origin;
		}
	}
}
