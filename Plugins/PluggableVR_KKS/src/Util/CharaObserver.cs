/*!	@file
	@brief PluggableVR: キャラ監視 
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
	internal class CharaObserver : ComponentScope<ChaControl>
	{
		private GameObjectList _acce = new GameObjectList();
		private GameObjectList _cloth = new GameObjectList();
		private GameObjectList _hair = new GameObjectList();

		protected override void OnStart()
		{
			Global.Logger.LogDebug(Target.fileParam.fullname + " found");
			base.OnStart();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(Target.fileParam.fullname + " gone");
			base.OnTerminate();
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			if (Target.sex != 0)
			{
				_updateAcce();
				_updateCloth();
				_updateHair();
			}
		}

		private void _updateAcce()
		{
			for (var i = 0; i < Target.objAccessory.Length; ++i)
			{
				var o = Target.objAccessory[i];
				var t = _acce[i];
				if (t == null && o != null) _acce[i] = t = new AcceObserver();
				if (t != null) t.Replace(o);
			}
		}
		private void _updateCloth()
		{
			for (var i = 0; i < Target.objClothes.Length; ++i)
			{
				var o = Target.objClothes[i];
				var t = _cloth[i];
				if (t == null && o != null) _cloth[i] = t = new ClothObserver();
				if (t != null) t.Replace(o);
			}
		}

		private void _updateHair()
		{
			for (var i = 0; i < Target.objHair.Length; ++i)
			{
				var o = Target.objHair[i];
				var t = _hair[i];
				if (t == null && o != null) _hair[i] = t = new HairObserver();
				if (t != null) t.Replace(o);
			}
		}
	}
}
