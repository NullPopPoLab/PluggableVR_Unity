/*!	@file
	@brief NullPopPoSpecial: 変化捕捉 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/

namespace NullPopPoSpecial
{
	//! 整数変化捕捉 
	public class RelativeInt32
	{
		//! 現在値 
		public int Current { get; private set; }
		//! 前回からの変化量 
		public int Delta { get; private set; }

		public RelativeInt32(int val = 0)
		{
			Current = val;
			Delta = 0;
		}

		//! 通常の現在値更新 
		public int Update(int val)
		{
			Delta = val - Current;
			Current = val;
			return Delta;
		}
	}

	//! 整数変化捕捉 
	public class RelativeInt64
	{
		//! 現在値 
		public long Current { get; private set; }
		//! 前回からの変化量 
		public long Delta { get; private set; }

		public RelativeInt64(long val = 0)
		{
			Current = val;
			Delta = 0;
		}

		//! 現在値更新 
		public long Update(long val)
		{
			Delta = val - Current;
			Current = val;
			return Delta;
		}
	}

	//! 実数変化捕捉 
	public class RelativeReal32
	{
		//! 現在値 
		public float Current { get; private set; }
		//! 前回からの変化量 
		public float Delta { get; private set; }

		public RelativeReal32(float val = 0)
		{
			Current = val;
			Delta = 0;
		}

		//! 現在値更新 
		public float Update(float val)
		{
			Delta = val - Current;
			Current = val;
			return Delta;
		}
	}

	//! 実数変化捕捉 
	public class RelativeReal64
	{
		//! 現在値 
		public double Current { get; private set; }
		//! 前回からの変化量 
		public double Delta { get; private set; }

		public RelativeReal64(double val = 0)
		{
			Current = val;
			Delta = 0;
		}

		//! 現在値更新 
		public double Update(double val)
		{
			Delta = val - Current;
			Current = val;
			return Delta;
		}
	}

	//! 二値変化捕捉 
	public class RelativeBool : RelativeInt32
	{
		public RelativeBool(bool val = false) :
			base(val ? 1 : 0)
		{ }

		//! 現在値更新 
		public int Update(bool val)
		{
			return base.Update(val ? 1 : 0);
		}

		//! 整数の現在値をboolとして扱う更新 
		public new int Update(int val)
		{
			return base.Update((val == 0) ? 0 : 1);
		}
	}
}
