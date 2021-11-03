/*!	@file
	@brief PluggableVR: グローバル情報群 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;
using System;
using System.Collections.Generic;

namespace PluggableVR_KK
{
	internal static class Global
	{
		internal static string ProcessName;
		internal static BepInEx.Logging.ManualLogSource Logger;

		internal static SceneObserver Scene = new SceneObserver();

		internal static Dictionary<string, Func<Flow>> Transit = new Dictionary<string, Func<Flow>>();
	}
}
