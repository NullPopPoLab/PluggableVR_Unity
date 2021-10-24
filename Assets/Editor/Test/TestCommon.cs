/*!	@file
	@brief PluggableVR: 単体テスト共通部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace NullPopPoSpecial
{
	public class TestCommon
	{

		//! 一致比較 
		public void Compare<T>(T a, T b, string msg = "")
		{
			if (msg != "") msg += "; " + a + "/" + b;
			UnityEngine.Assertions.Assert.AreEqual(a, b, msg);
		}

		//! 小数近似比較 
		public void CompareLoose(float a, float b, string msg = "")
		{
			if (msg != "") msg += "; " + a + "/" + b;
			UnityEngine.Assertions.Assert.AreApproximatelyEqual(a, b, msg);
		}

		//! 小数近似比較 閾値指定 
		public void CompareLoose(float a, float b, float thr, string msg = "")
		{
			if (msg != "") msg += "; " + a + "/" + b;
			UnityEngine.Assertions.Assert.AreApproximatelyEqual(a, b, thr, msg);
		}

		//! 3次元ベクトル近似比較 
		public void CompareLoose(Vector3 a, Vector3 b, string msg = "")
		{
			CompareLoose(a.x, b.x, (msg == "") ? "" : (msg + ".X"));
			CompareLoose(a.y, b.y, (msg == "") ? "" : (msg + ".Y"));
			CompareLoose(a.z, b.z, (msg == "") ? "" : (msg + ".Z"));
		}

		//! 3次元ベクトル近似比較 閾値指定 
		public void CompareLoose(Vector3 a, Vector3 b, float thr, string msg = "")
		{
			CompareLoose(a.x, b.x, thr, (msg == "") ? "" : (msg + ".X"));
			CompareLoose(a.y, b.y, thr, (msg == "") ? "" : (msg + ".Y"));
			CompareLoose(a.z, b.z, thr, (msg == "") ? "" : (msg + ".Z"));
		}

		//! クォータニオン近似比較 
		public void CompareLoose(Quaternion a, Quaternion b, string msg = "")
		{
			CompareLoose(RotUt.AxisX(a), RotUt.AxisX(b), (msg == "") ? "" : (msg + ".AxisX"));
			CompareLoose(RotUt.AxisY(a), RotUt.AxisY(b), (msg == "") ? "" : (msg + ".AxisY"));
			CompareLoose(RotUt.AxisZ(a), RotUt.AxisZ(b), (msg == "") ? "" : (msg + ".AxisZ"));
		}

		//! クォータニオン近似比較 閾値指定 
		public void CompareLoose(Quaternion a, Quaternion b, float thr, string msg = "")
		{
			CompareLoose(RotUt.AxisX(a), RotUt.AxisX(b), thr, (msg == "") ? "" : (msg + ".AxisX"));
			CompareLoose(RotUt.AxisY(a), RotUt.AxisY(b), thr, (msg == "") ? "" : (msg + ".AxisY"));
			CompareLoose(RotUt.AxisZ(a), RotUt.AxisZ(b), thr, (msg == "") ? "" : (msg + ".AxisZ"));
		}
	}
}
