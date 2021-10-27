/*!	@file
	@brief PluggableVR: 手順遷移 会話シーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 会話シーン 準備 
	public class Flow_ADV_Prepare : Flow_Switch_Prepare
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

			if (CurScene != Global.LastLoadedScene)
			{
				return new Flow_ADV_Start();
			}
			return null;
		}
	}

	//! 手順遷移 会話シーン 開始 
	public class Flow_ADV_Start : Flow_Switch_Start
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

			if (CameraLoc == Loc.FromWorldTransform(CameraRoot)) return null;
			UpdateCameraParam();

			return new Flow_ADV_Main();
		}
	}

	//! 手順遷移 会話シーン 本体 
	public class Flow_ADV_Main : Flow_Switch_Main
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