/*!	@file
	@brief PluggableVR: 手順遷移 シーン切り替え共通部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 シーン切り替え共通部 準備
	public class Flow_Switch_Prepare : Flow
	{
		protected string CurScene;

		protected override void OnStart()
		{
			base.OnStart();

			CurScene = Global.LastLoadedScene;
		}
	}

	//! 手順遷移 シーン切り替え共通部 開始
	public class Flow_Switch_Start : Flow
	{
		protected Transform CameraRoot;
		protected Camera MainCamera;

		protected Loc CameraLoc;

		protected override void OnStart()
		{
			base.OnStart();

			CameraRoot = GameObject.Find("/Camera").transform;
			MainCamera = CameraRoot.Find("Main Camera").GetComponent<Camera>();

			CameraLoc = Loc.FromWorldTransform(CameraRoot);
		}

		protected void UpdateCameraParam()
		{
			// メインカメラ位置に移動 
			var sc = MainCamera;
			VRManager.Instance.Reloc(Loc.FromWorldTransform(sc.transform));

			// メインカメラのパラメータを反映 
			var dc = (VRManager.Instance.Player as DemoPlayer).Camera.GetComponent<Camera>();
			dc.clearFlags = sc.clearFlags;
			dc.cullingMask = sc.cullingMask;
			dc.farClipPlane = sc.farClipPlane;
			dc.nearClipPlane = 0.1f;
			// 変更不可らしい 
			//			dc.fieldOfView = sc.fieldOfView;

			// 本来のメインカメラは無効化…するとハマるのでculingMaskを外してみる 
			sc.cullingMask = 0;
			//			sc.enabled = false;
			//			var lsn = sc.GetComponent<AudioListener>();
			//			if (lsn != null) lsn.enabled = false;
		}
	}

	//! 手順遷移 シーン切り替え共通部 本体
	public class Flow_Switch_Main : Flow
	{
		protected string CurScene;

		protected override Flow OnUpdate()
		{
			if(CurScene!=Global.LastLoadedScene){
				CurScene=Global.LastLoadedScene;
				if(!Global.Transit.ContainsKey(CurScene))return null;
				return Global.Transit[CurScene]();
			}

			return null;
		}
	}
}