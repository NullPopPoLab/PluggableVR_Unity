/*!	@file
	@brief NullPopPoSpecial: シリアライザ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.Reflection;

namespace NullPopPoSpecial
{
	//! シリアライズ内容 
	[Serializable]
	public struct Serialized
	{
		public string Module;
		public string Class;
		public object[] Prop;
		public object[] Fld;

		public Type Type { get { return Type.GetType(Class + ", " + Module); } }
	}

	//! シリアライザ 
	public static class Serializer
	{
		private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

		public static Serialized Encode(object src){
			var dst = new Serialized();
			var type = src.GetType();
			dst.Module = type.Module.Name;
			dst.Class = type.FullName;

			var pt = type.GetProperties(Flags);
			dst.Prop = new object[pt.Length];
			for (var i = 0; i < pt.Length; ++i)
			{
				var pp = pt[i];
				try
				{
					dst.Prop[i] = pp.GetValue(src, null);
				}
				catch { }
			}

			var ft = type.GetFields(Flags);
			dst.Fld = new object[pt.Length];
			for (var i = 0; i < ft.Length; ++i)
			{
				var fp = ft[i];
				dst.Fld[i] = fp.GetValue(src);
			}

			return dst;
		}

		public static void Decode<T>(Serialized src, T dst)
		{
			var type = dst.GetType();

			var pt = type.GetProperties(Flags);
			for (var i = 0; i < pt.Length; ++i)
			{
				var pp = pt[i];
				if (!pp.CanWrite) continue;
				try
				{
					pp.SetValue(dst, src.Prop[i], null);
				}
				catch { }
			}

			var ft = type.GetFields(Flags);
			for (var i = 0; i < ft.Length; ++i)
			{
				var fp = ft[i];
				fp.SetValue(dst, src.Fld[i]);
			}
		}

		public static void Copy<T>(T src, T dst)
		{
			var type = typeof(T);

			var pt = type.GetProperties(Flags);
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

			var ft = type.GetFields(Flags);
			for (var i = 0; i < ft.Length; ++i)
			{
				var fp = ft[i];
				fp.SetValue(dst, fp.GetValue(src));
			}
		}
	}
}
