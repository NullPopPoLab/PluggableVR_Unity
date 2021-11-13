/*!	@file
	@brief HierarchyDumper: キャラ探索 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;

namespace PluggableVR_CS2
{
	//! キャラ探索 
	internal class WorldScope : GameObjectScope
	{
		private GameObjectDic<int> _female = new GameObjectDic<int>();
		private GameObjectDic<int> _male = new GameObjectDic<int>();

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
				v.Start(c.gameObject);
				d.Set(id, v);
			}

			_female.Update();
			_male.Update();
			_female.Cleanup();
			_male.Cleanup();
		}
	}
}
