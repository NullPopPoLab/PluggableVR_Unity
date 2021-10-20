/*!	@file
	@brief PluggableVR: Selectable 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine.UI;

namespace PluggableVR
{
	//! Selectable 情報取得 
	public struct Dumper_Selectable
	{
		private Selectable _obj;

		public Dumper_Selectable(object obj) { _obj = obj as Selectable; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = "";
			s += indent + "Interactable: " + _obj.interactable + "\n";

			return s;
		}
	}
}
