﻿/*!	@file
	@brief PluggableVR: Title シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_EC
{
	//! Title シーン付随動作 
	internal class Scene_Title : SceneScope
	{
		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();
		}
	}
}