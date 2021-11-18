/*!	@file
	@brief HierarchyDumper: Animator 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace HierarchyDumper
{
	//! Animator 情報取得 
	public struct Dumper_Animator
	{
		private Animator _obj;

		public Dumper_Animator(object obj) { _obj = obj as Animator; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + DumpForm.Deep(_obj.avatar, "Avatar", (o) => o.name, (o) => new Dumper_Avatar(o).Dump(indent + "  "));
			s += indent + "ApplyRootMotion: " + _obj.applyRootMotion + "\n";

			return s;
		}
	}
}
