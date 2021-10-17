/*!	@file
	@brief PluggableVR: 回転量 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace PluggableVR
{
	//! 回転量 
	public struct Cycle
	{
		public float O;

		public Cycle(float o)
		{
			O = o;
		}

		//! 直角相当のインスタンス 
		public static Cycle Right { get { return new Cycle(0.25f); } }

		//! 完全回転数 
		public float Whole
		{
			get
			{
				return Mathf.Floor(O);
			}
		}

		//! 半端回転数 
		public float Fraction
		{
			get
			{
				return O - Mathf.Floor(O);
			}
		}

		//! ラジアン値 
		public float Radian
		{
			get
			{
				return O * 2.0f * Mathf.PI;
			}
		}

		//! 度値 
		public float Degree
		{
			get
			{
				return O * 360.0f;
			}
		}

		//! ラジアンから生成 
		public static Cycle FromRadian(float r)
		{
			return new Cycle(r * 0.5f / Mathf.PI);
		}

		//! 度から生成 
		/*!	@attention 除算を伴い速度も精度も劣るので、
				等分角を示す目的であればコンストラクタを直接使うべき。 @n
		*/
		public static Cycle FromDegree(float r)
		{
			return new Cycle(r / 360.0f);
		}

		public static Cycle operator +(Cycle dst, Cycle src)
		{
			return new Cycle(dst.O + src.O);
		}
		public static Cycle operator -(Cycle dst, Cycle src)
		{
			return new Cycle(dst.O - src.O);
		}
		public static Cycle operator *(Cycle dst, float src)
		{
			return new Cycle(dst.O * src);
		}
		public static Cycle operator /(Cycle dst, float src)
		{
			return new Cycle(dst.O / src);
		}
	}
}
