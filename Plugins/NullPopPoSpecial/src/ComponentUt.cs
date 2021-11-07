/*!	@file
	@brief NullPopPoSpecial: コンポーネントユーティリティ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/
using System;
using System.Reflection;
using UnityEngine;

namespace NullPopPoSpecial
{
	//! コンポーネントユーティリティ 
	public static class ComponentUt
	{
		//! コンポーネント単品のコピー 
		public static T Copy<T>(T src, GameObject target, bool onbeat=false) where T : Component
		{
			var af = onbeat ? false : target.activeSelf;
			if (af) target.SetActive(false);

			var type = src.GetType();
			var dst = target.AddComponent(type) as T;
			Serializer.Copy(src, dst);

			if (af) target.SetActive(true);
			return dst;
		}

		//! 同クラスのコンポーネントを全コピー 
		public static void Copy<T>(GameObject from, GameObject target, bool onbeat = false) where T : Component
		{
			var af = onbeat ? false : target.activeSelf;
			if (af) target.SetActive(false);

			var cs = from.GetComponents<T>();
			for (var i = 0; i < cs.Length; ++i) Copy(cs[i], target);

			if (af) target.SetActive(true);
		}

		//! コンポーネント単品をコピーし、コピー元は非稼働とする 
		public static T Possess<T>(T src, GameObject target, bool onbeat = false) where T : Behaviour
		{
			var dst = Copy(src, target, onbeat);
			src.enabled = false;
			return dst;
		}

		//! 同クラスのコンポーネントを全コピーし、コピー元は非稼働とする 
		public static void Possess<T>(GameObject from, GameObject target, bool onbeat = false) where T : Behaviour
		{
			var af = onbeat ? false : target.activeSelf;
			if (af) target.SetActive(false);

			var cs = from.GetComponents<T>();
			for (var i = 0; i < cs.Length; ++i) Possess(cs[i], target);

			if (af) target.SetActive(true);
		}

		//! 指定クラスのコンポーネントを一括エンコード 
		public static Serialized[] Encode(GameObject from, string[] clas)
		{
			var l = clas.Length;
			var dst = new Serialized[l];
			for (var i = 0; i < l; ++i)
			{
				try
				{
					var type = Type.GetType(clas[i]);
					var src = from.GetComponent(type);
					if (src == null) continue;
					dst[i] = Serializer.Encode(src);
				}
				catch { }
			}
			return dst;
		}

		//! 指定クラスのコンポーネントを一括デコード 
		public static void Decode(Serialized[] src, GameObject target, bool onbeat = false)
		{
			var af = onbeat ? false : target.activeSelf;
			if (af) target.SetActive(false);

			var l = src.Length;
			for (var i = 0; i < l; ++i)
			{
				var cn = src[i].Class;
				if (String.IsNullOrEmpty(cn)) continue;
				try
				{
					var type = Type.GetType(cn);
					var dst = target.GetComponent(type);
					if (dst == null) continue;
					Serializer.Decode(src[i], dst);
				}
				catch { }
			}

			if (af) target.SetActive(true);
		}
	}
}
