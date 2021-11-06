/*!	@file
	@brief PluggableVR: 手順遷移 会話 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! 手順遷移 会話
	internal class Flow_ADV : Flow_Common
	{
		private string _basescene;
		private Flow _prev;
		private Chaser _chaser;

		internal Flow_ADV(Flow prev, string basescene)
		{
			_basescene = basescene;
			_prev=prev;
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

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

			// メインカメラ追跡 
			_chaser = new Chaser(cam.Source.transform);
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
			if (!Global.Scene.GetSceneInfo(_basescene).isLoaded) return new Flow_Delay(new Flow_Title());
			// 終宴検知 
			if (!Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/ADV.unity").isLoaded) return new Flow_Delay(_prev);

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
