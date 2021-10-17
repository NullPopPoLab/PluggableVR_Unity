using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using PluggableVR;
using System;

namespace PluggableVR_CS2
{
    [BepInPlugin(GUID, "PluggableVR.CS2", VERSION)]
    [BepInProcess("CharaStudio")]
    public class Main : BaseUnityPlugin
    {
		public const string GUID = "com.nullpoppo.PluggableVR.CS2";
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
//      	Logger.LogInfo("* Scene: "+scn.name);
//			Logger.LogInfo("* Objs:");
//			Hierarchy.Dump((o,a)=>{
//				Logger.LogInfo(new string('\t',a.Count)+o.name);
//			});
		}
    }
}
