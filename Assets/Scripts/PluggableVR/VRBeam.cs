/*!	@file
	@brief PluggableVR: VR操作ビーム 基底 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

//! VR操作ビーム 
public class VRBeam : PlugCommon
{
	private bool _southpaw;
	public bool Southpaw
	{
		get { return _southpaw; }
		set
		{
			if (value == _southpaw) return;
			_southpaw = value;
			Change();
		}
	}

	public VRBeam()
	{
	}

	public void Change()
	{

		var vrmng = VRManager.Instance;
		var vrgui = vrmng.Input.GUI;
		var avatar = vrmng.Avatar as DemoAvatar;

		if (_southpaw)
		{
			vrgui.SetPointer(avatar.LeftHand.Raycaster.transform);
		}
		else
		{
			vrgui.SetPointer(avatar.RightHand.Raycaster.transform);
		}
	}

	public void Update(){
		OnUpdate();
	}

	protected virtual void OnUpdate(){ }
}
