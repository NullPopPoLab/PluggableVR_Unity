/*!	@file
	@brief NullPopPoSpecial: 手順進行 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/

namespace NullPopPoSpecial
{
	//! 手順進行 
	public class FlowStep
	{
		private FlowBase _cur;

		~FlowStep() { Terminate(); }

		public void Start(FlowBase flow)
		{
			Terminate();
			if (flow == null) return;
			_cur = flow;
			flow.Start();
		}

		public void Terminate()
		{
			if (_cur == null) return;
			_cur.Terminate();
			_cur = null;
		}

		public void Update()
		{
			if (_cur == null) return;
			_cur = _cur.Update();
		}
	}
}
