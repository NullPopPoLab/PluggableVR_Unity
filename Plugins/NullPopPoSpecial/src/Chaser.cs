/*!	@file
	@brief NullPopPoSpecial: 位置追跡 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;

namespace NullPopPoSpecial
{
	//! 位置追跡 
	public class Chaser
	{
		public Loc Loc { get; private set; }
		public int Staying { get; private set; }
		public bool IsMoving { get { return Staying <= _stayThreshold; } }

		private Loc _loc;
		private Transform _target;
		private int _stayThreshold;

		public Chaser(Transform target = null, int staythreshold = 1)
		{
			_stayThreshold = staythreshold;
			Reset(target);
		}

		public void Reset(Transform target)
		{
			Staying = _stayThreshold+1;
			_target = target;
			Loc = _loc = (target == null) ? Loc.Identity : Loc.FromWorldTransform(target);
		}

		public bool Update()
		{
			if (_target == null) return false;
			var f = false;
			var loc = Loc.FromWorldTransform(_target);

			if (Loc.CompareLoose(loc, _loc))
			{
				var m = IsMoving;
				++Staying;
				// 動きが止まったときのみ位置反映 
				if (m) { Loc = loc; f = true; }
			}
			else
			{
				// 動き始めたときのみ位置反映 
				if (Staying > 0) { Loc = loc; f = true; }
				Staying = 0;
			}

			_loc = loc;
			return f;
		}
	}
}
