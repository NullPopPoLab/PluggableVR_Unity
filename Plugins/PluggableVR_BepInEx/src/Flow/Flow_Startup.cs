/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.SN2
{
	//! 手順遷移 開始時 
	internal class Flow_Startup : Flow
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// Unityシーンロードに連動する遷移 
			Global.Transit["StudioSceneLoad"] = () =>
			{
				if (GameObject.Find("/SceneLoadScene") == null) return null;
				return new Flow_SceneLoaded();
			};
			Global.Transit["StudioCheck"] = () =>
			{
				if (GameObject.Find("/StudioCheck") == null) return null;
				return new Flow_ScenePurging();
			};

			// VR初期設定 
			var scale = 8.0f;
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
			player.SetCamera(Camera.main);

			// 元のカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			cam.Possess<GameScreenShot>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(4);

			return new Flow_Edit();
		}
	}
}