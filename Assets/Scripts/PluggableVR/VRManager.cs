﻿/*!	@file
	@brief PluggableVR: VRシステム管理 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PluggableVR
{
	//! VRシステム管理 
	internal class VRManager : PlugCommon
	{
		internal static VRManager Instance;

		//! ユーザ入力部 
		internal static Input Input { get; private set; }
		//! VR操作部 
		internal VRController Controller { get; private set; }

		//! 現在の手順遷移 
		private Flow _curFlow;
		//! 現在捕捉中のカメラ 
		private Camera _curCamera;

		internal VRManager()
		{
			Instance = this;
			Controller = new VRController();
		}

		//! 初期設定 
		internal void Initialize(Flow flow)
		{
			Input = Oculus.Input.Setup();

			OVRPlugin.rotation = true;
			OVRPlugin.useIPDInPositionTracking = true;

			Start(flow);
		}

		//! 機能終了 
		internal void Shutdown()
		{
			_curCamera = null;
			Controller.Shutdown();
		}

		//! 遷移開始 
		internal void Start(Flow flow)
		{
			if (_curFlow != null) return;
			if (flow == null) return;
			_curFlow = flow;
			flow.Start();
		}

		//! シーン変更検知時に呼ばれる 
		internal void SceneChanged(Scene scn)
		{
			if (Controller != null) Controller.SceneChanged(scn);
		}

		//! カメラ変更検知時に呼ばれる 
		internal void CameraChanged(Camera cam)
		{
			if (cam == _curCamera) return;
			if (cam == null)
			{
				// カメラがなくなったら機能終了とする 
				Shutdown();
				return;
			}
			if (Controller != null) Controller.Reloc(Loc.FromWorldTransform(cam.transform));
		}

		//! 位置だけ変更 
		internal void Repos(Vector3 pos)
		{
			if (Controller != null) Controller.Repos(pos);
		}

		//! 向きだけ変更 
		internal void Rerot(Quaternion rot)
		{
			if (Controller != null) Controller.Rerot(rot);
		}

		//! 位置,向き変更 
		internal void Reloc(Loc loc)
		{
			if (Controller != null) Controller.Reloc(loc);
		}

		//! 物理フレーム毎の更新 
		internal void FixedUpdate()
		{
			Input.FixedUpdate();
		}

		//! 描画フレーム毎の更新 
		internal void Update()
		{
			Input.Update();
			if (Controller != null) Controller.Update();

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
