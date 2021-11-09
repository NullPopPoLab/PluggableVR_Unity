/*!	@file
	@brief PluggableVR: 手順遷移 えっち 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! 手順遷移 えっち 
	internal class Flow_HProc : Flow_Common
	{
		private Flow _prev;
		private Chaser _chaser;

		internal Flow_HProc(Flow prev)
		{
			_prev=prev;
		}

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
//			cam.Possess<AmplifyOcclusionEffect>();
//			cam.Possess<AmplifyColorEffect>();
//			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
//			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();
//			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();
//			cam.Possess<Obi.ObiFluidRenderer>();

			// メインカメラから除去するComponent 
//			cam.Possess<CameraEffectorConfig>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);

			// メインカメラ追跡 
			_chaser = new Chaser(cam.Source.transform);
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

			// カメラ位置変更検知 
			if (_chaser.Update())
			{
				var mng = VRManager.Instance;
				var player = mng.Player;
				player.Reloc(_chaser.Loc);
			}

			return null;
		}
	}
}
