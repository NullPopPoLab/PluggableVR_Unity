/*!	@file
	@brief HierarchyDumper: Transform 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/Dumper_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace HierarchyDumper
{
	//! Transform 情報取得  
	public struct Dumper_Transform
	{
		private Transform _obj;

		public Dumper_Transform(object obj) { _obj = obj as Transform; }

		public string Dump(string indent = "")
		{

			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = "";
			s += indent + "Pos: " + DumpForm.From(_obj.position) + "\n";
			var r = _obj.rotation;
			s += indent + "RtX: " + DumpForm.From(RotUt.AxisX(r)) + "\n";
			s += indent + "RtY: " + DumpForm.From(RotUt.AxisY(r)) + "\n";
			s += indent + "RtZ: " + DumpForm.From(RotUt.AxisZ(r)) + "\n";
			s += indent + "Scl: " + DumpForm.From(_obj.localScale) + "\n";

			return s;
		}
	}
}
