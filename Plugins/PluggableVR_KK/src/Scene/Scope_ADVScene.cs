/*!	@file
	@brief PluggableVR: ADVScene スコープ付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! ADVScene スコープ付随動作 
	internal class Scope_ADVScene: GameObjectScope
	{
		private Camera _prevcam;

		private bool _visible;
		public bool IsVisible
		{
			get { return _visible; }
			private set
			{
				if (_visible == value) return;
				_visible = value;

				if (value)
				{
					Global.Logger.LogDebug(ToString() + " bgn");

					// 元カメラ退避 
					var mng = VRManager.Instance;
					_prevcam = (mng.Camera == null) ? null : mng.Camera.Source.Target;
					if (_prevcam != null) mng.Camera.Recall();

					// メインカメラ捕捉 
					var player = mng.Player;
					player.SetCamera(Camera.main);
					player.Camera.BePassive();

					// メインカメラから移設するComponent 
					var cam = player.Camera;
					cam.Possess<FlareLayer>();
//					cam.Possess<AmplifyColorEffect>();
//					cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
//					cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
//					cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

					// アバター表示Layerをカメラの表示対象内で選択 
					mng.Avatar.SetLayer(4);
				}
				else
				{
					Global.Logger.LogDebug(ToString() + " end");

					// 移設Component返却 
					var mng = VRManager.Instance;
					mng.Camera.Recall();

					// 元カメラに戻す 
					var player = mng.Player;
					player.SetCamera(_prevcam);
				}
			}
		}

		protected override void OnTerminate()
		{
			base.OnTerminate();

			IsVisible = false;
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			IsVisible = Target.activeInHierarchy;
		}
	}
}
