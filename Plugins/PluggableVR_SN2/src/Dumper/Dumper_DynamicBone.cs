/*!	@file
	@brief HierarchyDumper: DynamicBone 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;

namespace PluggableVR_SN2
{
	//! DynamicBone 情報取得 
	public struct Dumper_DynamicBone
	{
		private DynamicBone _obj;

		public Dumper_DynamicBone(object obj) { _obj = obj as DynamicBone; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + DumpForm.Enumrate(_obj.m_Colliders, "Colliders", (i) => indent + "  " + i, (v) => DumpForm.From(v)+"\n");

			return s;
		}
	}
}
