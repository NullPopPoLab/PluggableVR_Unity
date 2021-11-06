/*!	@file
	@brief PluggableVR: 手順遷移 移動シーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! 手順遷移 移動シーン 
	internal class Flow_InLevel : Flow_Common
	{
		private Flow _prev;
		private Canvas _minimap2d;

		public Flow_InLevel(Flow prev){
			_prev = prev;
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			_minimap2d = GameObject.Find("/ActionScene/UI/Minimap/MiniMapCircle/MiniMapCanvas2D").GetComponent<Canvas>();
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
			// 移動終了検知 
			if (!_minimap2d.enabled) return new Flow_Delay(_prev);

			// メインカメラ位置更新 
			var mng = VRManager.Instance;
			mng.Camera.Feedback();

			return null;
		}
	}
}