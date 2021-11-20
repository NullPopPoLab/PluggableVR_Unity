/*!	@file
	@brief PluggableVR: Canvas スコープ付随動作 共通部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_EC
{
	//! Canvas スコープ付随動作 共通部 
	internal class Scope_Canvas : ComponentScope<Canvas>
	{
		private bool _sortingOrderSig;
		private int _sortingOrderVal;
		public int SortingOrder{
			get{
				if (_sortingOrderSig) return _sortingOrderVal;
				if (Target == null) return 0;
				return Target.sortingLayerID;
			}
			set{
				_sortingOrderVal = value;
				_sortingOrderSig = true;
			}
		}

		protected override void OnStart()
		{
			base.OnStart();
			Global.Logger.LogDebug(Path + " bgn");
		}

		protected override void OnTerminate()
		{
			base.OnTerminate();
			Global.Logger.LogDebug(Path + " end");
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			if (Target.renderMode == RenderMode.ScreenSpaceCamera){
				Target.renderMode = RenderMode.ScreenSpaceOverlay;
				Global.Logger.LogDebug(Path + " be Overlay");
			}
			if(_sortingOrderSig){
				_sortingOrderSig = false;
				Target.sortingOrder = _sortingOrderVal;
			}
		}
	}
}
