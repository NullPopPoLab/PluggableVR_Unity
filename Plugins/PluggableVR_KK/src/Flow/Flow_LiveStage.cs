/*!	@file
	@brief PluggableVR: 手順遷移 ライブ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! ライブ状態 
	internal class Prop_LiveStage
	{
		public Transform Star;
		public Camera Camera_Prepare;
		public Camera Camera_Show;
	}


	//! 手順遷移 ライブ 
	internal class Flow_LiveStage : Flow_Common
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");

			// メインカメラの扱い 
			VRCamera.SourceMode = VRCamera.ESourceMode.Disabled;

			base.OnStart();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			var prop = new Prop_LiveStage();
			prop.Star = GameObject.Find("/chaF_001").transform;
			prop.Camera_Prepare = Camera.main;
			prop.Camera_Show = prop.Star.Find("BodyTop/p_cf_body_bone/cf_j_root/cf_n_height/CameraObject(Clone)/n_adjust/n_cam/Camera").GetComponent<Camera>();

			return new Flow_LiveStage_Prepare(prop);
		}
	}

	//! 手順遷移 ライブ 準備中 
	internal class Flow_LiveStage_Prepare : Flow_Common
	{
		private Prop_LiveStage _prop;

		public Flow_LiveStage_Prepare(Prop_LiveStage prop)
		{
			_prop = prop;
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");

			// メインカメラ切り替え 
			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(_prop.Camera_Prepare);

			// メインカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// メインカメラから遮断するComponent 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
			cam.Suppress<AmplifyColorEffect>();
			cam.Suppress<AmplifyOcclusionEffect>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(0);

			base.OnStart();
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

			// 抜け検知 
			if(Global.Scene.ActiveScene!="Assets/LiveStage/LiveStage.unity")return new Flow_Delay(new Flow_Title());
			// 開演検知 
			if (_prop.Camera_Show.gameObject.activeInHierarchy) return new Flow_LiveStage_Show(_prop);

			return StepScene();
		}
	}

	//! 手順遷移 ライブ 開演中 
	internal class Flow_LiveStage_Show : Flow_Common
	{
		private Prop_LiveStage _prop;

		public Flow_LiveStage_Show(Prop_LiveStage prop)
		{
			_prop = prop;
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");

			// VRカメラ位置を保持 
			var mng = VRManager.Instance;
			var loc = Loc.FromWorldTransform(mng.Camera.Target.transform);

			// メインカメラ切り替え 
			var player = mng.Player;
			player.SetCamera(_prop.Camera_Show);
			player.Reloc(loc);

			// メインカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();
			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// メインカメラから遮断するComponent 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
			cam.Suppress<AmplifyColorEffect>();
			cam.Suppress<AmplifyOcclusionEffect>();
//			cam.Suppress<CrossFade>();
//			cam.Suppress<CameraEffector>();
//			cam.Suppress<CameraEffectorConfig>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(0);

			base.OnStart();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			// 移設Component除去 
			var mng = VRManager.Instance;
			mng.Camera.Dispose();

			// 元カメラを有効に戻しとく 
			mng.Camera.Source.enabled = true;

			// メインカメラ切断 
			var player = mng.Player;
			player.SetCamera(null);

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// 終演検知 
			if (!_prop.Camera_Show.gameObject.activeInHierarchy) return new Flow_LiveStage_Prepare(_prop);

			return base.StepScene();
		}
	}
}