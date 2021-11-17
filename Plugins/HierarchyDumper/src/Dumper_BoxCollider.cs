/*!	@file
	@brief HierarchyDumper: BoxCollider 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace HierarchyDumper
{
	//! BoxCollider 情報取得 
	public struct Dumper_BoxCollider
	{
		private BoxCollider _obj;

		public Dumper_BoxCollider(object obj) { _obj = obj as BoxCollider; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = new Dumper_Collider(_obj).Dump(indent);
			s += indent + "Center: " + _obj.center + "\n";
			s += indent + "Size: " + _obj.size + "\n";

			return s;
		}
	}
}
