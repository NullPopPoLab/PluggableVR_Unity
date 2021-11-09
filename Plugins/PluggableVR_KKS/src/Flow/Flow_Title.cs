/*!	@file
	@brief PluggableVR: 手順遷移 タイトルシーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! 手順遷移 タイトルシーン 
	internal class Flow_Title : Flow_Common
	{
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

			// えっち遷移検知 
			if (Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/H.unity").isLoaded)
				return new Flow_H(this);
			// キャラメイク遷移検知 
			if (Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scripts/Scene/Custom/CustomScene.unity").isLoaded)
				return new Flow_CustomScene(this);

			return base.StepScene();
		}
	}
}