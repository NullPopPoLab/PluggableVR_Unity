/*!	@file
	@brief NullPopPoSpecial: 2次元回転 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace NullPopPoSpecial
{
	//! 2次元回転 
	/*!	@warning new で生成されたインスタンスは異常値で、 
			処理に混入すると原因特定困難なバグになりがちなので
			Rotの設定を伴わないインスタンス生成は必ず Identity を使う。
	*/
	public struct Rot2D
	{
		public float C; //!< cos成分 
		public float S; //!< sin成分 

		public Rot2D(float c, float s)
		{
			C = c;
			S = s;
		}

		public override string ToString()
		{
			return "(" + C + "," + S + ")";
		}

		//! ラジアン値 
		public float Radian
		{
			get
			{
				return Mathf.Atan2(S, C);
			}
		}

		//! 度値 
		public float Degree
		{
			get
			{
				return Radian * Mathf.Rad2Deg;
			}
		}

		//! 初期化済の値 
		public static Rot2D Identity
		{
			get
			{
				return new Rot2D(1, 0);
			}
		}

		//! 異常値 
		public static Rot2D Invalid
		{
			get
			{
				return new Rot2D(0, 0);
			}
		}

		//! 直角相当のインスタンス 
		public static Rot2D Right
		{
			get
			{
				return new Rot2D(0, 1);
			}
		}

		//! 回転量から生成 
		public static Rot2D FromCycle(Cycle src)
		{
			var r = src.Fraction * 4.0f;
			var w = (int)r;
			var s = Mathf.Sin((r - w) * Mathf.PI * 0.5f);
			var c = SafeMathf.Sqrt(1.0f - s * s);
			switch (w)
			{
				case 3: return new Rot2D(s, -c);
				case 2: return new Rot2D(-c, -s);
				case 1: return new Rot2D(-s, c);
				default: return new Rot2D(c, s);
			}
		}

		//! ラジアンから生成 
		public static Rot2D FromRadian(float r)
		{
			return new Rot2D(Mathf.Cos(r), Mathf.Sin(r));
		}

		//! 度から生成 
		public static Rot2D FromDegree(float r)
		{
			return FromRadian(r * Mathf.Deg2Rad);
		}

		//! コサイン成分から生成 
		public static Rot2D FromCosine(float c)
		{
			return new Rot2D(c, SafeMathf.Sqrt(1.0f - c * c));
		}

		//! ベクトルから生成 
		public static Rot2D FromVector(Vector2 v)
		{
			var n = v.normalized;
			return new Rot2D(n.x, n.y);
		}

		//! 有効性確認 
		public bool IsValid
		{
			get
			{
				return C * C + S * S >= Mathf.Epsilon;
			}
		}

		//! 倍角生成 
		public Rot2D Double
		{
			get
			{
				return new Rot2D(C * C - S * S, 2.0f * S * C);
			}
		}

		//! 半角生成 
		/*!	@attention -180°~+180°として扱われるため、
				270°の半分は135°ではなく-45°となる。 @n
		*/
		public Rot2D Half
		{
			get
			{
				var c = C * 0.5f;
				var p = 0.5f + c;
				var m = 0.5f - c;
				var t = new Rot2D(
					SafeMathf.Sqrt(p),
					SafeMathf.Sqrt(m)
				);
				if (S < 0.0f) t.S = -t.S;
				return t;
			}
		}

		//! 正規化 
		public bool Normalize()
		{
			var l = C * C + S * S;
			if (l < Mathf.Epsilon) return false;

			var m = 1.0f / SafeMathf.Sqrt(l);
			C *= m;
			S *= m;
			return true;
		}

		//! 逆回転 
		public static Rot2D operator -(Rot2D src)
		{
			return new Rot2D(src.C, -src.S);
		}

		//! 回転の加算 
		public static Rot2D operator +(Rot2D dst, Rot2D src)
		{
			return new Rot2D(
				dst.C * src.C + dst.S * src.S,
				dst.S * src.C - dst.C * src.S
			);
		}

		//! 回転の減算 
		public static Rot2D operator -(Rot2D dst, Rot2D src)
		{
			return new Rot2D(
				dst.C * src.C - dst.S * src.S,
				dst.S * src.C + dst.C * src.S
			);
		}

		//! ベクトルに回転を適用 
		public static Vector2 operator *(Rot2D dst, Vector2 src)
		{
			return new Vector2(
				dst.C * src.x - dst.S * src.y,
				dst.S * src.x + dst.C * src.y
			);
		}
	}
}
