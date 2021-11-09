/*!	@file
	@brief PluggableVR: 手順遷移 オープニング 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;
using ActionGame;

namespace PluggableVR_KKS
{
	//! 手順遷移 オープニング 
	internal class Flow_OpeningScene : Flow_Common
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// メインカメラ捕捉 
			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(Camera.main);

			// メインカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
			//			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			//			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
			//			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();
			//			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// メインカメラから遮断するComponent 
			//			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
			//			cam.Suppress<AmplifyColorEffect>();
			//			cam.Suppress<AmplifyOcclusionEffect>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();
			return new Flow_OpeningScene_Main();
		}
	}

	//! 手順遷移 オープニング 本体 
	internal class Flow_OpeningScene_Main : Flow_Common
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

			// 会話遷移検知 
			var mng = VRManager.Instance;
			var camera = mng.Camera;
			if (GameObject.Find("/ADVScene") != null)
			{
				return new Flow_ADV(this, "Assets/Illusion/Game/Scene/OpeningScene.unity");
			}

			var next = StepScene();
			if (next == null) return null;

			// メインカメラ切断 
			mng.Camera.Dispose();
			var player = mng.Player;
			player.SetCamera(null);

			return next;
		}
	}
}
