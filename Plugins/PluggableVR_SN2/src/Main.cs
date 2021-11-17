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

namespace PluggableVR_SN2
{
    [BepInPlugin(GUID, "PluggableVR_SN2", VERSION)]
    [BepInProcess("StudioNEOV2")]
    public class Main : BaseUnityPlugin
    {
		public const string GUID = "com.nullpoppo.PluggableVR.SN2";
		public const string VERSION = "0.0.4.1";

		public static Main Instance;
		public static bool Enabled { get; private set; }

		protected void Awake()
		{
			Instance = this;

			Enabled = XRSettings.enabled;
			if (!Enabled) return;

			Global.ProcessName = Paths.ProcessName;
			Global.Logger = Logger;

			VRCamera.Revision = VRCamera.ERevision.Legacy;
			VRCamera.SourceMode = VRCamera.ESourceMode.Disabled;

			Dumper.Register();

			Harmony.CreateAndPatchAll(typeof(Main));
		}

		protected void OnEnable()
		{
			if (!Enabled) return;
			Global.Enable();
		}

		protected void OnDisable()
		{
			if (!Enabled) return;
			Global.Disable();
		}

		protected void FixedUpdate()
		{
			if (!Enabled) return;
			Global.FixedUpdate();
		}

		protected void Update()
		{
			if (!Enabled) return;
			Global.Update();
		}

		protected void LateUpdate()
		{
			if (!Enabled) return;
			Global.LateUpdate();
		}
    }
}
