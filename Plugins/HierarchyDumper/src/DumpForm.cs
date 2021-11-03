/*!	@file
	@brief HierarchyDumper: Dump用書式 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using System;

namespace HierarchyDumper
{
	public class DumpForm
	{
		public const string TypeMismatch = "!!! Type Mismatch !!!\n";

		public static string From(float v)
		{
			var s = (v < 0.0f);
			if (s) v = -v;
			return (s ? "-" : "+") + v.ToString("F3");
		}

		public static string From(Vector2 v)
		{
			return "(" + From(v.x) + "," + From(v.y) + ")";
		}

		public static string From(Vector3 v)
		{
			return "(" + From(v.x) + "," + From(v.y) + "," + From(v.z) + ")";
		}

		public static string From(UnityEngine.Object v)
		{
			if (v == null) return "(null)";
			var t = v.GetType();
			var n = t.Name;
			if (!String.IsNullOrEmpty(t.Namespace)) n = t.Namespace + "." + n;
			if (t.Module != null) n += " (" + t.Module + ")";
			return "<" + v.GetInstanceID() + "> " + n;
		}

		public static string Deep<T>(T v, string capt, Func<T, string> idx = null, Func<T, string> sub = null)
		{
			var s = capt + ": ";

			if (v == null) return s + "(null)\n";
			s += ((idx == null) ? v.ToString() : idx(v)) + "\n";

			if (sub != null) s += sub(v);
			return s;
		}
	}
}
