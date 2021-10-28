/*!	@file
	@brief PluggableVR: 手順遷移 えっちシーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 えっちシーン 
	internal class Flow_HScene : Flow_Common
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			UpdateCameraParam(4);
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

			return new Flow_Delay(new Flow_HScene_Main());
		}
	}

	//! 手順遷移 えっちシーン 本体 
	internal class Flow_HScene_Main : Flow_Common
	{
		private Camera _mainCamera;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// Camera構造が通常と違う 
			var cb = GameObject.Find("/HCamera").transform;
			_mainCamera = cb.Find("Main Camera").GetComponent<Camera>();

			UpdateCameraParam(4, _mainCamera);
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