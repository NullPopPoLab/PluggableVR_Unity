/*!	@file
	@brief HierarchyDumper: CapsuleCollider 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace HierarchyDumper
{
	//! CapsuleCollider 情報取得 
	public struct Dumper_CapsuleCollider
	{
		private CapsuleCollider _obj;

		public Dumper_CapsuleCollider(object obj) { _obj = obj as CapsuleCollider; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = new Dumper_Collider(_obj).Dump(indent);
			s += indent + "Center: " + _obj.center + "\n";
			s += indent + "Direction: " + _obj.direction + "\n";
			s += indent + "Height: " + _obj.height + "\n";
			s += indent + "Radius: " + _obj.radius + "\n";

			return s;
		}
	}
}
