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
	internal class VRManager : PlugCommon
	{
		internal static VRManager Instance;

		internal bool IsReady { get; private set; }

		//! ユーザ入力部 
		internal static Input Input { get; private set; }
		//! VR操作元 
		internal VRPlayer Player { get; private set; }
		//! VR操作先 
		internal VRAvatar Avatar { get { return Player.Avatar; } }
		//! VRカメラ 
		internal VRCamera Camera { get { return Player.Camera; } }

		//! 現在の手順遷移 
		private Flow _curFlow;

		internal VRManager()
		{
			Instance = this;
		}

		//! 初期設定 
		internal void Initialize(Flow flow)
		{
			if (IsReady) return;
			IsReady = true;

			Input = Oculus.Input.Setup();

			OVRPlugin.rotation = true;
			OVRPlugin.useIPDInPositionTracking = true;

			Start(flow);
		}

		//! 機能終了 
		internal void Shutdown()
		{
			if (!IsReady) return;
			IsReady = false;
		}

		//! 遷移開始 
		internal void Start(Flow flow)
		{
			if (!IsReady) return;
			if (_curFlow != null) return;
			if (flow == null) return;
			_curFlow = flow;
			flow.Start();
		}

		//! プレイヤー設定 
		internal void SetPlayer(VRPlayer player)
		{
			if (!IsReady) return;
			Player = player;
		}

		//! 位置だけ変更 
		internal void Repos(Vector3 pos)
		{
			if (!IsReady) return;
			if (Player != null) Player.Repos(pos);
		}

		//! 向きだけ変更 
		internal void Rerot(Quaternion rot)
		{
			if (!IsReady) return;
			if (Player != null) Player.Rerot(rot);
		}

		//! 位置,向き変更 
		internal void Reloc(Loc loc)
		{
			if (!IsReady) return;
			if (Player != null) Player.Reloc(loc);
		}

		//! 物理フレーム毎の更新 
		internal void FixedUpdate()
		{
			Input.FixedUpdate();
		}

		//! 描画フレーム毎の更新 
		internal void Update()
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
		internal void LateUpdate()
		{
			Input.LateUpdate();
		}
	}
}
