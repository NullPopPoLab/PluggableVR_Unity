/*!	@file
	@brief PluggableVR: VR操作先 基底 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

//! VR操作先 
public class VRAvatar : PlugCommon
{
	public int Layer{ get; private set; }

	public VRAvatar()
	{
	}

	//! 表示レイヤ設定 
	public void SetLayer(int layer)
	{
		if (layer == Layer) return;
		Layer = layer;
		OnChangeLayer(layer);
	}
	protected virtual void OnChangeLayer(int layer){}
}
