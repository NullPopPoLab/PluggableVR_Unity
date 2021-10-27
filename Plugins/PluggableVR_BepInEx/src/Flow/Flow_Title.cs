/*!	@file
	@brief PluggableVR: 手順遷移 タイトル画面 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 タイトル画面 準備 
	public class Flow_Title_Prepare : Flow_Switch_Prepare
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

			if(CurScene!=Global.LastLoadedScene){
				return new Flow_Title_Start();
			}
			return null;
		}
	}

	//! 手順遷移 タイトル画面 開始 
	public class Flow_Title_Start : Flow_Switch_Start
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
			return new Flow_Title_Main();
		}
	}

	//! 手順遷移 タイトル画面 本体 
	public class Flow_Title_Main : Flow_Switch_Main
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
	}
}