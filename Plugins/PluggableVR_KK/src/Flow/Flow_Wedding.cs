/*!	@file
	@brief PluggableVR: 手順遷移 ウェディング 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! 手順遷移 ウェディング 準備
	internal class Flow_Wedding : Flow_Common
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
			cam.Possess<AmplifyColorEffect>();
			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// メインカメラから遮断するComponent 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(0);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			// 移設Component除去 
			var mng = VRManager.Instance;
			mng.Camera.Recall();

			// 元カメラを有効に戻しとく 
			mng.Camera.Source.enabled = true;

			// メインカメラ切断 
			var player = mng.Player;
			player.SetCamera(null);

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// 開宴検知 
			if(Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/ADV.unity").isLoaded)return new Flow_Wedding_Show();

			// メインカメラ位置更新 
			var mng = VRManager.Instance;
			mng.Camera.Feedback();

			return StepScene();
		}
	}

	//! 手順遷移 ウェディング 開宴
	internal class Flow_Wedding_Show : Flow_Common
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
			cam.Possess<AmplifyColorEffect>();
			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(4);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			// 移設Component除去 
			var mng = VRManager.Instance;
			mng.Camera.Dispose();

			// メインカメラ切断 
			var player = mng.Player;
			player.SetCamera(null);

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// 脱出検知 
			if(!Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scripts/Scene/Wedding/Wedding.unity").isLoaded)return new Flow_Delay(new Flow_Title());
			// 終宴検知 
			if(!Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/ADV.unity").isLoaded)return new Flow_Delay(new Flow_Wedding());

			return  null;
		}
	}
}
