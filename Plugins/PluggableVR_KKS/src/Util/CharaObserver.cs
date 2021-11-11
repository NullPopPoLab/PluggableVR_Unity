﻿/*!	@file
	@brief HierarchyDumper: キャラ監視 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System.Collections.Generic;
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! キャラ監視 
	internal class CharaObserver
	{
		internal GameObject Target { get; private set; }

		internal CharaObserver(GameObject target)
		{
			Target = target;
		}

		internal void AddPlayerColliders()
		{
			if (Target == null) return;
			var db = Target.GetComponentsInChildren<DynamicBone>();

//			Global.Logger.LogDebug(""+ db.Length+" DynamicBones found");

			for (var i = 0; i < db.Length; ++i)
			{
				if (db[i].m_Colliders == null) continue;
				db[i].m_Colliders.Add(Global.DemoAvatarExtra.HeadCollider);
				db[i].m_Colliders.Add(Global.DemoAvatarExtra.LeftHandCollider);
				db[i].m_Colliders.Add(Global.DemoAvatarExtra.RightHandCollider);
			}
		}

		internal void RemovePlayerColliders()
		{
			if (Target == null) return;
			var db = Target.GetComponentsInChildren<DynamicBone>();

			for (var i = 0; i < db.Length; ++i)
			{
				if (db[i].m_Colliders == null) continue;
				db[i].m_Colliders.Remove(Global.DemoAvatarExtra.HeadCollider);
				db[i].m_Colliders.Remove(Global.DemoAvatarExtra.LeftHandCollider);
				db[i].m_Colliders.Remove(Global.DemoAvatarExtra.RightHandCollider);
			}
		}
	}
}