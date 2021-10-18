using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Studio;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.SceneManagement;
using PluggableVR;
using System;

namespace PluggableVR_CS
{
    [BepInPlugin(GUID, "PluggableVR.CS", VERSION)]
    [BepInProcess("CharaStudio")]
    public class Main : BaseUnityPlugin
    {
        public const string GUID = "com.nullpoppo.PluggableVR.CS";
        public const string VERSION = "0.0.1.1";

        public static bool Enabled{ get; private set; }

        protected void Awake()
        {
            Enabled = VRSettings.enabled;
            if (!Enabled) return;

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
            Logger.LogInfo("* Scene: " + scn.name);

//            Hierarchy.Dump2File("Hierarchy", scn.name);
        }
    }
}
