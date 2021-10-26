/*!	@file
	@brief HierarchyDumper: MeshRenderer 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/Dumper_Unity
*/
using UnityEngine;

namespace HierarchyDumper
{
	//! MeshRenderer 情報取得 
	public struct Dumper_MeshRenderer
	{
		private MeshRenderer _obj;

		public Dumper_MeshRenderer(object obj) { _obj = obj as MeshRenderer; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = new Dumper_Renderer(_obj).Dump(indent);

			return s;
		}
	}
}
