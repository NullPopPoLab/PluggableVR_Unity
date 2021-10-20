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
		private Loc _offset;
		private bool _sticking = false;
		private bool _elevating = false;
		private RelativeBool _push_pstk = new RelativeBool();

		internal VRPlayer(VRAvatar target)
		{
			_rig = CreateRootObject("VRPlayer", Loc.FromWorldTransform(target.Eye)).transform;
			_cam = CreateChildObject("VRCamera", _rig, Loc.Identity, false).transform;
			GameObject.DontDestroyOnLoad(_rig.gameObject);

			var cam = _cam.gameObject.AddComponent<Camera>();
			cam.nearClipPlane = 0.01f;
			_cam.gameObject.AddComponent<AudioListener>();


			_targetView = target;
			_targetCtrl = _targetView.CreateControl();
			ResetRig();

			// 初期状態では頭を非表示とする 
			// 俯瞰操作の間だけ表示 
			_targetView.Head.gameObject.SetActive(false);

			// xは常に非表示 
			// y,zは _elevating で切り替える 
			_showElevating();
		}

		private void _showElevating()
		{
			_targetView.Up.SetActive(_elevating);
			_targetView.Fore.SetActive(!_elevating);
		}

		internal void Update()
		{
			var inp = VRManager.Input;

			// スティックの指載せで操作の主観/俯瞰を切り替える 
			var stk1 = inp.HandPrimary.IsStickTouching();
			var stk2 = inp.HandSecondary.IsStickTouching();
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
			_push_pstk.Update(inp.HandPrimary.IsStickPressed());
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
				var tilt = inp.HandSecondary.GetStickTilting();
				_targetCtrl.Origin.Rot *= PluggableVR.RotUt.RotY(90.0f * Mathf.Deg2Rad * tilt.x * Time.deltaTime);
			}

			// スティック移動 
			if (stk1)
			{
				// スティック倒し状態 
				var tilt = inp.HandPrimary.GetStickTilting();
				// zx平面上のy軸2D回転 
				var dir = PluggableVR.RotUt.PlaneZX(_cam.rotation);
				if (_elevating)
				{
					// スティックy方向はy軸と一致 
					// スティックx方向はy軸回転(正面はz方向なのでさらに+90°)を反映 
					// ついでに移動スピードも掛ける 
					var m = new Vector3(tilt.x*dir.C, tilt.y, -tilt.x*dir.S) * Time.deltaTime;
					_targetCtrl.Origin.Pos += m;
				}
				else
				{
					// スティック方向をy軸回転 
					// ついでに移動スピードも掛ける 
					var mzx = dir * new Vector2(tilt.y, tilt.x) * Time.deltaTime;
					_targetCtrl.Origin.Pos += new Vector3(mzx.y, 0, mzx.x);
				}
			}

			// アバター位置反映 
			var ofs = _targetCtrl.Origin * _offset;
			_targetCtrl.WorldEye = ofs * inp.Head.GetEyeTracking();
			_targetCtrl.WorldLeftHand = ofs * inp.HandLeft.GetHandTracking();
			_targetCtrl.WorldRightHand = ofs * inp.HandRight.GetHandTracking();
			_targetView.UpdateControl(_targetCtrl);
		}

		//! VRカメラを所定の位置に戻す 
		internal void ResetRig()
		{
			var inp = VRManager.Input;

			// カメラ位置 
			var ce = Loc.FromWorldTransform(_cam);
			// トラッキング位置 
			var re = inp.Head.GetEyeTracking();
			// 操作対象の目位置 
			var ve = _targetCtrl.WorldEye;

			// 回転Y軸を真上に戻した状態で判定 
			ce.Rot = RotUt.ReturnY(ce.Rot);
			re.Rot = RotUt.ReturnY(re.Rot);
			ve.Rot = RotUt.ReturnY(ve.Rot);

			// リグを操作対象の目位置に合わせる 
			ve.ToWorldTransform(_rig);
			// カメラリセット 
			inp.Reset();

			// 入力オフセット 
			_offset = ve / _targetCtrl.Origin;
		}

		//! カメラ位置変更 
		internal void ChangeCamera(Loc loc){

			// 操作対象の目位置からの差分をOriginに反映 
			var d = loc / _targetCtrl.WorldEye;
			_targetCtrl.Origin *= d;
			_targetView.UpdateControl(_targetCtrl);
			ResetRig();
		}
	}
}
