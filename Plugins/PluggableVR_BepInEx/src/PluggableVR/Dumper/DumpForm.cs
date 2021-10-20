/*!	@file
	@brief PluggableVR: Dump用書式 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using System.Collections.Generic;

namespace PluggableVR
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
			if(v==null)return "None";
			return "<" + v.GetInstanceID() + "> " + v.GetType().Name;
		}
	}
}
