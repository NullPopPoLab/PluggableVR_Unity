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
		//! コンポーネント単品のコピー 
		public static T Copy<T>(T src, GameObject target) where T : Component
		{
			var af = target.activeSelf;
			if (af) target.SetActive(false);

			var bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
			var type = src.GetType();
			var dst = target.AddComponent(type) as T;

			var pt = type.GetProperties(bf);
			for (var j = 0; j < pt.Length; ++j)
			{
				var pp = pt[j];
				if (!pp.CanWrite) continue;
				try
				{
					pp.SetValue(dst, pp.GetValue(src, null), null);
				}
				catch { }
			}

			var ft = type.GetFields(bf);
			for (var j = 0; j < ft.Length; ++j)
			{
				var fp = ft[j];
				fp.SetValue(dst, fp.GetValue(src));
			}

			if (af) target.SetActive(true);
			return dst;
		}

		//! 同クラスのコンポーネントを全コピー 
		public static void Copy<T>(GameObject from, GameObject target) where T : Component
		{
			var af = target.activeSelf;
			if (af) target.SetActive(false);

			var cs = from.GetComponents<T>();
			for (var i = 0; i < cs.Length; ++i) Copy(cs[i], target);

			if (af) target.SetActive(true);
		}

		//! コンポーネント単品をコピーし、コピー元は非稼働とする 
		public static T Possess<T>(T src, GameObject target) where T : Behaviour
		{
			var dst = Copy(src, target);
			src.enabled = false;
			return dst;
		}

		//! 同クラスのコンポーネントを全コピーし、コピー元は非稼働とする 
		public static void Possess<T>(GameObject from, GameObject target) where T : Behaviour
		{
			var af = target.activeSelf;
			if (af) target.SetActive(false);

			var cs = from.GetComponents<T>();
			for (var i = 0; i < cs.Length; ++i) Possess(cs[i], target);

			if (af) target.SetActive(true);
		}
	}
}
