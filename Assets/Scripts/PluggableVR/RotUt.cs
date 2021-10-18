/*!	@file
	@brief PluggableVR: 回転ユーティリティ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace PluggableVR
{
	//! 回転ユーティリティ 
	/*!	@note 主に Quaternion のテクニック集 
	*/
	public static class RotUt
	{
		//! X軸回転 
		public static Quaternion RotX(float rad)
		{
			rad *= 0.5f;
			var c = Mathf.Cos(rad);
			var s = Mathf.Sin(rad);
			return new Quaternion(s, 0, 0, c);
		}
		public static Quaternion RotX(Rot2D rot)
		{
			rot = rot.Half;
			var c = rot.C;
			var s = rot.S;
			return new Quaternion(s, 0, 0, c);
		}

		//! Y軸回転 
		public static Quaternion RotY(float rad)
		{
			rad *= 0.5f;
			var c = Mathf.Cos(rad);
			var s = Mathf.Sin(rad);
			return new Quaternion(0, s, 0, c);
		}
		public static Quaternion RotY(Rot2D rot)
		{
			rot = rot.Half;
			var c = rot.C;
			var s = rot.S;
			return new Quaternion(0, s, 0, c);
		}

		//! Z軸回転 
		public static Quaternion RotZ(float rad)
		{
			rad *= 0.5f;
			var c = Mathf.Cos(rad);
			var s = Mathf.Sin(rad);
			return new Quaternion(0, 0, s, c);
		}
		public static Quaternion RotZ(Rot2D rot)
		{
			rot = rot.Half;
			var c = rot.C;
			var s = rot.S;
			return new Quaternion(0, 0, s, c);
		}

		//! 任意軸回転 
		public static Quaternion Rot(Vector3 dir, float rad)
		{
			rad *= 0.5f;
			var c = Mathf.Cos(rad);
			var s = Mathf.Sin(rad);
			return new Quaternion(s * dir.x, s * dir.y, s * dir.z, c);
		}
		public static Quaternion Rot(Vector3 dir, Rot2D rot)
		{
			rot = rot.Half;
			var c = rot.C;
			var s = rot.S;
			return new Quaternion(s * dir.x, s * dir.y, s * dir.z, c);
		}

		//! 回転x軸x成分抽出 
		public static float Xx(Quaternion src)
		{
			return 1.0f - 2.0f * (src.y * src.y + src.z * src.z);
		}

		//! 回転x軸y成分抽出 
		public static float Xy(Quaternion src)
		{
			return 2.0f * (src.x * src.y + src.w * src.z);
		}

		//! 回転x軸z成分抽出 
		public static float Xz(Quaternion src)
		{
			return 2.0f * (src.x * src.z - src.w * src.y);
		}

		//! 回転y軸x成分抽出 
		public static float Yx(Quaternion src)
		{
			return 2.0f * (src.x * src.y - src.w * src.z);
		}

		//! 回転y軸y成分抽出 
		public static float Yy(Quaternion src)
		{
			return 1.0f - 2.0f * (src.z * src.z + src.x * src.x);
		}

		//! 回転y軸z成分抽出 
		public static float Yz(Quaternion src)
		{
			return 2.0f * (src.y * src.z + src.w * src.x);
		}

		//! 回転z軸x成分抽出 
		public static float Zx(Quaternion src)
		{
			return 2.0f * (src.x * src.z + src.w * src.y);
		}

		//! 回転z軸y成分抽出 
		public static float Zy(Quaternion src)
		{
			return 2.0f * (src.y * src.z - src.w * src.x);
		}

		//! 回転z軸z成分抽出 
		public static float Zz(Quaternion src)
		{
			return 1.0f - 2.0f * (src.x * src.x + src.y * src.y);
		}

		//! 回転x軸抽出 
		public static Vector3 AxisX(Quaternion src)
		{
			return new Vector3(Xx(src), Xy(src), Xz(src));
		}

		//! 回転y軸抽出 
		public static Vector3 AxisY(Quaternion src)
		{
			return new Vector3(Yx(src), Yy(src), Yz(src));
		}

		//! 回転z軸抽出 
		public static Vector3 AxisZ(Quaternion src)
		{
			return new Vector3(Zx(src), Zy(src), Zz(src));
		}

		//! 回転x軸を既定方向に戻す 
		public static Quaternion ReturnX(Quaternion src)
		{
			// x軸が傾いていなければ処理無用 
			if (Xx(src) >= 1.0f) return src;

			// 戻すための回転軸 
			var a = AxisX(src);
			var b = new Vector3(1, 0, 0);
			var c = Vector3.Cross(a, b);
			var l = c.sqrMagnitude;
			c = (l < Mathf.Epsilon) ?
				new Vector3(0, 0, 1) : // 180°傾いているときはとりあえずz軸で戻しとく 
				(c * 1.0f / SafeMathf.Sqrt(l)); // 外積方向を回転軸とする 
			var d = Vector3.Dot(a, b); // 回転量cos成分 

			return Rot(c, Rot2D.FromCosine(d)) * src;
		}

		//! 回転y軸を既定方向に戻す 
		public static Quaternion ReturnY(Quaternion src)
		{
			// y軸が傾いていなければ処理無用 
			if (Yy(src) >= 1.0f) return src;

			// 戻すための回転軸 
			var a = AxisY(src);
			var b = new Vector3(0, 1, 0);
			var c = Vector3.Cross(a, b);
			var l = c.sqrMagnitude;
			c = (l < Mathf.Epsilon) ?
				new Vector3(1, 0, 0) : // 180°傾いているときはとりあえずx軸で戻しとく 
				(c * 1.0f / SafeMathf.Sqrt(l)); // 外積方向を回転軸とする 
			var d = Vector3.Dot(a, b); // 回転量cos成分 

			return Rot(c, Rot2D.FromCosine(d)) * src;
		}

		//! 回転z軸を既定方向に戻す 
		public static Quaternion ReturnZ(Quaternion src)
		{
			// z軸が傾いていなければ処理無用 
			if (Zz(src) >= 1.0f) return src;

			// 戻すための回転軸 
			var a = AxisZ(src);
			var b = new Vector3(0, 0, 1);
			var c = Vector3.Cross(a, b);
			var l = c.sqrMagnitude;
			c = (l < Mathf.Epsilon) ?
				new Vector3(0, 1, 0) : // 180°傾いているときはとりあえずy軸で戻しとく 
				(c * 1.0f / SafeMathf.Sqrt(l)); // 外積方向を回転軸とする 
			var d = Vector3.Dot(a, b); // 回転量cos成分 

			return Rot(c, Rot2D.FromCosine(d)) * src;
		}

		//! xy平面回転抽出 
		public static Rot2D PlaneXY(Quaternion src)
		{
			// z軸を戻してx軸から回転を得る 
			var t = ReturnZ(src);
			return new Rot2D(Xx(t), Xy(t));
		}

		//! yz平面回転抽出 
		public static Rot2D PlaneYZ(Quaternion src)
		{
			// x軸を戻してy軸から回転を得る 
			var t = ReturnX(src);
			return new Rot2D(Yy(t), Yz(t));
		}

		//! zx平面回転抽出 
		public static Rot2D PlaneZX(Quaternion src)
		{
			// y軸を戻してz軸から回転を得る 
			var t = ReturnY(src);
			return new Rot2D(Zz(t), Zx(t));
		}
	}
}
