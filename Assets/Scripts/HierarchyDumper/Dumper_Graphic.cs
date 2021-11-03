/*!	@file
	@brief HierarchyDumper: Graphic 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine.UI;

namespace HierarchyDumper
{
	//! Graphic 情報取得 
	public struct Dumper_Graphic
	{
		private Graphic _obj;

		public Dumper_Graphic(object obj) { _obj = obj as Graphic; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "RaycastTarget: " + _obj.raycastTarget + "\n";
			s += indent + "Depth: " + _obj.depth + "\n";

			return s;
		}
	}
}
