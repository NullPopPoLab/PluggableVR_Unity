/*!	@file
	@brief HierarchyDumper: DynamicBoneCollider 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;

namespace PluggableVR_SN2
{
	//! DynamicBoneCollider 情報取得 
	public struct Dumper_DynamicBoneCollider
	{
		private DynamicBoneCollider _obj;

		public Dumper_DynamicBoneCollider(object obj) { _obj = obj as DynamicBoneCollider; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "Center: " + _obj.m_Center + "\n";
			s += indent + "Radius: " + _obj.m_Radius + "\n";
			s += indent + "Height: " + _obj.m_Height + "\n";
			s += indent + "Direction: " + _obj.m_Direction + "\n";
			s += indent + "Bound: " + _obj.m_Bound + "\n";

			return s;
		}
	}
}
