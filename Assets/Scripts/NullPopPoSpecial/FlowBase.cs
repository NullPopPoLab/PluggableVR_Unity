/*!	@file
	@brief NullPopPoSpecial: 手順遷移 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace NullPopPoSpecial
{
	//! 手順遷移 
	public class FlowBase
	{
		public bool IsBusy { get; private set; }

		//! 開始 
		public void Start()
		{
			if (IsBusy) return;
			IsBusy = true;
			OnStart();
		}

		//! 終了 
		public void Terminate()
		{
			if (!IsBusy) return;
			IsBusy = false;
			OnTerminate();
		}

		//! 更新 
		public FlowBase Update()
		{
			if (!IsBusy) return null;
			var next = OnUpdate();
			if (!IsBusy) return null;
			if (next == null) return this;
			Terminate();
			next.Start();
			return next;
		}

		//! 開始動作 
		protected virtual void OnStart() { }

		//! 終端動作 
		protected virtual void OnTerminate() { }

		//! 更新動作 
		/*!	@return 次の手順 (null=継続)
		*/
		protected virtual FlowBase OnUpdate() { return null; }
	}
}
