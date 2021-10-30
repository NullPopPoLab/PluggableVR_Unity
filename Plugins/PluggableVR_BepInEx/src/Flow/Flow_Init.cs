/*!	@file
	@brief PluggableVR: 手順遷移 初期シーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 初期シーン 
	internal class Flow_Init : Flow_Common
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// 元のAudioListener無効化 
			// しようと思ったらここでとれない謎 
//			GameObject.Find("/Sound/Listener").GetComponent<AudioListener>().enabled = false;
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// タイトル画面遷移待ち 
			if (Global.LastLoadedScene != "Title") return null;

			return new Flow_Title();
		}
	}
}