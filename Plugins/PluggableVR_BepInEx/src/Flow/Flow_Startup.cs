/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.KK
{
	//! 手順遷移 開始時 
	internal class Flow_Startup : Flow
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			Terminate();
			return;

			// Unityシーンロードに連動する遷移 
#if false
			Global.Transit["StudioSceneLoad"] = () =>
			{
				if (GameObject.Find("/SceneLoadScene") == null) return null;
				return new Flow_SceneLoader();
			};
			Global.Transit["StudioCheck"] = () =>
			{
				if (GameObject.Find("/StudioCheck") == null) return null;
				return new Flow_ScenePurging();
			};
#endif

			// VR初期設定 
			var scale = 1.0f;
			var avatar = new DemoAvatar(Loc.Identity, scale);
			var player = new DemoPlayer(avatar, scale);
			player.Camera.SourceMode = VRCamera.ESourceMode.Disabled;
			VRManager.Instance.SetPlayer(player);

			// 手の軸表示を消す 
			avatar.LeftHand.SetActive(false);
			avatar.RightHand.SetActive(false);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// メインカメラ生成待ち 
			var mc = Camera.main;
			if (mc == null) return null;

			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(mc);

			// 元のカメラから移設するComponent 
			var cam = player.Camera;
//			cam.Possess<FlareLayer>();
//			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
//			cam.Possess<AmplifyColorEffect>();
//			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();


			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(4);

//			return new Flow_Edit();
			return null;
		}
	}
}