/*!	@file
	@brief PluggableVR: プラグインクラス共通部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR
{
	public class PlugCommon
	{
		//! Hierarchy 最上位に GameObject 生成 
		public static GameObject CreateRootObject(string name, Loc loc)
		{
			var obj = new GameObject(name);
			loc.ToWorldTransform(obj.transform);
			return obj;
		}

		//! 指定 GameObject の下に GameObject 生成 
		public static GameObject CreateChildObject(string name, Transform parent, Loc loc, bool world)
		{
			var obj = new GameObject(name);
			obj.transform.SetParent(parent);
			if (world) loc.ToWorldTransform(obj.transform);
			else loc.ToLocalTransform(obj.transform);
			return obj;
		}

		//! Hierarchy 最上位に Primitive 生成 
		public static GameObject CreateRootPrimitive(PrimitiveType type, bool collision, string name, Loc loc)
		{
			var obj = GameObject.CreatePrimitive(type);
			obj.name = name;
			loc.ToWorldTransform(obj.transform);
			if (!collision) GameObject.Destroy(obj.GetComponent<Collider>());
			return obj;
		}

		//! 指定 GameObject の下に Primitive 生成 
		public static GameObject CreateChildPrimitive(PrimitiveType type, bool collision, string name, Transform parent, Loc loc, bool world)
		{
			var obj = GameObject.CreatePrimitive(type);
			obj.name = name;
			obj.transform.SetParent(parent);
			if (world) loc.ToWorldTransform(obj.transform);
			else loc.ToLocalTransform(obj.transform);
			if (!collision) GameObject.Destroy(obj.GetComponent<Collider>());
			return obj;
		}
	}
}
