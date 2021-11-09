/*!	@file
	@brief PluggableVR: 手順遷移 夜メニュー 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;
using ActionGame;

namespace PluggableVR_KKS
{
	//! 手順遷移 夜メニュー 
	internal class Flow_HomeMenu : Flow_Common
	{
		private Flow _prev;
		private HomeMenu _homemenu;

		public Flow_HomeMenu(Flow prev){
			_prev = prev;
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			_homemenu = GameObject.Find("/ActionScene/UI/HomeMenu").GetComponent<HomeMenu>();
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
			if (!_homemenu.visible) return new Flow_Delay(_prev);

			// メインカメラ位置更新 
			var mng = VRManager.Instance;
			mng.Camera.Feedback();

			return null;
		}
	}
}