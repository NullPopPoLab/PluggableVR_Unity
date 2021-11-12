/*!	@file
	@brief PluggableVR: グローバル情報群 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using BepInEx;
using NullPopPoSpecial;
using PluggableVR;
using System;
using System.Collections.Generic;

namespace PluggableVR_KKS
{
	internal struct DemoAvatarExtra{
		internal DynamicBoneCollider HeadCollider;
		internal DynamicBoneCollider LeftHandCollider;
		internal DynamicBoneCollider RightHandCollider;
	}

	internal static class Global
	{
		internal static string ProcessName;
		internal static BepInEx.Logging.ManualLogSource Logger;

		private static VRManager _vrmng;
		private static Dictionary<string, Func<SceneScopeBase>> _launcher = new Dictionary<string, Func<SceneScopeBase>>();

		private static RelativeBool _push_rbtn2 = new RelativeBool();

		internal static DemoAvatarExtra DemoAvatarExtra;

		internal static void Enable()
		{
			SceneInfo.Enable(new SceneLauncher());

			_vrmng = new VRManager();
			_vrmng.Initialize();
		}

		internal static void Disable()
		{
			SceneInfo.Disable();
			_launcher.Clear();
		}

		internal static void FixedUpdate()
		{
			_vrmng.FixedUpdate();
		}

		internal static void Update()
		{
			_vrmng.Update();

			var inp = VRManager.Input;
			_push_rbtn2.Update(inp.HandRight.IsButton2Pressed());
			if (inp.HandLeft.IsButton2Pressed() && _push_rbtn2.Delta > 0)
			{
				HierarchyDumper.Dumper.Dump2File("Hier_" + Paths.ProcessName);
			}
		}

		internal static void LateUpdate()
		{
			_vrmng.LateUpdate();
		}
	}
}
