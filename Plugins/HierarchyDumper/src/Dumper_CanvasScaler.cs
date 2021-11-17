/*!	@file
	@brief HierarchyDumper: CanvasScaler 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine.UI;

namespace HierarchyDumper
{
	//! CanvasScaler 情報取得 
	public struct Dumper_CanvasScaler
	{
		private CanvasScaler _obj;

		public Dumper_CanvasScaler(object obj) { _obj = obj as CanvasScaler; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "ScreenMatchMode: " + _obj.screenMatchMode + "\n";
			s += indent + "UIScaleMode: " + _obj.uiScaleMode + "\n";
			s += indent + "ReferenceResolution: " + DumpForm.From(_obj.referenceResolution) + "\n";
			s += indent + "ScaleFactor: " + _obj.scaleFactor + "\n";
			s += indent + "MatchWidthOrHeight: " + _obj.matchWidthOrHeight + "\n";

			return s;
		}
	}
}
