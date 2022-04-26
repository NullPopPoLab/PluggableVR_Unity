/*!	@file
	@brief PluggableVR: デモ用VR操作ビーム 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using NullPopPoSpecial;

namespace PluggableVR
{
	//! デモ用VR操作ビーム 
	public class DemoBeam: VRBeam
	{
		private RelativeBool _chgtrig = new RelativeBool();
		private RelativeBool _reloctrig = new RelativeBool();

		public DemoBeam(){
		}

		protected override void OnUpdate() {
			base.OnUpdate();

			var vrmng = VRManager.Instance;
			var input = vrmng.Input;
			var h1 = Southpaw ? input.HandLeft : input.HandRight;
			var h2 = Southpaw ? input.HandRight : input.HandLeft;
			if (_reloctrig.Update(h1.IsButton2Pressed()) > 0)
			{
				input.GUI.Relocate();
			}
			if (_chgtrig.Update(h2.IsButton1Pressed()) > 0)
			{
				input.Southpaw = Southpaw = !Southpaw;
			}
		}
	}
}
