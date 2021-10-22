﻿/*!	@file
	@brief PluggableVR: VR操作元 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace PluggableVR
{
	//! VR操作元 
	internal class VRPlayer : PlugCommon
	{
		internal Transform Rig { get; private set; }
		internal Transform Camera { get; private set; }
		internal VRAvatar Avatar { get; private set; }

		private AvatarControl _ctrl;
		private Loc _offset;
		private bool _sticking = false;
		private bool _elevating = false;
		private RelativeBool _push_pstk = new RelativeBool();

		internal VRPlayer(VRAvatar target)
		{
			Rig = CreateRootObject("VRPlayer", Loc.FromWorldTransform(target.Eye)).transform;
			Camera = CreateChildObject("VRCamera", Rig, Loc.Identity, false).transform;
			GameObject.DontDestroyOnLoad(Rig.gameObject);

			var cam = Camera.gameObject.AddComponent<Camera>();
			cam.nearClipPlane = 0.01f;
			Camera.gameObject.AddComponent<AudioListener>();


			Avatar = target;
			_ctrl = Avatar.CreateControl();
			ResetRig();

			// 初期状態では頭を非表示とする 
			// 俯瞰操作の間だけ表示 
			Avatar.Head.gameObject.SetActive(false);

			// xは常に非表示 
			// y,zは _elevating で切り替える 
			_showElevating();
		}

		private void _showElevating()
		{
			Avatar.UpFromHead.SetActive(_elevating);
			Avatar.ForeFromHead.SetActive(!_elevating);
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
					Avatar.Head.gameObject.SetActive(true);
					_showElevating();
				}
			}
			else
			{
				if (!(stk1 || stk2))
				{
					_sticking = false;
					ResetRig();
					Avatar.Head.gameObject.SetActive(false);
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
				_ctrl.Origin.Rot *= PluggableVR.RotUt.RotY(90.0f * Mathf.Deg2Rad * tilt.x * Time.deltaTime);
			}

			// スティック移動 
			if (stk1)
			{
				// スティック倒し状態 
				var tilt = inp.HandPrimary.GetStickTilting();
				// zx平面上のy軸2D回転 
				var dir = PluggableVR.RotUt.PlaneZX(Camera.rotation);
				if (_elevating)
				{
					// スティックy方向はy軸と一致 
					// スティックx方向はy軸回転(正面はz方向なのでさらに+90°)を反映 
					// ついでに移動スピードも掛ける 
					var m = new Vector3(tilt.x * dir.C, tilt.y, -tilt.x * dir.S) * Time.deltaTime;
					_ctrl.Origin.Pos += m;
				}
				else
				{
					// スティック方向をy軸回転 
					// ついでに移動スピードも掛ける 
					var mzx = dir * new Vector2(tilt.y, tilt.x) * Time.deltaTime;
					_ctrl.Origin.Pos += new Vector3(mzx.y, 0, mzx.x);
				}
			}

			// アバター位置反映 
			var ofs = _ctrl.Origin * _offset;
			_ctrl.WorldEye = ofs * inp.Head.GetEyeTracking();
			_ctrl.WorldLeftHand = ofs * inp.HandLeft.GetHandTracking();
			_ctrl.WorldRightHand = ofs * inp.HandRight.GetHandTracking();
			Avatar.UpdateControl(_ctrl);
		}

		//! VRカメラを所定の位置に戻す 
		internal void ResetRig()
		{
			var inp = VRManager.Input;

			// カメラ位置 
			var ce = Loc.FromWorldTransform(Camera);
			// トラッキング位置 
			var re = inp.Head.GetEyeTracking();
			// 操作対象の目位置 
			var ve = _ctrl.WorldEye;

			// 回転Y軸を真上に戻した状態で判定 
			ce.Rot = RotUt.ReturnY(ce.Rot);
			re.Rot = RotUt.ReturnY(re.Rot);
			ve.Rot = RotUt.ReturnY(ve.Rot);

			// リグを操作対象の目位置に合わせる 
			ve.ToWorldTransform(Rig);
			// カメラリセット 
			inp.Reset();

			// 入力オフセット 
			_offset = _ctrl.Origin.Inversed * ve;
		}

		//! 位置だけ変更 
		internal void Repos(Vector3 pos)
		{

			// 操作対象の目位置からの差分をOriginに反映 
			_ctrl.Origin.Pos += pos - _ctrl.WorldEye.Pos;

			Avatar.UpdateControl(_ctrl);
			ResetRig();
		}

		//! 向きだけ変更 
		internal void Rerot(Quaternion rot)
		{

			// 操作対象の目向きからの差分をOriginに反映 
			// ただしY軸を真上に戻す 
			_ctrl.Origin.Rot *= Quaternion.Inverse(RotUt.ReturnY(_ctrl.WorldEye.Rot)) * RotUt.ReturnY(rot);

			Avatar.UpdateControl(_ctrl);
			ResetRig();
		}

		//! 位置,向き変更 
		internal void Reloc(Loc loc)
		{

			// 操作対象の目位置からの差分をOriginに反映 
			// ただしY軸を真上に戻す 
			_ctrl.Origin *= _ctrl.WorldEye.Inversed * loc;
			_ctrl.Origin.Rot = RotUt.ReturnY(_ctrl.Origin.Rot);

			Avatar.UpdateControl(_ctrl);

			_sticking = false;
			ResetRig();
			Avatar.Head.gameObject.SetActive(false);
		}
	}
}
