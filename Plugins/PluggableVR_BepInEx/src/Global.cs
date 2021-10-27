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

namespace PluggableVR.HS2
{
	internal static class Global
	{
		internal static BepInEx.Logging.ManualLogSource Logger;
		internal static string LastLoadedScene;

		internal static Camera MainCamera;
		internal static Dictionary<string, Func<Flow>> Transit = new Dictionary<string, Func<Flow>>();
	}
}
