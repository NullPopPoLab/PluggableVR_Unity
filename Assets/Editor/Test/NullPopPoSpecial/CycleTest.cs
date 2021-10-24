/*!	@file
	@brief NullPopPoSpecial: Cycle 単体テスト 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NUnit.Framework;

namespace NullPopPoSpecial
{
	public class CycleTest : TestCommon
	{

		[Test]
		public void FromDegree()
		{

			var threshold = 0.0001f;

			// 度からのインスタンス生成 
			// (ラジアンより精度が低い模様) 
			for (var d = -1000; d <= 1000; ++d)
			{
				CompareLoose(d, Cycle.FromDegree(d).Degree, threshold, "Cycle.FromDegree(" + d + " deg)");
			}
		}

		[Test]
		public void FromRadian()
		{

			// ラジアンからのインスタンス生成 
			for (var d = -1000; d <= 1000; ++d)
			{
				var r = Mathf.Deg2Rad * d;
				CompareLoose(r, Cycle.FromRadian(r).Radian, "Cycle.FromRadian(" + d + " deg)");
			}
		}
	}
}
