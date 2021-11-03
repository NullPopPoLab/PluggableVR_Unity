/*!	@file
	@brief PluggableVR: VRシステム管理 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR
{
	//! VRシステム管理 
	public class VRManager : PlugCommon
	{
		public static VRManager Instance;

		public bool IsReady { get; private set; }

		//! ユーザ入力部 
		public static Input Input { get; private set; }
		//! VR操作元 
		public VRPlayer Player { get; private set; }
		//! VR操作先 
		public VRAvatar Avatar { get { return Player.Avatar; } }
		//! VRカメラ 
		public VRCamera Camera { get { return Player.Camera; } }

		//! 現在の手順遷移 
		private Flow _curFlow;

		public VRManager()
		{
			Instance = this;
		}

		//! 初期設定 
		public void Initialize(Flow flow)
		{
			if (IsReady) return;
			IsReady = true;

			Input = Oculus.Input.Setup();

			OVRPlugin.rotation = true;
			OVRPlugin.useIPDInPositionTracking = true;

			Start(flow);
		}

		//! 機能終了 
		public void Shutdown()
		{
			if (!IsReady) return;
			IsReady = false;
		}

		//! 遷移開始 
		public void Start(Flow flow)
		{
			if (!IsReady) return;
			if (_curFlow != null) return;
			if (flow == null) return;
			_curFlow = flow;
			flow.Start();
		}

		//! プレイヤー設定 
		public void SetPlayer(VRPlayer player)
		{
			if (!IsReady) return;
			Player = player;
		}

		//! 位置だけ変更 
		public void Repos(Vector3 pos)
		{
			if (!IsReady) return;
			if (Player != null) Player.Repos(pos);
		}

		//! 向きだけ変更 
		public void Rerot(Quaternion rot)
		{
			if (!IsReady) return;
			if (Player != null) Player.Rerot(rot);
		}

		//! 位置,向き変更 
		public void Reloc(Loc loc)
		{
			if (!IsReady) return;
			if (Player != null) Player.Reloc(loc);
		}

		//! 物理フレーム毎の更新 
		public void FixedUpdate()
		{
			Input.FixedUpdate();
		}

		//! 描画フレーム毎の更新 
		public void Update()
		{
			if (!IsReady) return;
			Input.Update();
			if (Player != null) Player.Update();

			if (_curFlow != null)
			{
				_curFlow = _curFlow.Update();
			}
		}

		//! アニメーション処理後の更新 
		public void LateUpdate()
		{
			Input.LateUpdate();
		}
	}
}
