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
		public const string VERSION = "0.0.5.1";

		public static Main Instance;

		private RelativeBool _push_rbtn2 = new RelativeBool();

		protected void Awake()
		{
			Instance = this;
			Harmony.CreateAndPatchAll(typeof(Main));
		}

		protected void Update()
		{
			if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.P))
			{
				Dumper.Dump2File("Hier_"+Paths.ProcessName);
			}
		}
	}
}
