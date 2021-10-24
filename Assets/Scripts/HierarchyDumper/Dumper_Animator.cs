/*!	@file
	@brief HierarchyDumper: Animator 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/Dumper_Unity
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
			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = "";
			s += indent + "Avatar: "+((_obj.avatar==null)?"None":(_obj.avatar.name + "\n" + new Dumper_Avatar(_obj.avatar).Dump(indent + "  ")));
			s += indent + "ApplyRootMotion: "+_obj.applyRootMotion+"\n";

			return s;
		}
	}
}
