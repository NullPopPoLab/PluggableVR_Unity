/*!	@file
	@brief PluggableVR: SafeMathf 単体テスト 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NUnit.Framework;

namespace NullPopPoSpecial
{
	public class SafeMathfTest : TestCommon
	{

		[Test]
		public void Sqrt()
		{
			// 有効範囲補正 
			Compare(Mathf.Sqrt(0), SafeMathf.Sqrt(-0.1f));
		}

		[Test]
		public void Acos()
		{
			// 有効範囲補正 
			Compare(Mathf.Acos(1), SafeMathf.Acos(1.1f));
			Compare(Mathf.Acos(-1), SafeMathf.Acos(-1.1f));
		}

		[Test]
		public void Asin()
		{
			// 有効範囲補正 
			Compare(Mathf.Asin(1), SafeMathf.Asin(1.1f));
			Compare(Mathf.Asin(-1), SafeMathf.Asin(-1.1f));
		}
	}
}
