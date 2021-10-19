﻿/*!	@file
	@brief PluggableVR: プラグイン起動部 
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

namespace PluggableVR.CS
{
	[BepInPlugin(GUID, "PluggableVR.CS", VERSION)]
	[BepInProcess("CharaStudio")]
	public class Main : BaseUnityPlugin
	{
		public const string GUID = "com.nullpoppo.PluggableVR.CS";
		public const string VERSION = "0.0.2.0";

		public static Main Instance;
		public static bool Enabled{ get; private set; }

		internal static BepInEx.Logging.ManualLogSource PlugLog { get; private set; }
		internal static VRManager VRManager{ get; private set; }

		protected void Awake()
		{
			Instance = this;

			Enabled = VRSettings.enabled;
			if (!Enabled) return;

			PlugLog = Logger;
			VRManager = new VRManager();
			VRManager.Initialize();
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
			VRManager.SceneChanged(scn);

//			Hierarchy.Dump2File("Hierarchy", "SceneChanged");
		}

		protected void FixedUpdate()
		{
			if (!Enabled) return;

			VRManager.FixedUpdate();
		}

		protected void Update()
		{
			if (!Enabled) return;

			VRManager.Update();
		}

		protected void LateUpdate()
		{
			if (!Enabled) return;

			VRManager.LateUpdate();
		}
	}
}