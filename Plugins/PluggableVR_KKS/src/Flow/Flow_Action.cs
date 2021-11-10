/*!	@file
	@brief PluggableVR: 手順遷移 インゲーム 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;
using ActionGame;

namespace PluggableVR_KKS
{
	//! 手順遷移 インゲーム 
	internal class Flow_Action : Flow_Common
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
			return new Flow_Action_Main();
		}
	}

	//! 手順遷移 インゲーム 本体 
	internal class Flow_Action_Main : Flow_Common
	{
		internal CharaFinder Female = new CharaFinder(false);
		internal CharaFinder Male = new CharaFinder(true);

		private Camera _minimap;
		private HomeMenu _homemenu;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			var mm2 = GameObject.Find("/ActionScene/UI/Minimap/MinimapCamera");
			_minimap = (mm2 == null) ? null : mm2.GetComponent<Camera>();
			_homemenu = GameObject.Find("/ActionScene/UI/HomeMenu").GetComponent<HomeMenu>();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// 夜メニュー遷移検知 
			if (_homemenu.visible)
			{
				return new Flow_HomeMenu(this);
			}

			// キャラメイク遷移検知 
			if (Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scripts/Scene/Custom/CustomScene.unity").isLoaded){
				// 戻ってきてまた使うので有効に戻しとく 
				VRManager.Instance.Camera.Recall();
				return new Flow_CustomScene(this);
			}

			// えっち遷移検知 
			if (Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/H.unity").isLoaded)
			{
				return new Flow_H(this);
			}

			// 会話遷移検知 
			var mng = VRManager.Instance;
			var camera = mng.Camera;
			if (GameObject.Find("/ActionScene/ADVScene") != null)
			{
				return new Flow_ADV(this, "Assets/Illusion/Game/Scene/Action.unity", Female.Next);
			}

			// 移動シーン検知 
			if (_minimap != null && _minimap.enabled)
			{
				return new Flow_Moving(this);
			}

			var next = StepScene();
			if (next != null)
			{
				// メインカメラ切断 
				mng.Camera.Dispose();
				var player = mng.Player;
				player.SetCamera(null);

				return next;
			}

			return null;
		}

		private void _findMale(CharaObserver obs)
		{
			Global.Logger.LogDebug("find male: " + obs.Target.name);
		}

		private void _findFemale(CharaObserver obs)
		{
			Global.Logger.LogDebug("find female: " + obs.Target.name);

			obs.AddPlayerColliders();
		}
	}
}
