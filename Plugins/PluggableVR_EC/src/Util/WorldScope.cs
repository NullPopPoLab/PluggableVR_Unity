/*!	@file
	@brief PluggableVR: キャラ探索 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;

namespace PluggableVR_EC
{
	//! キャラ探索 
	internal class WorldScope : GameObjectScope
	{
		private ComponentDic<int, ChaControl> _female = new ComponentDic<int, ChaControl>();
		private ComponentDic<int, ChaControl> _male = new ComponentDic<int, ChaControl>();

		protected override void OnStart()
		{
			base.OnStart();

			Global.Logger.LogDebug(Name + " found");
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(Name + " gone");

			base.OnTerminate();
		}

		protected override void OnUpdate()
		{
			var t = Target.GetComponentsInChildren<ChaControl>();
			for (var i = 0; i < t.Length; ++i)
			{
				var c = t[i];
				var d = (c.sex != 0) ? _female : _male;
				var id = c.gameObject.GetInstanceID();
				var v = d.Get(id);
				if (v != null) continue;
				v = new CharaObserver();
				v.Start(c);
				d.Set(id, v);
			}

			_female.Update();
			_male.Update();
			_female.Cleanup();
			_male.Cleanup();
		}
	}
}
