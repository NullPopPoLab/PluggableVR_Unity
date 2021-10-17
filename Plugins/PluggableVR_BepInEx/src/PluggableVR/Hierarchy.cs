/*!	@file
	@brief Hierarchy: Hierarchy操作機能群 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableVR
{
	//! Hierarchy操作機能群 
	public static class Hierarchy
	{
		//! 見つかったオブジェクトの報告 
		/*!	@param obj 見つかったオブジェクト
			@param ancestral 先祖代々のオブジェクト群
		*/
		public delegate void CB_FoundObject(GameObject obj, IList<GameObject> ancestral);

		private static void _dumpChildren(GameObject obj, List<GameObject> a, CB_FoundObject cb)
		{
			cb(obj, a);
			a.Add(obj);
			foreach (Transform ct in obj.transform) _dumpChildren(ct.gameObject, a, cb);
			a.RemoveAt(a.Count - 1);
		}

		//! GameObject 全列挙 
		/*!	@note Editorで表示されない隠しオブジェクトも含まれる
		*/
		public static void Dump(CB_FoundObject cb)
		{
			if (cb == null) return;

			var a = new List<GameObject>();
			foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
			{
				var t = obj.transform;
				if (t == null) continue;
				if (t.parent != null) continue;
				_dumpChildren(obj, a, cb);
			}
		}
	}
}
