/*!	@file
	@brief PluggableVR: 手順遷移 えっち 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! 手順遷移 えっち 
	internal class Flow_HProc : Flow_Common
	{
		private Flow _prev;

		internal Flow_HProc(Flow prev)
		{
			_prev=prev;
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// UIのVR対応までひとまず Canvas をoverlayにしとく 
			var ui = GameObject.Find("/Canvas").GetComponent<Canvas>();
			ui.renderMode = RenderMode.ScreenSpaceOverlay;

			// メインカメラの扱い 
			VRCamera.SourceMode = VRCamera.ESourceMode.Blind;

			// メインカメラ捕捉 
			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(Camera.main);

			// メインカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
//			cam.Possess<AmplifyColorEffect>();
//			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
//			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
//			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(4);
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

			// 終了検知 
			if (!Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/HProc.unity").isLoaded) return new Flow_Delay(_prev);

			// メインカメラ位置更新 
			var mng = VRManager.Instance;
			mng.Camera.Feedback();

			return null;
		}
	}
}
