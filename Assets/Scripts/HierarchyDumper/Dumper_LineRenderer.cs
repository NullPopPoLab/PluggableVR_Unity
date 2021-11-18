/*!	@file
	@brief HierarchyDumper: LineRenderer 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace HierarchyDumper
{
	//! LineRenderer 情報取得 
	public struct Dumper_LineRenderer
	{
		private LineRenderer _obj;

		public Dumper_LineRenderer(object obj) { _obj = obj as LineRenderer; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = new Dumper_Renderer(_obj).Dump(indent);

			return s;
		}
	}
}
