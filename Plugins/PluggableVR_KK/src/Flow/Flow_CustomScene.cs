/*!	@file
	@brief PluggableVR: 手順遷移 キャラエディット 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! 手順遷移 キャラエディット 
	internal class Flow_CustomScene : Flow_Common
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
			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
			cam.Possess<ChaCustom.CustomRender>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(0);
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

			// メインカメラ位置更新 
			var mng = VRManager.Instance;
			mng.Camera.Feedback();

			return base.StepScene();
		}
	}
}