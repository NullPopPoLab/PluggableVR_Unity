/*!	@file
	@brief PluggableVR: 手順遷移 えっち ローディング 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! 手順遷移 えっち ローディング 
	internal class Flow_H : Flow_Common
	{
		private Flow _prev;

		internal Flow_H(Flow prev)
		{
			_prev=prev;
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

			// ロード完了検知 
			if (Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/HProc.unity").isLoaded)
			{
				return new Flow_HProc(_prev);
			}

			return null;
		}
	}
}
