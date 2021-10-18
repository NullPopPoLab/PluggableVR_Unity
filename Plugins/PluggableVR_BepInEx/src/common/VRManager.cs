/*!	@file
	@brief PluggableVR: VRシステム管理 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PluggableVR
{
	public class VRManager: PlugCommon
	{
		internal static VRManager Instance;

		private Camera _prevMainCamera;

		internal VRManager(){
		}

		public void SceneChanged(Scene scn){

			if(VRController.Instance!=null)VRController.Instance.SceneChanged(scn);
		}

		private void _switchVRController(){

			// メインカメラが捕捉できないうちは何もしない 
			var mc=Camera.main;
			if(mc==null)return;

			if(VRController.Instance==null){
				// ここでVR操作開始可 
				VRController.Instance=new VRController(mc);
			}
			else if(mc!=_prevMainCamera){
				// メインカメラ変更捕捉 
				_prevMainCamera=mc;
				VRController.Instance.MainCameraChanged(mc);
			}
		}

		public void Update(){
			_switchVRController();
		}
	}
}
