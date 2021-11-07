/*!	@file
	@brief HierarchyDumper: RectTransform 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace HierarchyDumper
{
	//! RectTransform 情報取得 
	public struct Dumper_RectTransform
	{
		private RectTransform _obj;

		public Dumper_RectTransform(object obj) { _obj = obj as RectTransform; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = new Dumper_Transform(_obj).Dump(indent);
			s += indent + "Rect: " + _obj.rect + "\n";
			s += indent + "AnchoredPosition: " + _obj.anchoredPosition + "\n";
			s += indent + "Pivot: " + _obj.pivot + "\n";

			return s;
		}
	}
}
