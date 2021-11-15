/*!	@file
	@brief PluggableVR: Action シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! Action シーン付随動作 
	internal class Scene_Action : SceneScope
	{
#if false
		private Scope_NowLoading _loading = new Scope_NowLoading();
		private Scope_ADVScene _adv = new Scope_ADVScene();
		private Scope_HomeMenu _home = new Scope_HomeMenu();
		private Scope_ClassRoomList _class = new Scope_ClassRoomList();
		private Scope_Player _player = new Scope_Player();
		private WorldScope _world = new WorldScope();
//		private ComponentScope<Camera> _minimap = new ComponentScope<Camera>();

		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			// メインカメラ捕捉 
			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(Camera.main);
			player.Camera.BePassive();

			// メインカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
//			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
//			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
//			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();
//			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// メインカメラから遮断するComponent 
//			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
//			cam.Suppress<AmplifyColorEffect>();
//			cam.Suppress<AmplifyOcclusionEffect>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);

			// サブスコープ群 
			_loading.Start("/Manager(Clone)/SceneCanvas");
			_adv.Start("/ActionScene/ADVScene");
			_home.Start("/ActionScene/UI/HomeMenu");
			_class.Start("/EntryLive(Clone)/ClassroomCanvas");
			_player.Start("/ActionScene/Player");
//			_minimap.Start("/ActionScene/UI/Minimap/MinimapCamera");

			_world.Start("/ActionScene");
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			// 常に処理対象 
			_loading.Update();
			_adv.Update();

			// サブシーン動作中は他を処理しない 
			var f = _loading.IsVisible || _adv.IsVisible||
				SceneInfo.IsAvailable("Assets/Illusion/Game/Scene/EstheticScene.unity") ||
				SceneInfo.IsAvailable("Assets/Illusion/Game/Scene/H.unity") ||
				SceneInfo.IsAvailable("Assets/Illusion/Game/Scripts/Scene/Custom/CustomScene.unity");

			// メニュー類 
			_home.Interrupt = f; _home.Update();
			_class.Interrupt = f; _class.Update();
			f |= _home.IsVisible || _class.IsVisible;

			// キャラ入退場検知 
			if (!f || _adv.IsVisible) _world.Update();

			// プレイヤー移動 
			_player.Interrupt = f;
			_player.Update();

			// 元カメラ復活問題検出 
			var mng = VRManager.Instance;
			var cam = mng.Camera.Source.Target;
			if (cam.cullingMask != 0)
			{
				Global.Logger.LogWarning("Source camera cullingMask: " + cam.cullingMask);
				cam.cullingMask = 0;
			}
			if (cam.clearFlags != CameraClearFlags.Nothing)
			{
				Global.Logger.LogWarning("Source camera clearFlags: " + cam.clearFlags);
				cam.clearFlags = CameraClearFlags.Nothing;
			}
		}
#endif
	}
}
