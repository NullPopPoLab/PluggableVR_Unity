#if false
/*!	@file
	@brief PluggableVR: 手順遷移 シーン切り替え共通部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR_KKS
{
	//! 手順遷移 共通機能
	internal class Flow_Common : Flow
	{
		internal ChangingCensor<string> CurrentScene;

		protected override void OnStart()
		{
			base.OnStart();

			CurrentScene.Reset(Global.Scene.ActiveScene);
		}

		//! 通常のシーン切り替え遷移 
		internal Flow StepScene(){

			var path = Global.Scene.ActiveScene;
			if (!CurrentScene.Update(path)) return null;

			var scn = Global.Scene.GetSceneInfo(path);
			var n = scn.name;
			if (!Global.Transit.ContainsKey(n)) return null;
			return Global.Transit[n]();
		}
	}
}
#endif
