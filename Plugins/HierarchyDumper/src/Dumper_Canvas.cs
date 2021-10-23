/*!	@file
	@brief HierarchyDumper: Canvas 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/Dumper_Unity
*/
using UnityEngine;

namespace HierarchyDumper
{
	//! Canvas 情報取得 
	public struct Dumper_Canvas
	{
		private Canvas _obj;

		public Dumper_Canvas(object obj) { _obj = obj as Canvas; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = "";
			s += indent + "RenderMode: " + _obj.renderMode + "\n";
			s += indent + "RootCanvas: " + DumpForm.From(_obj.rootCanvas) + "\n";
			s += indent + "WorldCamera: " + DumpForm.From(_obj.worldCamera) + "\n";
			s += indent + "SortingLayer: " + _obj.sortingLayerID + " ("+_obj.sortingLayerName+")\n";
			s += indent + "SortingOrder: " + _obj.sortingOrder + "\n";
			s += indent + "OverrideSorting: " + _obj.overrideSorting + "\n";
			s += indent + "RenderOrder: " + _obj.renderOrder + "\n";
			s += indent + "PlaneDistance: " + _obj.planeDistance + "\n";
			s += indent + "TargetDisplay: " + _obj.targetDisplay + "\n";

			return s;
		}
	}
}
