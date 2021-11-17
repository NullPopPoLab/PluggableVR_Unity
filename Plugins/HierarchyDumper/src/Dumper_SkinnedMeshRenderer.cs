/*!	@file
	@brief HierarchyDumper: SkinnedMeshRenderer 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace HierarchyDumper
{
	//! SkinnedMeshRenderer 情報取得 
	public struct Dumper_SkinnedMeshRenderer
	{
		private SkinnedMeshRenderer _obj;

		public Dumper_SkinnedMeshRenderer(object obj) { _obj = obj as SkinnedMeshRenderer; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = new Dumper_Renderer(_obj).Dump(indent);

			return s;
		}
	}
}
