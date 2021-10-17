using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Studio;
using UnityEngine;
using UnityEngine.SceneManagement;
using PluggableVR;
using System;

namespace PluggableVR_KK
{
    [BepInPlugin(GUID, "PluggableVR.KK", VERSION)]
    [BepInProcess("Koikatu")]
    public class Main : BaseUnityPlugin
    {
		public const string GUID = "com.nullpoppo.PluggableVR.KK";
		public const string VERSION = "0.0.1.0";

        protected void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(Main));
        }

        protected void OnEnable()
        {
            SceneManager.sceneLoaded += _onSceneChanged;
        }

        protected void OnDisable()
        {
            SceneManager.sceneLoaded -= _onSceneChanged;
        }

        private void _onSceneChanged(Scene scn, LoadSceneMode mode)
        {
            Logger.LogInfo("* Scene: "+scn.name);
		}
    }
}
