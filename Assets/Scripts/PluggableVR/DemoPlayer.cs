/*!	@file
	@brief PluggableVR: デモ用VR操作元 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR
{
	//! デモ用VR操作元 
	internal class DemoPlayer : VRPlayer
	{
		internal float Scale { get; private set; }
		internal Transform Rig { get; private set; }
		internal Transform Camera { get; private set; }
		internal new DemoAvatar Avatar { get { return base.Avatar as DemoAvatar; } set { base.Avatar = value; } }

		private DemoControl _ctrl;
		private Loc _offset;
		private bool _sticking = false;
		private bool _elevating = false;
		private RelativeBool _push_pstk = new RelativeBool();

		internal DemoPlayer(DemoAvatar target, float scale = 1.0f)
		{
			Scale = scale;

			var root = CreateRootObject("RoomScale", Loc.Identity).transform;
			root.localScale = new Vector3(scale, scale, scale);
			GameObject.DontDestroyOnLoad(root.gameObject);

			Rig = CreateChildObject("VRPlayer", root, Loc.FromWorldTransform(target.Eye), false).transform;
			Camera = CreateChildObject("VRCamera", Rig, Loc.Identity, false).transform;

			var cam = Camera.gameObject.AddComponent<Camera>();
			cam.nearClipPlane = 0.01f;
			Camera.gameObject.AddComponent<AudioListener>();


			Avatar = target;
			_ctrl = Avatar.CreateControl();
			ResetRig();

			// 初期状態では頭を非表示とする 
			// 俯瞰操作の間だけ表示 
			Avatar.View.gameObject.SetActive(false);

			// xは常に非表示 
			// y,zは _elevating で切り替える 
			_showElevating();
		}

		private void _showElevating()
		{
			Avatar.UpFromHead.SetActive(_elevating);
			Avatar.ForeFromHead.SetActive(!_elevating);
		}

		protected override void OnUpdate()
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
					Avatar.View.gameObject.SetActive(true);
					_showElevating();
				}
			}
			else
			{
				if (!(stk1 || stk2))
				{
					_resetControl();
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
				var dr = RotUt.RotY(90.0f * Mathf.Deg2Rad * tilt.x * Time.deltaTime);
				var pp = _ctrl.WorldPivot.Pos;
				_ctrl.Origin.Rot *= dr;
				_ctrl.Origin.Rot = RotUt.ReturnY(_ctrl.Origin.Rot);
				_ctrl.Origin.Pos -= _ctrl.WorldPivot.Pos - pp;
			}

			// スティック移動 
			if (stk1)
			{
				// スティック倒し状態 
				var tilt = inp.HandPrimary.GetStickTilting();
				// zx平面上のy軸2D回転 
				var dir = RotUt.PlaneZX(Camera.rotation);
				if (_elevating)
				{
					// スティックy方向はy軸と一致 
					// スティックx方向はy軸回転(正面はz方向なのでさらに+90°)を反映 
					// ついでに移動スピードも掛ける 
					var m = new Vector3(tilt.x * dir.C, tilt.y, -tilt.x * dir.S) * Time.deltaTime;
					_ctrl.Origin.Pos += m * Scale;
				}
				else
				{
					// スティック方向をy軸回転 
					// ついでに移動スピードも掛ける 
					var mzx = dir * new Vector2(tilt.y, tilt.x) * Time.deltaTime;
					_ctrl.Origin.Pos += new Vector3(mzx.y, 0, mzx.x) * Scale;
				}
			}

			// アバター位置反映 
			var ofs = _ctrl.Origin * _offset;
			_ctrl.WorldEye = ofs * (inp.Head.GetEyeTracking() * Scale);
			_ctrl.WorldLeftHand = ofs * (inp.HandLeft.GetHandTracking() * Scale);
			_ctrl.WorldRightHand = ofs * (inp.HandRight.GetHandTracking() * Scale);

			// アバター頭位置 
			var ahd = _ctrl.WorldEye * new Loc(new Vector3(0, 0, -DemoAvatar.HeadToEye), Quaternion.identity);
			// アバター肩位置 
			var asd = ahd * new Loc(new Vector3(0, -DemoAvatar.NeckLength, 0), Quaternion.identity);
			// アバター回転基準位置 
			_ctrl.WorldPivot = asd - new Vector3(0, DemoAvatar.ShoulderHeight, 0);
			_ctrl.LocalPivot.Rot = RotUt.ReturnY(_ctrl.LocalPivot.Rot);

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

		private void _resetControl()
		{
			_sticking = false;
			Avatar.UpdateControl(_ctrl);
			ResetRig();
			Avatar.View.gameObject.SetActive(false);
		}

		//! 位置だけ変更 
		protected override void OnRepos(Vector3 pos)
		{
			// 操作対象の目位置からの差分をOriginに反映 
			_ctrl.Origin.Pos += pos - _ctrl.WorldEye.Pos;

			_resetControl();
		}

		//! 向きだけ変更 
		protected override void OnRerot(Quaternion rot)
		{
			// 操作対象の目向きからの差分をOriginに反映 
			// ただしY軸を真上に戻す 
			rot=RotUt.ReturnY(rot);
			_ctrl.Origin.Rot *= Quaternion.Inverse(RotUt.ReturnY(_ctrl.WorldEye.Rot)) * RotUt.ReturnY(rot);
			_ctrl.Origin.Rot = RotUt.ReturnY(_ctrl.Origin.Rot);

			_resetControl();
		}

		//! 位置,向き変更 
		protected override void OnReloc(Loc loc)
		{
			// 操作対象の目位置からの差分をOriginに反映 
			// ただしY軸を真上に戻す 
			loc.Rot=RotUt.ReturnY(loc.Rot);
			_ctrl.Origin *= _ctrl.WorldEye.Inversed * loc;
			_ctrl.Origin.Rot = RotUt.ReturnY(_ctrl.Origin.Rot);

			// 回転の兼ね合いでズレること多々あるので強制的に 
			_ctrl.Origin.Pos -= _ctrl.WorldEye.Pos - loc.Pos;

			_resetControl();
		}
	}
}
