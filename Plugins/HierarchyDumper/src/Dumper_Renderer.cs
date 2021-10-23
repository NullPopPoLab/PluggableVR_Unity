/*!	@file
	@brief HierarchyDumper: Renderer 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/Dumper_Unity
*/
using UnityEngine;

namespace HierarchyDumper
{
	//! Renderer 情報取得 
	public struct Dumper_Renderer
	{
		private Renderer _obj;

		public Dumper_Renderer(object obj) { _obj = obj as Renderer; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = "";
			s += indent + "Enabled: " + _obj.enabled + "\n";
			s += indent + "IsVisible: " + _obj.isVisible + "\n";
			s += indent + "SortingLayer: " + _obj.sortingLayerID + " (" + _obj.sortingLayerName + ")\n";
			s += indent + "SortingOrder: " + _obj.sortingOrder + "\n";

			return s;
		}
	}
}
