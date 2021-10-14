/*!	@file
	@brief PluggableVR: VR操作先 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

//! VR操作先 
public class VRAvatar : MonoBehaviour
{
	[SerializeField, Tooltip("頭(表示用)")]
	public Transform Head;
	[SerializeField, Tooltip("目位置")]
	public Transform Eye;
	[SerializeField, Tooltip("左手位置")]
	public Transform LeftHand;
	[SerializeField, Tooltip("右手位置")]
	public Transform RightHand;

	//! 操作構造生成 
	/*!	@note 位置参照の正確性および書き込み手順の正当性を確保するため、
			直接transformへのアクセスをさせるべきではない。
	*/
	public PluggableVR.AvatarControl CreateControl()
	{
		var t = new PluggableVR.AvatarControl();
		t.Origin = PluggableVR.Loc.FromWorldTransform(transform);
		var inv = t.Origin.Inversed;
		t.LocalEye = inv * PluggableVR.Loc.FromWorldTransform(Eye);
		t.LocalLeftHand = inv * PluggableVR.Loc.FromWorldTransform(LeftHand);
		t.LocalRightHand = inv * PluggableVR.Loc.FromWorldTransform(RightHand);
		return t;
	}

	//! 操作構造反映 
	public void UpdateControl(PluggableVR.AvatarControl cs)
	{
		cs.Origin.ToWorldTransform(transform);
		cs.LocalEye.ToLocalTransform(Eye);
		cs.LocalLeftHand.ToLocalTransform(LeftHand);
		cs.LocalRightHand.ToLocalTransform(RightHand);
	}
}
