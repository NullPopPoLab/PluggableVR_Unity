/*!	@file
	@brief HierarchyDumper: Dump用書式 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/Dumper_Unity
*/
using UnityEngine;
using System.Collections.Generic;

namespace HierarchyDumper
{
	public static class DumpForm
	{
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

		public static string From(Object v)
		{
			if (v == null) return "None";
			var t = v.GetType();
			var n = t.Name;
			if (t.Namespace != "") n = t.Namespace + "." + n;
			if (t.Module != null) n += " (" + t.Module + ")";
			return "<" + v.GetInstanceID() + "> " + n;
		}
	}
}
