/*!	@file
	@brief NullPopPoSpecial: Rot2D 単体テスト 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NUnit.Framework;

namespace NullPopPoSpecial
{
	public class Rot2DTest : TestCommon
	{

		[Test]
		public void FromCycle()
		{

			// 回転量と度のインスタンス生成比較 
			for (var d = -1080; d <= 1080; ++d)
			{
				var r = Cycle.FromDegree(d);
				var t1 = Rot2D.FromCycle(r);
				var t2 = Rot2D.FromDegree(d);
				CompareLoose(t1.C, t2.C, "Rot2D.FromCycle(" + d + " deg).C");
				CompareLoose(t1.S, t2.S, "Rot2D.FromCycle(" + d + " deg).S");
			}

			// 直角から生成されるインスタンスで端数が出ないことを保証 
			Compare(Rot2D.FromCycle(Cycle.Right * -2).C, -1, "Rot2D.FromCycle(-2r).C");
			Compare(Rot2D.FromCycle(Cycle.Right * -2).S, 0, "Rot2D.FromCycle(-2r).S");
			Compare(Rot2D.FromCycle(Cycle.Right * -1).C, 0, "Rot2D.FromCycle(-1r).C");
			Compare(Rot2D.FromCycle(Cycle.Right * -1).S, -1, "Rot2D.FromCycle(-1r).S");
			Compare(Rot2D.FromCycle(Cycle.Right * 0).C, 1, "Rot2D.FromCycle(0r).C");
			Compare(Rot2D.FromCycle(Cycle.Right * 0).S, 0, "Rot2D.FromCycle(0r).S");
			Compare(Rot2D.FromCycle(Cycle.Right * 1).C, 0, "Rot2D.FromCycle(1r).C");
			Compare(Rot2D.FromCycle(Cycle.Right * 1).S, 1, "Rot2D.FromCycle(1r).S");
			Compare(Rot2D.FromCycle(Cycle.Right * 2).C, -1, "Rot2D.FromCycle(2r).C");
			Compare(Rot2D.FromCycle(Cycle.Right * 2).S, 0, "Rot2D.FromCycle(2r).S");
			Compare(Rot2D.FromCycle(Cycle.Right * 3).C, 0, "Rot2D.FromCycle(3r).C");
			Compare(Rot2D.FromCycle(Cycle.Right * 3).S, -1, "Rot2D.FromCycle(3r).S");
			Compare(Rot2D.FromCycle(Cycle.Right * 4).C, 1, "Rot2D.FromCycle(4r).C");
			Compare(Rot2D.FromCycle(Cycle.Right * 4).S, 0, "Rot2D.FromCycle(4r).S");
		}

		[Test]
		public void FromDegree()
		{

			// 度からのインスタンス生成 
			for (var d = -360; d <= 360; ++d)
			{
				var r = Mathf.Deg2Rad * d;
				var c = Mathf.Cos(r);
				var s = Mathf.Sin(r);
				var t = Rot2D.FromDegree(d);
				CompareLoose(t.C, c, "Rot2D.FromDegree(" + d + " deg).C");
				CompareLoose(t.S, s, "Rot2D.FromDegree(" + d + " deg).S");
			}
		}

		[Test]
		public void FromCosine()
		{

			// コサイン値からのインスタンス生成 
			for (var d = 0; d <= 180; ++d)
			{
				var r = Mathf.Deg2Rad * d;
				var c = Mathf.Cos(r);
				var s = Mathf.Sin(r);
				var t = Rot2D.FromCosine(c);
				CompareLoose(t.C, c, "Rot2D.FromCosine(" + d + " deg).S");
				CompareLoose(t.S, s, "Rot2D.FromCosine(" + d + " deg).S");
			}
		}

		[Test]
		public void Double()
		{

			// 倍角 
			for (var d = -360; d <= 360; ++d)
			{
				var t1 = Rot2D.FromDegree(d * 2);
				var t2 = Rot2D.FromDegree(d).Double;
				CompareLoose(t1.C, t2.C, "Rot2D(" + d + " deg).Double.C");
				CompareLoose(t1.S, t2.S, "Rot2D(" + d + " deg).Double.S");
			}
		}

		[Test]
		public void Half()
		{

			// 半角 
			// (注) ±180°を超えると、半分にしてどちらに寄せるか問題
			//		により結果がややこしいことになる。
			// (注) ±180°ぴったりのつもりでも、微妙な計算誤差で
			//		結果が二分されるため、これまたややこしい。
			for (var d = -179; d < 180; ++d)
			{
				var t1 = Rot2D.FromDegree(d * 0.5f);
				var t2 = Rot2D.FromDegree(d).Half;
				CompareLoose(t1.C, t2.C, "Rot2D(" + d + " deg).Half.C");
				CompareLoose(t1.S, t2.S, "Rot2D(" + d + " deg).Half.S");
			}
		}
	}
}
