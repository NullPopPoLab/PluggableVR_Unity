/*!	@file
	@brief NullPopPoSpecial: コンポーネントユーティリティ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/
using System.Reflection;
using UnityEngine;

namespace NullPopPoSpecial
{
	//! コンポーネントユーティリティ 
	public static class ComponentUt
	{
		//! コンポーネントのコピー 
		public static T Copy<T>(T src, GameObject target) where T : Component
		{

			var af = target.activeSelf;
			if (af) target.SetActive(false);

			var dst = target.AddComponent<T>();
			var type = dst.GetType();

			var bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
			var pt = type.GetProperties(bf);
			for (var i = 0; i < pt.Length; ++i)
			{
				var pp = pt[i];
				if (!pp.CanWrite) continue;
				try
				{
					pp.SetValue(dst, pp.GetValue(src, null), null);
				}
				catch { }
			}

			var ft = type.GetFields(bf);
			for (var i = 0; i < ft.Length; ++i)
			{
				var fp = ft[i];
				fp.SetValue(dst, fp.GetValue(src));
			}

			if (af) target.SetActive(true);
			return dst;
		}

		//! コンポーネントをコピーし、コピー元は非稼働とする 
		public static T Posess<T>(T src, GameObject target) where T : Behaviour
		{
			var dst = Copy(src, target);
			src.enabled = false;
			return dst;
		}
	}
}
