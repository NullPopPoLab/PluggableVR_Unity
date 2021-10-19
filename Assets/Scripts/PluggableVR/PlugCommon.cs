/*!	@file
	@brief PluggableVR: プラグインクラス共通部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace PluggableVR
{
	public class PlugCommon
	{
		// Hierarchy 最上位に GameObject 生成 
		public static GameObject CreateRootObject(string name, Loc loc)
		{
			var obj = new GameObject(name);
			loc.ToWorldTransform(obj.transform);
			return obj;
		}

		// 指定 GameObject の下に GameObject 生成 
		public static GameObject CreateChildObject(string name, Transform parent, Loc loc, bool world)
		{
			var obj = new GameObject(name);
			obj.transform.SetParent(parent);
			if (world) loc.ToWorldTransform(obj.transform);
			else loc.ToLocalTransform(obj.transform);
			return obj;
		}
	}
}
