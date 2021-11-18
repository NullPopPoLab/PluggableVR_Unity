/*!	@file
	@brief HierarchyDumper: パーツ監視 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System.Collections.Generic;
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;
using PluggableVR;

namespace PluggableVR_HS2
{
	//! パーツ監視 
	internal class PartObserver: GameObjectScope
	{
		protected override void OnStart()
		{
//			Global.Logger.LogDebug(Name + " found");
			base.OnStart();

			_addPlayerColliders();
		}

		protected override void OnTerminate()
		{
//			Global.Logger.LogDebug(Name + " gone");
			base.OnTerminate();
		}

		private void _addPlayerColliders()
		{
			if (Target == null) return;
			var db = Target.GetComponentsInChildren<DynamicBone>();

			for (var i = 0; i < db.Length; ++i)
			{
				if (db[i].m_Colliders == null) continue;
				db[i].m_Colliders.Add(Global.DemoAvatarExtra.HeadCollider);
				db[i].m_Colliders.Add(Global.DemoAvatarExtra.LeftHandCollider);
				db[i].m_Colliders.Add(Global.DemoAvatarExtra.RightHandCollider);
			}
		}

		private void _removePlayerColliders()
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
