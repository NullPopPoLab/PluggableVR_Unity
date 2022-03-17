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

namespace PluggableVR_KK
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
		}

		internal static void LateUpdate()
		{
			_vrmng.LateUpdate();
		}
	}
}
