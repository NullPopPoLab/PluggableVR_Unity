/*!	@file
	@brief PluggableVR: 手順遷移 えっちシーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 えっちシーン 準備 
	public class Flow_HScene_Prepare : Flow_Switch_Prepare
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
				return new Flow_HScene_Start();
			}
			return null;
		}
	}

	//! 手順遷移 えっちシーン 開始 
	public class Flow_HScene_Start : Flow_Switch_Start
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
			return new Flow_HScene_Main();
		}
	}

	//! 手順遷移 えっちシーン 本体 
	public class Flow_HScene_Main : Flow_Switch_Main
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