/*!	@file
	@brief PluggableVR: Button 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine.UI;

namespace PluggableVR
{
	//! Button 情報取得 
	public struct Dumper_Button
	{
		private Button _obj;

		public Dumper_Button(object obj) { _obj = obj as Button; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = new Dumper_Selectable(_obj).Dump(indent);

			return s;
		}
	}
}
