/*!	@file
	@brief PluggableVR: Text 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine.UI;

namespace PluggableVR
{
	//! Text 情報取得 
	public struct Dumper_Text
	{
		private Text _obj;

		public Dumper_Text(object obj) { _obj = obj as Text; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = new Dumper_Selectable(_obj).Dump(indent);
			s += indent + "Text: " + _obj.text + "\n";

			return s;
		}
	}
}
