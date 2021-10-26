/*!	@file
	@brief PluggableVR: グローバル情報群 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using PluggableVR;
using System;

namespace PluggableVR.HS2
{
	internal static class Global
	{
		internal static BepInEx.Logging.ManualLogSource Logger;
		internal static string LastLoadedScene;

		internal static Camera MainCamera;
	}
}
