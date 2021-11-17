/*!	@file
	@brief HierarchyDumper: Slider 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine.UI;

namespace HierarchyDumper
{
	//! Slider 情報取得 
	public struct Dumper_Slider
	{
		private Slider _obj;

		public Dumper_Slider(object obj) { _obj = obj as Slider; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = new Dumper_Selectable(_obj).Dump(indent);
			s += indent + "Value: " + _obj.value + "\n";

			return s;
		}
	}
}
