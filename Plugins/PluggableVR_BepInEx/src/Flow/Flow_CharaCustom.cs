/*!	@file
	@brief PluggableVR: 手順遷移 キャラメイク 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 キャラメイク 準備 
	internal class Flow_CharaCustom : Flow_Common
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

			return new Flow_Delay(new Flow_CharaCustom_Main());
		}
	}

	//! 手順遷移 キャラメイク 本体 
	internal class Flow_CharaCustom_Main : Flow_Common
	{
		private Camera _mainCamera;
		private Camera _coordCamera;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// Camera構造が通常と違う 
			var cb = GameObject.Find("/CharaCustom/CustomControl/CharaCamera").transform;
			_mainCamera = cb.Find("Main Camera").GetComponent<Camera>();
			_coordCamera = cb.Find("CoordinateCamera").GetComponent<Camera>();

			UpdateCameraParam(10, _mainCamera);
			_coordCamera.cullingMask = 0;
			Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>(_mainCamera);
			Possess<GameScreenShot>(_mainCamera);
			Possess<UnityEngine.EventSystems.PhysicsRaycaster>(_mainCamera);
			Possess<CharaCustom.CustomRender>(_mainCamera);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			Remove<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			Remove<GameScreenShot>();
			Remove<UnityEngine.EventSystems.PhysicsRaycaster>();
			Remove<CharaCustom.CustomRender>();

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();
			return StepScene();
		}
	}
}