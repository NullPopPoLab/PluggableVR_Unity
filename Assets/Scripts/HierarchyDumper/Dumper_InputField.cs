/*!	@file
	@brief HierarchyDumper: InputField 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine.UI;

namespace HierarchyDumper
{
	//! InputField 情報取得 
	public struct Dumper_InputField
	{
		private InputField _obj;

		public Dumper_InputField(object obj) { _obj = obj as InputField; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = new Dumper_Selectable(_obj).Dump(indent);
			s += indent + "ReadOnly: " + _obj.readOnly + "\n";
			s += indent + "ContentType: " + _obj.contentType + "\n";
			s += indent + "CharacterValidation: " + _obj.characterValidation + "\n";
			s += indent + "Text: " + _obj.text + "\n";

			return s;
		}
	}
}
