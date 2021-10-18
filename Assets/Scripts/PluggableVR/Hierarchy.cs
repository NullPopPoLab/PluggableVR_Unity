/*!	@file
	@brief Hierarchy: Hierarchy操作機能群 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.IO;
using System.Text;
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

		private static string _fmtFloat(float v)
		{
			var s = (v < 0.0f);
			if (s) v = -v;
			return (s ? "-" : "+") + v.ToString("F3");
		}

		private static string _fmtVector(Vector3 v)
		{
			return "(" + _fmtFloat(v.x) + "," + _fmtFloat(v.y) + "," + _fmtFloat(v.z) + ")";
		}

		private static void _dump2File(FileStream fs, GameObject obj, IList<GameObject> anc)
		{

			var il = anc.Count;
			var s = "";

			var t1 = "";
			var t2 = "";
			var t3 = "";
			var t4 = "";
			var t5 = "";
			var t6 = "";
			for (var i = 0; i < il - 2; ++i) t1 += " |  ";
			t2 = t1;
			if (il > 0)
			{
				t1 += " +--";
				t2 += " |  ";
			}
			t6 = t5 = t4 = t3 = t2;
			t2 += " | ";
			t3 += " |~";
			t4 += " |  ; ";
			t5 += " |";
			t6 += " `";

			s += t1 + (obj.activeSelf ? "[*]" : "[_]");
			if (obj.isStatic) s += "[S]";
			s += " " + obj.name;
			s += "\n";

			s += t2 + "Scene: " + obj.scene.name + "\n";
			s += t2 + "InstanceID: " + obj.GetInstanceID() + "\n";
			s += t2 + "Layer: " + obj.layer + "\n";
			s += t2 + "Tag: " + obj.tag + "\n";

			var tr = obj.transform;
			s += t2 + "Pos: " + _fmtVector(tr.position) + "\n";
			var r = tr.rotation;
			s += t2 + "RtX: " + _fmtVector(RotUt.AxisX(r)) + "\n";
			s += t2 + "RtY: " + _fmtVector(RotUt.AxisY(r)) + "\n";
			s += t2 + "RtZ: " + _fmtVector(RotUt.AxisZ(r)) + "\n";

			var cs = obj.GetComponents(typeof(Component));
			il = cs.Length;
			for (var i = 0; i < il; ++i)
			{
				var c = cs[i];
				var b = c as Behaviour;
				s += t3;
				if (b == null) s += "{@} ";
				else s += b.enabled ? "{*} " : "{_} ";

				var cn = c.ToString();
				var ol = obj.name.Length;
				if (cn.Substring(cn.Length - 1) != ")") { }
				else if (cn.Substring(0, ol + 2) != obj.name + " (") { }
				else cn = cn.Substring(ol + 2, cn.Length - ol - 3);
				s += cn + "\n";

				s += t4 + "InstanceID: " + c.GetInstanceID() + "\n";
			}

			if (tr.childCount < 1) s += t6 + "\n";

			var bin = new UTF8Encoding(true).GetBytes(s);
			fs.Write(bin, 0, bin.Length);
		}

		//! GameObject 全列挙してファイルに書き出す 
		public static void Dump2File(string prefix, string suffix = "")
		{
			string path;
			do
			{
				path = prefix + DateTime.Now.ToString("_yyymmdd_HHmmss_fff");
				if (suffix != "") path += "_" + suffix;
				path += ".log";
			} while (File.Exists(path));

			var fs = new FileStream(path, FileMode.Create);
			fs.Close();
			fs = new FileStream(path, FileMode.Truncate);
			Dump((obj, anc) => _dump2File(fs, obj, anc));
			fs.Close();
		}
	}
}
