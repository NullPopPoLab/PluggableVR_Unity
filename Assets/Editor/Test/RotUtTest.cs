/*!	@file
	@brief PluggableVR: RotUt 単体テスト 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NUnit.Framework;

namespace PluggableVR
{

	public class RotUtTest : TestCommon
	{

		private Quaternion _testSubj
		{
			get
			{
				return Quaternion.Euler(50, 100, 200);
			}
		}

		[Test]
		public void Axis()
		{

			// 回転軸の向き計算 
			var q = _testSubj;
			CompareLoose(RotUt.AxisX(q), q * new Vector3(1, 0, 0), "AxisX");
			CompareLoose(RotUt.AxisY(q), q * new Vector3(0, 1, 0), "AxisY");
			CompareLoose(RotUt.AxisZ(q), q * new Vector3(0, 0, 1), "AxisZ");
		}

		[Test]
		public void RotX()
		{

			// X軸回転計算 
			for (var d = -360; d < 360; ++d)
			{
				var r = Mathf.PI / 180.0f * d;
				var q1 = RotUt.RotX(r);
				var q2 = RotUt.RotX(Rot2D.FromDegree(d));
				CompareLoose(q1, q2, "RotX(" + d + ")");
			}
		}

		[Test]
		public void RotY()
		{

			// Y軸回転計算 
			for (var d = -360; d < 360; ++d)
			{
				var r = Mathf.PI / 180.0f * d;
				var q1 = RotUt.RotY(r);
				var q2 = RotUt.RotY(Rot2D.FromDegree(d));
				CompareLoose(q1, q2, "RotY(" + d + ")");
			}
		}

		[Test]
		public void RotZ()
		{

			// Z軸回転計算 
			for (var d = -360; d < 360; ++d)
			{
				var r = Mathf.PI / 180.0f * d;
				var q1 = RotUt.RotZ(r);
				var q2 = RotUt.RotZ(Rot2D.FromDegree(d));
				CompareLoose(q1, q2, "RotZ(" + d + ")");
			}
		}

		[Test]
		public void RotAny()
		{

			var q = _testSubj;

			// 任意軸回転計算 
			for (var d = -360; d < 360; ++d)
			{
				var r = Mathf.PI / 180.0f * d;
				var q1 = RotUt.Rot(RotUt.AxisX(q), r);
				var q2 = RotUt.Rot(RotUt.AxisX(q), Rot2D.FromDegree(d));
				CompareLoose(q1, q2, "RotAnyX(" + d + ")");

				q1 = RotUt.Rot(RotUt.AxisY(q), r);
				q2 = RotUt.Rot(RotUt.AxisY(q), Rot2D.FromDegree(d));
				CompareLoose(q1, q2, "RotAnyY(" + d + ")");

				q1 = RotUt.Rot(RotUt.AxisZ(q), r);
				q2 = RotUt.Rot(RotUt.AxisZ(q), Rot2D.FromDegree(d));
				CompareLoose(q1, q2, "RotAnyZ(" + d + ")");
			}
		}

		[Test]
		public void ReturnX()
		{

			var q1 = _testSubj;
			var q2 = RotUt.ReturnX(q1);

			// X軸を元に戻す 
			CompareLoose(RotUt.AxisX(q2), new Vector3(1, 0, 0), "ReturnX");
		}

		[Test]
		public void ReturnY()
		{

			var q1 = _testSubj;
			var q2 = RotUt.ReturnY(q1);

			// Y軸を元に戻す 
			CompareLoose(RotUt.AxisY(q2), new Vector3(0, 1, 0), "ReturnY");
		}

		[Test]
		public void ReturnZ()
		{

			var q1 = _testSubj;
			var q2 = RotUt.ReturnZ(q1);

			// Z軸を元に戻す 
			CompareLoose(RotUt.AxisZ(q2), new Vector3(0, 0, 1), "ReturnZ");
		}
	}
}
