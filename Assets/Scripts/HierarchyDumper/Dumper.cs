/*!	@file
	@brief HierarchyDumper: Hierarchy 構造書き出し 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchyDumper
{
	//! Hierarchy 構造書き出し 
	public static class Dumper
	{
		//! 見つかったオブジェクトの報告 
		/*!	@param obj 見つかったオブジェクト
			@param ancestral 先祖代々のオブジェクト群
			@return 子オブジェクトの探索も行う
		*/
		public delegate bool CB_FoundObject(GameObject obj, IList<GameObject> ancestral);

		private static void _dumpChildren(GameObject obj, List<GameObject> a, CB_FoundObject cb)
		{
			if (!cb(obj, a)) return;
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

		//! GameObject 全列挙してファイルに書き出す 
		public static void Dump2File(string prefix, string suffix = "")
		{
			string path;
			do
			{
				path = prefix + DateTime.Now.ToString("_yyMMdd_HHmmss_fff");
				if (suffix != "") path += "_" + suffix;
				path += ".log";
			} while (File.Exists(path));

			var fs = new FileStream(path, FileMode.Create);
			fs.Close();
			fs = new FileStream(path, FileMode.Truncate);
			Dump((obj, anc) => {
				var txt = new Dumper_GameObject(obj).Dump();
				var bin = new UTF8Encoding(true).GetBytes(txt);
				fs.Write(bin, 0, bin.Length);
				return false;
			});
			fs.Close();
		}
	}
}
