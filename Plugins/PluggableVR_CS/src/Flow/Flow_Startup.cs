/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_CS
{
	//! 手順遷移 開始時 
	internal class Flow_Startup : Flow
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// VR初期設定 
			var scale = 1.0f;
			var avatar = new DemoAvatar(Loc.Identity, scale);
			var player = new DemoPlayer(avatar, scale);
			VRManager.Instance.SetPlayer(player);

			// 手の軸表示を消す 
			avatar.LeftHand.Axes.Node.SetActive(false);
			avatar.RightHand.Axes.Node.SetActive(false);
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
			cam.Possess<FlareLayer>();
			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
			cam.Possess<AmplifyColorEffect>();
			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(4);

			return new Flow_Edit();
		}
	}
}