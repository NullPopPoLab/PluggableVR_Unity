﻿/*!	@file
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
		private CharaFinder _female = new CharaFinder(false);
		private CharaFinder _male = new CharaFinder(true);

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

			// 追加されたキャラに対する処置 
			var idx = _male.Next;
			_male.Find(_findMale);
			_female.Find(_findFemale);

			return null;
		}

		private void _findMale(CharaObserver obs)
		{
//			Global.Logger.LogDebug("find male: " + obs.Target.name);
		}

		private void _findFemale(CharaObserver obs)
		{
//			Global.Logger.LogDebug("find female: " + obs.Target.name);

			var avatar = VRManager.Instance.Avatar as DemoAvatar;
			var db = obs.Target.GetComponentsInChildren<DynamicBone>();

			Global.Logger.LogDebug("" + db.Length + " DynamicBone found on " + obs.Target.name);

			for (var i = 0; i < db.Length; ++i)
			{
				if (db[i].m_Colliders == null) continue;
				db[i].m_Colliders.Add(avatar.LeftHand.Collider.GetComponent<DynamicBoneCollider>());
				db[i].m_Colliders.Add(avatar.RightHand.Collider.GetComponent<DynamicBoneCollider>());
			}
		}
	}
}
