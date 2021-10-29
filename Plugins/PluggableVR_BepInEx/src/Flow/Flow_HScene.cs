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
		private Transform _camera;
		private ChangingCensor<Loc> _cameraLoc;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			_camera = GameObject.Find("/HCamera").transform;
			_cameraLoc = new ChangingCensor<Loc>(Loc.FromWorldTransform(_camera));
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			if (!_cameraLoc.Update(Loc.FromWorldTransform(_camera))) return null;
			return new Flow_HScene_Main();

//			if (!LastLoadedScene.Update(Global.LastLoadedScene)) return null;
//			return new Flow_Delay(new Flow_HScene_Main());
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
			Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>(_mainCamera);
			Possess<CrossFade>(_mainCamera);
			Possess<Obi.ObiFluidRenderer>(_mainCamera);
//			Possess<UnityStandardAssets.ImageEffects.DepthOfField>(_mainCamera);
			Possess<UnityStandardAssets.ImageEffects.GlobalFog>(_mainCamera);
			Possess<UnityStandardAssets.ImageEffects.SunShafts>(_mainCamera);
//			Possess<ScreenEffect>(_mainCamera);
			Possess<CameraEffector.ConfigEffectorWet>(_mainCamera);
			Possess<PlaceholderSoftware.WetStuff.WetStuff>(_mainCamera);
			Possess<GameScreenShotAssist>(_mainCamera);

			Suppress<UnityStandardAssets.ImageEffects.DepthOfField>(_mainCamera);
			Suppress<ScreenEffect>(_mainCamera);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			Remove<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			Remove<CrossFade>();
			Remove<Obi.ObiFluidRenderer>();
//			Remove<UnityStandardAssets.ImageEffects.DepthOfField>();
			Remove<UnityStandardAssets.ImageEffects.GlobalFog>();
			Remove<UnityStandardAssets.ImageEffects.SunShafts>();
//			Remove<ScreenEffect>();
			Remove<CameraEffector.ConfigEffectorWet>();
			Remove<PlaceholderSoftware.WetStuff.WetStuff>();
			Remove<GameScreenShotAssist>();

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();
			return StepScene();
		}
	}
}