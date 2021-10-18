/*!	@file
	@brief PluggableVR: VR操作部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PluggableVR
{
	public class VRController: PlugCommon
	{
		internal static VRController Instance;

		private Camera _mainCamera;
		private VRPlayer _player;
		private VRAvatar _avatar;

		internal VRController(Camera mc){

			_mainCamera = mc;

			var loc=Loc.FromWorldTransform(mc.transform);
			_avatar=new VRAvatar(loc);
			_player=new VRPlayer(loc, _avatar);

			var cam=mc.GetComponent<Camera>();
			if(cam!=null)cam.enabled=false;
			var lsn=mc.GetComponent<AudioListener>();
			if(lsn!=null)lsn.enabled=false;

Hierarchy.Dump2File("Hierarchy","VRControllerCreated");
		}

		//! シーン変更捕捉 
		public void SceneChanged(Scene scn){

			// 現在のメインカメラ位置でリセット 
		}

		//! メインカメラ変更捕捉 
		public void MainCameraChanged(Camera mc){

			// 変更されたメインカメラ位置でリセット 
		}
	}
}
