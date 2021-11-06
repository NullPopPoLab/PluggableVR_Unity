/*!	@file
	@brief PluggableVR: 手順遷移 夜メニュー 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! 手順遷移 夜メニュー 
	internal class Flow_NightMenu : Flow_Common
	{
		private Flow _prev;
		public Flow_NightMenu(Flow prev){
			_prev = prev;
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
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

			// 脱出検知 
			if (!Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/Action.unity").isLoaded) return new Flow_Delay(new Flow_Title());
			// 夜メニュー終了検知 
			if (!Global.Scene.GetSceneInfo("Assets/Illusion/assetbundle/action/menu/Menu/NightMenu.unity").isLoaded) return new Flow_Delay(_prev);

			// メインカメラ位置更新 
			var mng = VRManager.Instance;
			mng.Camera.Feedback();

			return null;
		}
	}
}