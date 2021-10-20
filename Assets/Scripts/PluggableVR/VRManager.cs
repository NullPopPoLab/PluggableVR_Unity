/*!	@file
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
		//! ユーザ入力部 
		internal static Input Input { get; private set; }
		//! VR操作部 
		internal VRController Controller { get; private set; }

		//! 現在捕捉中のカメラ 
		private Camera _curCamera;

		internal VRManager()
		{
			Controller = new VRController();
		}

		//! 初期設定 
		internal void Initialize(Camera cam)
		{
			Input = Oculus.Input.Setup();

			OVRPlugin.rotation = true;
			OVRPlugin.useIPDInPositionTracking = true;

			// カメラが指定されているときだけ稼働とする 
			if (cam == null) return;
			_curCamera = cam;
			Controller.Initialize(cam);
		}

		//! 機能終了 
		internal void Shutdown()
		{

			_curCamera = null;
			Controller.Shutdown();
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
			if (Controller != null) Controller.CameraChanged(cam);
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
		}

		//! アニメーション処理後の更新 
		internal void LateUpdate()
		{
			Input.LateUpdate();
		}
	}
}
