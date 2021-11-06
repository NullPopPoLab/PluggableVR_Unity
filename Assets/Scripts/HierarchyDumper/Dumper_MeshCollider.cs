/*!	@file
	@brief HierarchyDumper: MeshCollider 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace HierarchyDumper
{
	//! MeshCollider 情報取得 
	public struct Dumper_MeshCollider
	{
		private MeshCollider _obj;

		public Dumper_MeshCollider(object obj) { _obj = obj as MeshCollider; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = new Dumper_Collider(_obj).Dump(indent);
			s += indent + "Convex: " + _obj.convex + "\n";
			s += indent + "SharedMesh: " + DumpForm.From(_obj.sharedMesh) + "\n";

			return s;
		}
	}
}
