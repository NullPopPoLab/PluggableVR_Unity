/*!	@file
	@brief NullPopPoSpecial: 位置情報単位構造 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial_Unity
*/
using System;
using UnityEngine;

namespace NullPopPoSpecial
{
	//! 位置情報単位構造 
	/*!	@warning new で生成されたインスタンスは異常値で、 
			処理に混入すると原因特定困難なバグになりがちなので
			Rotの設定を伴わないインスタンス生成は必ず Identity を使う。
	*/
	[Serializable]
	public struct Loc
	{
		public Vector3 Pos;
		public Quaternion Rot;

		public Loc(Vector3 pos, Quaternion rot)
		{
			Pos = pos;
			Rot = rot;
		}

		//! 初期化済の値 
		public static Loc Identity
		{
			get
			{
				var t = new Loc();
				t.Rot.w = 1.0f;
				return t;
			}
		}

		//! Unity World Transform からの取り込み 
		public static Loc FromWorldTransform(Transform src)
		{
			return new Loc(src.position, src.rotation);
		}

		//! Unity Local Transform からの取り込み 
		public static Loc FromLocalTransform(Transform src)
		{
			return new Loc(src.localPosition, src.localRotation);
		}

		//! 有効性確認 
		public bool IsValid
		{
			get
			{
				return Rot.x * Rot.x + Rot.y * Rot.y + Rot.z * Rot.z + Rot.w * Rot.w >= Mathf.Epsilon;
			}
		}

		//! 逆値 
		public Loc Inversed
		{
			get
			{
				var t = new Loc();
				t.Rot = Quaternion.Inverse(Rot);
				t.Pos = t.Rot * (-Pos);
				return t;
			}
		}
		public static Loc operator !(Loc src) { return src.Inversed; }

		//! 正規化 
		public bool Normalize()
		{
			var l = Rot.x * Rot.x + Rot.y * Rot.y + Rot.z * Rot.z + Rot.w * Rot.w;
			if (l < Mathf.Epsilon) return false;

			var m = 1.0f / Mathf.Sqrt(l);
			Rot.x *= m;
			Rot.y *= m;
			Rot.z *= m;
			Rot.w *= m;
			return true;
		}
		public static Loc operator +(Loc src)
		{
			var t = src;
			return t.Normalize() ? t : src;
		}

		//! 位置に加算 
		public static Loc operator +(Loc dst, Vector3 src)
		{
			var t = dst;
			t.Pos += src;
			return t;
		}

		//! 位置に減算 
		public static Loc operator -(Loc dst, Vector3 src)
		{
			var t = dst;
			t.Pos -= src;
			return t;
		}

		//! 位置に乗算 
		public static Loc operator *(Loc dst, float src)
		{
			var t = dst;
			t.Pos *= src;
			return t;
		}

		//! 回転に加算 
		public static Loc operator +(Loc dst, Quaternion src)
		{
			var t = dst;
			t.Rot *= src;
			return t;
		}

		//! 回転に減算 
		public static Loc operator -(Loc dst, Quaternion src)
		{
			var t = dst;
			t.Rot = t.Rot * Quaternion.Inverse(src);
			return t;
		}

		//! 位置合成 
		public static Loc operator *(Loc parent, Loc child)
		{
			return new Loc(
				parent.Pos + parent.Rot * child.Pos,
				parent.Rot * child.Rot
			);
		}

		//! Unity World Transform への設定 
		public void ToWorldTransform(Transform t)
		{
			t.position = Pos;
			t.rotation = Rot;
		}

		//! Unity Local Transform への設定 
		public void ToLocalTransform(Transform t)
		{
			t.localPosition = Pos;
			t.localRotation = Rot;
		}

		public static bool CompareLoose(Loc v1, Loc v2, float threshold_p, float threshold_r)
		{
			var d = !v1 * v2;
			if (((d.Pos.x < 0.0f) ? (-d.Pos.x) : d.Pos.x) > threshold_p) return false;
			if (((d.Pos.y < 0.0f) ? (-d.Pos.y) : d.Pos.y) > threshold_p) return false;
			if (((d.Pos.z < 0.0f) ? (-d.Pos.z) : d.Pos.z) > threshold_p) return false;
			if (RotUt.Xx(d.Rot) < 1.0f - threshold_r) return false;
			if (RotUt.Yy(d.Rot) < 1.0f - threshold_r) return false;
			if (RotUt.Zz(d.Rot) < 1.0f - threshold_r) return false;
			return true;
		}
		public static bool CompareLoose(Loc v1, Loc v2, float threshold)
		{
			return CompareLoose(v1, v2, threshold, threshold);
		}
		public static bool CompareLoose(Loc v1, Loc v2)
		{
			return CompareLoose(v1, v2, Mathf.Epsilon, Mathf.Epsilon);
		}

		public static bool operator !=(Loc v1, Loc v2) {
			if (v1.Pos != v2.Pos) return true;
			if (v1.Rot != v2.Rot) return true;
			return false;
		}
		public static bool operator ==(Loc v1, Loc v2) { return !(v1 != v2); }


		public override bool Equals(object obj)
		{
			return this == (Loc)obj;
		}

		public override int GetHashCode()
		{
			return new { Pos, Rot }.GetHashCode();
		}
	}
}
