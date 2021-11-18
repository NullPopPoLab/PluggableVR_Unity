/*!	@file
	@brief HierarchyDumper: CanvasGroup 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace HierarchyDumper
{
	//! CanvasGroup 情報取得 
	public struct Dumper_CanvasGroup
	{
		private CanvasGroup _obj;

		public Dumper_CanvasGroup(object obj) { _obj = obj as CanvasGroup; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "Alpha: " + _obj.alpha + "\n";
			s += indent + "BlocksRaycasts: " + _obj.blocksRaycasts + "\n";
			s += indent + "IgnoreParentGroups: " + _obj.ignoreParentGroups + "\n";
			s += indent + "Interactable: " + _obj.interactable + "\n";

			return s;
		}
	}
}
