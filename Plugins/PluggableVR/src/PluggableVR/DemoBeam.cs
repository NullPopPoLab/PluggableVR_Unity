/*!	@file
	@brief PluggableVR: デモ用VR操作ビーム 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using NullPopPoSpecial;

namespace PluggableVR
{
	//! デモ用VR操作ビーム 
	public class DemoBeam
	{
		private bool _southpaw;
		public bool Southpaw{
			get{return _southpaw;}
			set{
				if(value==_southpaw)return;
				_southpaw=value;
				Change();
			}
		}

		public DemoBeam(bool southpaw){
			_southpaw=southpaw;
			Change();
		}

		public void Change(){

			var vrmng=VRManager.Instance;
			var vrgui = vrmng.Input.GUI;
			var avatar=vrmng.Avatar as DemoAvatar;

			if(_southpaw){
				vrgui.SetPointer(avatar.LeftHand.Raycaster.transform);
			}
			else{
				vrgui.SetPointer(avatar.RightHand.Raycaster.transform);
			}
		}
	}
}
