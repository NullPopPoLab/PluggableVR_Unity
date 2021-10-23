/*!	@file
	@brief HierarchyDumper: プラグイン起動部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Studio;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.SceneManagement;
using PluggableVR;
using System;

namespace HierarchyDumper
{
	[BepInPlugin(GUID, "HierarchyDumper", VERSION)]
	public class Main : BaseUnityPlugin
	{
		public const string GUID = "com.nullpoppo.HierarchyDumper";
		public const string VERSION = "0.0.2.3";

		public static Main Instance;

		private VRManager _vrmng;

		private RelativeBool _push_rbtn2 = new RelativeBool();

		protected void Awake()
		{
			Instance = this;
			Harmony.CreateAndPatchAll(typeof(Main));
		}

		protected void Update()
		{
#if false
			var inp = VRManager.Input;
			_push_rbtn2.Update(inp.HandRight.IsButton2Pressed());
			if (inp.HandLeft.IsButton2Pressed() && _push_rbtn2.Delta > 0)
			{
				Hierarchy.Dump2File("Hierarchy");
			}
#endif
		}
	}
}
