/*!	@file
	@brief PluggableVR: Toggle 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine.UI;

namespace PluggableVR
{
	//! Toggle 情報取得 
	public struct Dumper_Toggle
	{
		private Toggle _obj;

		public Dumper_Toggle(object obj) { _obj = obj as Toggle; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = new Dumper_Selectable(_obj).Dump(indent);
			s += indent + "IsOn: " + _obj.isOn + "\n";

			return s;
		}
	}
}
