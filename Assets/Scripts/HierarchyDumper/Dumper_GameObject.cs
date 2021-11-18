/*!	@file
	@brief HierarchyDumper: GameObject 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchyDumper
{
	//! GameObject 情報取得 
	public struct Dumper_GameObject
	{
		private GameObject _obj;
		public Dumper_GameObject(GameObject obj) { _obj = obj; }

		public string Dump(string indent0 = "", string indent1 = "")
		{
			var s = indent0 + (_obj.activeSelf ? "[*]" : "[_]");
			if (_obj.isStatic) s += "[S]";
			s += " " + _obj.GetInstanceID() + ": " + _obj.name;
			s += "\n";

			var i1 = indent1 + " | ";
			s += i1 + "Scene: " + _obj.scene.name + "\n";
			s += i1 + "Layer: " + _obj.layer + "\n";
			s += i1 + "Tag: " + _obj.tag + "\n";

			var i20 = indent1 + " |~";
			var i21 = indent1 + " | ";
			var cs = _obj.GetComponents(typeof(Component));
			var il = cs.Length;
			for (var i = 0; i < il; ++i)
			{
				var c = cs[i];
				var b = c as Behaviour;
				s += i20;
				if (b == null) s += "{@} ";
				else s += b.enabled ? "{*} " : "{_} ";
				s += "" + DumpForm.ClassInfo(c) + "\n";

				s += SpecialDumper.Dump(c, i21);
			}

			var i30 = indent1 + " +--";
			var i31 = indent1 + " |  ";
			foreach (Transform ct in _obj.transform)
			{
				s += new Dumper_GameObject(ct.gameObject).Dump(i30, i31);
			}

			s += indent1 + " `\n";
			return s;
		}
	}
}
