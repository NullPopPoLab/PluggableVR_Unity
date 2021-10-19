/*!	@file
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
using PluggableVR;
using System;

namespace PluggableVR.SN2
{
    [BepInPlugin(GUID, "PluggableVR.SN2", VERSION)]
    [BepInProcess("StudioNEOV2")]
    public class Main : BaseUnityPlugin
    {
		public const string GUID = "com.nullpoppo.PluggableVR.SN2";
		public const string VERSION = "0.0.2.0";

		public static Main Instance;
		public static bool Enabled{ get; private set; }

		internal static BepInEx.Logging.ManualLogSource PlugLog { get; private set; }
		internal static VRManager VRManager{ get; private set; }

		private RelativeBool _push_rbtn2 = new RelativeBool();

		protected void Awake()
		{
			Instance = this;

			Enabled = XRSettings.enabled;
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

			var inp = VRManager.Input;
			_push_rbtn2.Update(inp.HandRight.IsButton2Pressed());
			if (inp.HandLeft.IsButton2Pressed() && _push_rbtn2.Delta > 0)
			{
				Hierarchy.Dump2File("Hierarchy");
			}
		}

		protected void LateUpdate()
		{
			if (!Enabled) return;

			VRManager.LateUpdate();
		}
    }
}
