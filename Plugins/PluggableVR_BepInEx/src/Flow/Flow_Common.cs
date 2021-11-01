/*!	@file
	@brief PluggableVR: 手順遷移 シーン切り替え共通部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.KK
{
	//! 手順遷移 共通機能
	internal class Flow_Common : Flow
	{
		internal ChangingCensor<string> LastLoadedScene;

		protected override void OnStart()
		{
			base.OnStart();

//			LastLoadedScene.Reset(Global.LastLoadedScene);
		}

		//! 通常のシーン切り替え遷移 
		internal Flow StepScene(){

#if false
			var scn = Global.LastLoadedScene;
			if (!LastLoadedScene.Update(scn)) return null;

			if (!Global.Transit.ContainsKey(scn)) return null;
			return Global.Transit[scn]();
#endif
			return null;
		}
	}
}