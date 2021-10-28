/*!	@file
	@brief PluggableVR: 手順遷移 会話シーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 会話シーン 
	internal class Flow_ADV : Flow_Common
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			if (!LastLoadedScene.Update(Global.LastLoadedScene)) return null;

			var mc = GameObject.Find("/ADVMainScene/ADVScene(Clone)/BasePosition/Cameras/Main Camera");
			if (mc == null) return null;
			return new Flow_ADV_Main();

//			return new Flow_Delay(new Flow_ADV_Main());
		}
	}

	//! 手順遷移 会話シーン 本体 
	internal class Flow_ADV_Main : Flow_Common
	{
		private Camera _mainCamera;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// Camera構造が通常と違う 
			_mainCamera = GameObject.Find("/ADVMainScene/ADVScene(Clone)/BasePosition/Cameras/Main Camera").GetComponent<Camera>();

			UpdateCameraParam(4,_mainCamera);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();
			return StepScene();
		}
	}
}