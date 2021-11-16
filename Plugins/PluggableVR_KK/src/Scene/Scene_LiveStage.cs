/*!	@file
	@brief PluggableVR: LiveStage シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! LiveStage シーン付随動作 
	internal class Scene_LiveStage : SceneScope
	{
		private CharaObserver _chara = new CharaObserver();
		private ShowCamera _showCamera = new ShowCamera();

		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			// メインカメラ捕捉 
			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(Camera.main);
			player.Camera.BeActive();

			// 元のカメラから移設するComponent 
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

			// キャラ状態捕捉 
			_chara.Start("/chaF_001");

			// 開演待ち 
			_showCamera.Start("/chaF_001/BodyTop/p_cf_body_bone/cf_j_root/cf_n_height/CameraObject(Clone)/n_adjust/n_cam/Camera");
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			_chara.Update();
			_showCamera.Update();
		}

		internal class ShowCamera : ComponentScope<Camera>
		{
			private Camera _prevcam;
			private bool _ready;
			public bool IsReady
			{
				get { return _ready; }
				private set
				{
					if (_ready == value) return;
					_ready = value;

					if (value)
					{
						Global.Logger.LogDebug(ToString() + " bgn");

						// 元カメラ退避 
						var mng = VRManager.Instance;
						_prevcam = (mng.Camera == null) ? null : mng.Camera.Source.Target;
						if (_prevcam != null) mng.Camera.Recall();

						// メインカメラ捕捉 
						var player = mng.Player;
						player.SetCamera(Target);
						player.Camera.BeActive();
						player.Reloc(Loc.FromWorldTransform(_prevcam.transform));

						// 元のカメラから移設するComponent 
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

						// アバター表示Layerをカメラの表示対象内で選択 
						mng.Avatar.SetLayer(0);
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

				IsReady = false;
			}

			protected override void OnUpdate()
			{
				base.OnUpdate();

				IsReady = GameObject.activeInHierarchy;
			}
		}
	}
}
