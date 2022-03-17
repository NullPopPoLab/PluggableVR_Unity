/*!	@file
	@brief HierarchyDumper: プラグイン起動部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using NullPopPoSpecial;

namespace HierarchyDumper
{
	[BepInPlugin(GUID, "HierarchyDumper", VERSION)]
	public class Main : BaseUnityPlugin
	{
		public const string GUID = "com.nullpoppo.HierarchyDumper";
		public const string VERSION = "0.1.0.1";

		public static Main Instance;

		private ConfigEntry<KeyboardShortcut> _dumpTrigger;

		protected void Awake()
		{
			Instance = this;

			_dumpTrigger = Config.Bind("Keyboard Shortcuts", "Dump the Hierarchy", new KeyboardShortcut(KeyCode.P, KeyCode.RightAlt, KeyCode.AltGr), new ConfigDescription("Dump GameObject info to File"));

			Harmony.CreateAndPatchAll(typeof(Main));
		}

		protected void Update()
		{
			if (_dumpTrigger.Value.IsDown())
			{
				Dumper.Dump2File("Hier_"+Paths.ProcessName);
			}
		}
	}
}
