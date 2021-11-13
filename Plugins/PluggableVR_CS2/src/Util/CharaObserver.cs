/*!	@file
	@brief HierarchyDumper: キャラ監視 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System.Collections.Generic;
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;
using PluggableVR;

namespace PluggableVR_CS2
{
	//! キャラ監視 
	internal class CharaObserver : GameObjectScope
	{
		private ChaControl _cc;
		private GameObjectList _acce = new GameObjectList();
		private GameObjectList _cloth = new GameObjectList();
		private GameObjectList _hair = new GameObjectList();

		protected override void OnStart()
		{
			_cc = Target.GetComponent<ChaControl>();
			Global.Logger.LogDebug(_cc.fileParam.fullname + " found");
			base.OnStart();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(_cc.fileParam.fullname + " gone");
			base.OnTerminate();
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			if (_cc.sex != 0)
			{
				_updateAcce();
				_updateCloth();
				_updateHair();
			}
		}

		private void _updateAcce()
		{
			for (var i = 0; i < _cc.objAccessory.Length; ++i)
			{
				var o = _cc.objAccessory[i];
				var t = _acce[i];
				if (t == null && o != null) _acce[i] = t = new AcceObserver();
				if (t != null) t.Replace(o);
			}
		}
		private void _updateCloth()
		{
			for (var i = 0; i < _cc.objClothes.Length; ++i)
			{
				var o = _cc.objClothes[i];
				var t = _cloth[i];
				if (t == null && o != null) _cloth[i] = t = new ClothObserver();
				if (t != null) t.Replace(o);
			}
		}

		private void _updateHair()
		{
			for (var i = 0; i < _cc.objHair.Length; ++i)
			{
				var o = _cc.objHair[i];
				var t = _hair[i];
				if (t == null && o != null) _hair[i] = t = new HairObserver();
				if (t != null) t.Replace(o);
			}
		}
	}
}
