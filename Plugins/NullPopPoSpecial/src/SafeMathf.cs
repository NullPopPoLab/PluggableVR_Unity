/*!	@file
	@brief NullPopPoSpecial: 安全な数学関数 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial_Unity
*/
using UnityEngine;

namespace NullPopPoSpecial
{
	//! 安全な数学関数 
	/*!	@note 主に加減算の計算誤差でNaNが仕込まれる問題を防ぐ
	*/
	public static class SafeMathf
	{

		public static float Sqrt(float src)
		{
			if (src <= 0.0f) return 0.0f;
			return Mathf.Sqrt(src);
		}

		public static float Acos(float src)
		{
			if (src >= 1.0f) return 0.0f;
			if (src <= -1.0f) return Mathf.PI;
			return Mathf.Acos(src);
		}

		public static float Asin(float src)
		{
			if (src >= 1.0f) return Mathf.PI * 0.5f;
			if (src <= -1.0f) return -Mathf.PI * 0.5f;
			return Mathf.Asin(src);
		}
	}
}
