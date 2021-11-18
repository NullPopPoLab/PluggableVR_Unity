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

namespace PluggableVR_EC
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
		private static Scenewalk _scenes;

		private static RelativeBool _push_rbtn2 = new RelativeBool();

		internal static DemoAvatarExtra DemoAvatarExtra;

		internal static void Enable()
		{
			_scenes = new Scenewalk();
			SceneInfo.Enable(_scenes);

			_vrmng = new VRManager();
			_vrmng.Initialize();
		}

		internal static void Disable()
		{
			_scenes = null;
			SceneInfo.Disable();
		}

		internal static void FixedUpdate()
		{
			_vrmng.FixedUpdate();
		}

		internal static void Update()
		{
			_scenes.Update();
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
