/*!	@file
	@brief HierarchyDumper: Avatar 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace HierarchyDumper
{
	//! Avatar 情報取得 
	public struct Dumper_Avatar
	{
		private Avatar _obj;

		public Dumper_Avatar(object obj) { _obj = obj as Avatar; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "IsValid: " + _obj.isValid + "\n";
			s += indent + "IsHuman: " + _obj.isHuman + "\n";

			return s;
		}
	}
}
