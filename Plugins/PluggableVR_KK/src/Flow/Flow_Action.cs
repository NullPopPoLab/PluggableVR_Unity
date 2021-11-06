/*!	@file
	@brief PluggableVR: 手順遷移 インゲーム 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! 手順遷移 インゲーム 
	internal class Flow_Action : Flow_Common
	{
		private Canvas _minimap2d; 

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			var mm2 = GameObject.Find("/ActionScene/UI/Minimap/MiniMapCircle/MiniMapCanvas2D");
			_minimap2d = (mm2==null)?null:mm2.GetComponent<Canvas>();

			// メインカメラの扱い 
			VRCamera.SourceMode = VRCamera.ESourceMode.Blind;

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

			// 夜メニュー遷移検知 
			if (Global.Scene.GetSceneInfo("Assets/Illusion/assetbundle/action/menu/Menu/NightMenu.unity").isLoaded)
			{
				// 戻ってきてまた使うので有効に戻しとく 
				VRManager.Instance.Camera.Recall();
				return new Flow_NightMenu(this);
			}

			// えっち遷移検知 
			if (Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/H.unity").isLoaded)
			{
				// この時点で元カメラもうないらしい 
				VRManager.Instance.Camera.Dispose();
				return new Flow_H(this);
			}

			// 会話遷移検知 
			var mng = VRManager.Instance;
			var camera = mng.Camera;
			var cs = camera.Source;
			if (cs!=null && cs.transform.parent.name == "Cameras")
			{
				// 戻ってきてまた使うので有効に戻しとく 
				VRManager.Instance.Camera.Recall();
				return new Flow_ADV(this, true, "Assets/Illusion/Game/Scene/Action.unity");
			}

			// 移動シーン検知 
			if(_minimap2d!=null && _minimap2d.enabled){
				// 戻ってきてまた使うので有効に戻しとく 
				VRManager.Instance.Camera.Recall();
				return new Flow_Moving(this);
			}

			var next = StepScene();
			if (next == null) return null;
			VRManager.Instance.Camera.Dispose();
			return next;
		}
	}
}