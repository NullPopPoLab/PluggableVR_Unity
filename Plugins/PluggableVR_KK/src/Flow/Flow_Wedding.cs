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
	//! 手順遷移 ウェディング 
	internal class Flow_Wedding : Flow_Common
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// メインカメラの扱い 
			VRCamera.SourceMode = VRCamera.ESourceMode.Disabled;

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

			// メインカメラ切断 
			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(null);

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// 開宴検知 
			if (Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/ADV.unity").isLoaded)
			{
				// 終宴後また使うので有効に戻しとく 
				VRManager.Instance.Camera.Recall();
				return new Flow_ADV(this, "Assets/Illusion/Game/Scripts/Scene/Wedding/Wedding.unity");
			}

			var next = StepScene();
			if (next == null) return null;
			VRManager.Instance.Camera.Dispose();
			return next;
		}
	}
}
