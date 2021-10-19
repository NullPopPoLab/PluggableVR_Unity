/*!	@file
	@brief PluggableVR: VRプラグ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

//! VRプラグ 
public class VRPlug : MonoBehaviour
{
	private PluggableVR.VRManager _vrmng=new PluggableVR.VRManager();

	protected void Awake(){

		_vrmng.Initialize();
	}

	protected void FixedUpdate(){

		_vrmng.FixedUpdate();
	}

	protected void Update(){

		_vrmng.Update();
	}

	protected void LateUpdate(){

		_vrmng.LateUpdate();
	}
}
