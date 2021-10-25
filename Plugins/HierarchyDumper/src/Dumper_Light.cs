/*!	@file
	@brief HierarchyDumper: Light 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/Dumper_Unity
*/
using UnityEngine;

namespace HierarchyDumper
{
	//! Light 情報取得 
	public struct Dumper_Light
	{
		private Light _obj;

		public Dumper_Light(object obj) { _obj = obj as Light; }

		public string Dump(string indent = "")
		{

			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = "";
			s += indent + "RenderMode: " + _obj.renderMode + "\n";
			s += indent + "Type: " + _obj.type + "\n";
			s += indent + "Color: " + _obj.color + "\n";
			s += indent + "CullingMask: " + _obj.cullingMask.ToString("X") + "\n";

			return s;
		}
	}
}
