/*!	@file
	@brief HierarchyDumper: Collider 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace HierarchyDumper
{
	//! Collider 情報取得 
	public struct Dumper_Collider
	{
		private Collider _obj;

		public Dumper_Collider(object obj) { _obj = obj as Collider; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "Enabled: " + _obj.enabled + "\n";
			s += indent + "IsTrigger: " + _obj.isTrigger + "\n";
			s += indent + "Material: " + DumpForm.From(_obj.material) + "\n";
			s += indent + "AttachedRigidbody: " + DumpForm.From(_obj.attachedRigidbody) + "\n";

			return s;
		}
	}
}
