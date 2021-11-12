#if false
/*!	@file
	@brief PluggableVR: 手順遷移 えっち 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System.Collections.Generic;
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
		private CharaFinder _female;
		private CharaFinder _male;

		internal Flow_HProc(Flow prev, int chafidx, int chamidx)
		{
			_prev=prev;
			_female = new CharaFinder(false,chafidx);
			_male = new CharaFinder(true,chamidx);
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// UIのVR対応までひとまず Canvas をoverlayにしとく 
			var ui = GameObject.Find("/Canvas").GetComponent<Canvas>();
			ui.renderMode = RenderMode.ScreenSpaceOverlay;

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

			for (var i = 0; i < _female.List.Count; ++i)
			{
				_female.List[i].RemovePlayerColliders();
			}

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// 終了検知 
			if (!Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/HProc.unity").isLoaded) return new Flow_HEnd(_prev);

			// カメラ位置変更検知 
			if (_chaser.Update())
			{
				var mng = VRManager.Instance;
				var player = mng.Player;
				player.Reloc(_chaser.Loc);
			}

			// 追加されたキャラに対する処置 
			_male.Find(_maleFound);
			_female.Find(_femaleFound);

			return null;
		}

		private void _maleFound(CharaObserver obs)
		{
//			Global.Logger.LogDebug("male found: " + obs.Target.name);
		}

		private void _femaleFound(CharaObserver obs)
		{
//			Global.Logger.LogDebug("female found: " + obs.Target.name);

			obs.AddPlayerColliders();
		}
	}
}
#endif
