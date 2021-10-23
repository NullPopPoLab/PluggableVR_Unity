/*!	@file
	@brief HierarchyDumper: Text 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/Dumper_Unity
*/
using UnityEngine.UI;

namespace HierarchyDumper
{
	//! Dropdown 情報取得 
	public struct Dumper_Dropdown
	{
		private Dropdown _obj;

		public Dumper_Dropdown(object obj) { _obj = obj as Dropdown; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = new Dumper_Selectable(_obj).Dump(indent);
			s += indent + "CaptionText: " + _obj.captionText + "\n";
			s += indent + "value: " + _obj.value +" ("+_obj.itemText+")"+ "\n";

			return s;
		}
	}
}
