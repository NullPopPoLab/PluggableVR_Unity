/*!	@file
	@brief NullPopPoSpecial: 変更捕捉 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/

namespace NullPopPoSpecial
{
	//! 変更捕捉 
	public struct ChangingCensor<T>
	{
		public T Current { get; private set; }

		public ChangingCensor(T init = default(T))
		{
			Current = init;
		}

		public void Reset(T val)
		{
			Current = val;
		}

		public bool Update(T val)
		{
			var f = !Current.Equals(val);
			Current = val;
			return f;
		}
	}
}
