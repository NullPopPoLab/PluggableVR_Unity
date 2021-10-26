﻿/*!	@file
	@brief PluggableVR: プラグイン起動部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
    [BepInPlugin(GUID, "PluggableVR.HS2", VERSION)]
    [BepInProcess("HoneySelect2")]
    public class Main : BaseUnityPlugin
    {
		public const string GUID = "com.nullpoppo.PluggableVR.HS2";
		public const string VERSION = "0.0.3.1";

		public static Main Instance;
		public static bool Enabled { get; private set; }

		private VRManager _vrmng;

		private RelativeBool _push_rbtn2 = new RelativeBool();

		protected void Awake()
		{
			Instance = this;

			Enabled = XRSettings.enabled;
			if (!Enabled) return;

			Global.Logger = Logger;
			_vrmng = new VRManager();
			_vrmng.Initialize(new Flow_Startup());
			Harmony.CreateAndPatchAll(typeof(Main));
		}

		protected void OnEnable()
		{
			if (!Enabled) return;

			SceneManager.sceneLoaded += _onSceneChanged;
		}

		protected void OnDisable()
		{
			if (!Enabled) return;

			SceneManager.sceneLoaded -= _onSceneChanged;
		}

		private void _onSceneChanged(Scene scn, LoadSceneMode mode)
		{
//			Logger.LogInfo("Scene: "+scn.name);
			Global.LastLoadedScene = scn.name;
//			HierarchyDumper.Dumper.Dump2File("Hier_" + Paths.ProcessName, "Scene-"+scn.name);
		}

		protected void FixedUpdate()
		{
			if (!Enabled) return;

			_vrmng.FixedUpdate();
		}

		protected void Update()
		{
			if (!Enabled) return;

			_vrmng.Update();

			var inp = VRManager.Input;
			_push_rbtn2.Update(inp.HandRight.IsButton2Pressed());
			if (inp.HandLeft.IsButton2Pressed() && _push_rbtn2.Delta > 0)
			{
				HierarchyDumper.Dumper.Dump2File("Hier_" + Paths.ProcessName);
			}
		}

		protected void LateUpdate()
		{
			if (!Enabled) return;

			_vrmng.LateUpdate();
		}
    }
}
