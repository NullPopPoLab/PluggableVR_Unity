/*!	@file
	@brief HierarchyDumper: FadeCanvas 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;

namespace PluggableVR_KKS
{
	//! FadeCanvas 情報取得 
	public struct Dumper_FadeCanvas
	{
		private FadeCanvas _obj;

		public Dumper_FadeCanvas(object obj) { _obj = obj as FadeCanvas; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "CanvasGroup: " + DumpForm.From(_obj.canvasGroup) + "\n";
			s += indent + "IsEnd: " + _obj.isEnd + "\n";
			s += indent + "IsFading: " + _obj.isFading + "\n";

			return s;
		}
	}
}
