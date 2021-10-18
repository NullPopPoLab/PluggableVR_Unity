/*!	@file
	@brief PluggableVR: VR操作元 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PluggableVR
{
	//! VR操作元 
	public class VRPlayer : PlugCommon
	{
		private Transform _rig;
		private Transform _cam;

		internal VRPlayer(Loc loc, VRAvatar target)
		{

			_rig = CreateRootObject("VRPlayer", loc).transform;
			_cam = CreateChildObject("VRCamera", _rig, Loc.Identity, false).transform;
			GameObject.DontDestroyOnLoad(_rig.gameObject);

			_cam.gameObject.AddComponent<Camera>();
			_cam.gameObject.AddComponent<AudioListener>();
			_rig.gameObject.AddComponent<OVRManager>();
		}
	}
}
