#if false
/*!	@file
	@brief PluggableVR: 手順遷移 キャラエディット 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! 手順遷移 キャラエディット 
	internal class Flow_CustomScene : Flow_Common
	{
		private Flow _prev;
		private Camera _prevcam;
		public Flow_CustomScene(Flow prev){
			_prev = prev;
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// 元カメラ退避 
			var mng = VRManager.Instance;
			_prevcam = (mng.Camera == null) ? null : mng.Camera.Source;

			// メインカメラ捕捉 
			var player = mng.Player;
			player.SetCamera(Camera.main);

			// メインカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
			cam.Possess<AmplifyColorEffect>();
			cam.Possess<ChaCustom.CustomRender>();
			cam.Possess<GameScreenShot>();
			cam.Possess<UnityEngine.EventSystems.PhysicsRaycaster>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			// 移設Component除去 
			var mng = VRManager.Instance;
			mng.Camera.Dispose();

			// メインカメラ切断 
			var player = mng.Player;
			player.SetCamera(_prevcam);

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// キャラメイク終了検知 
			if (!Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scripts/Scene/Custom/CustomScene.unity").isLoaded) return new Flow_Delay(_prev);

			// メインカメラ位置更新 
			var mng = VRManager.Instance;
			mng.Camera.Feedback();

			return base.StepScene();
		}
	}
}
#endif
