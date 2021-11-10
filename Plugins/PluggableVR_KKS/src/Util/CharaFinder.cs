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

namespace PluggableVR_KKS
{
	//! キャラ探索 
	internal class CharaFinder
	{
		internal bool Male { get; private set; }
		internal int From { get; private set; }
		internal int Next { get; private set; }
		internal List<CharaObserver> List { get; private set; }

		private string _prefix;

		internal CharaFinder(bool male, int from = 1)
		{
			List = new List<CharaObserver>();
			Male = male;
			From = Next = from;
			_prefix = "cha" + (male ? "M_" : "F_");
		}

		public void Reset(){
			List.Clear();
			Next = From;
		}

		public int Find(Action<CharaObserver> onfind=null)
		{
			var c = 0;
			while (true)
			{
				var n = _prefix + Next.ToString("D3");
				var obj = GameObject.Find(n);
				if (obj == null) break;

				var obs = new CharaObserver(obj);
				List.Add(obs);
				if (onfind != null) onfind(obs);
				++Next;
			}

			return c;
		}
	}
}
