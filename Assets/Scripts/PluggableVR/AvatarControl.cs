/*!	@file
	@brief PluggableVR: VR操作先 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;

namespace PluggableVR
{
	//! VR操作先 
	public class AvatarControl
	{
		public Loc Origin; //!< 移動基準点(主に足元) 
		public Loc LocalEye; //!< 基準点からの目位置差分 
		public Loc LocalLeftHand; //!< 基準点からの左手位置差分 
		public Loc LocalRightHand; //!< 基準点からの右手位置差分 

		//! ワールド目位置 
		public Loc WorldEye
		{
			get { return Origin * LocalEye; }
			set { LocalEye = Origin.Inversed*value; }
		}
		//! ワールド左手位置 
		public Loc WorldLeftHand
		{
			get { return Origin * LocalLeftHand; }
			set { LocalLeftHand = Origin.Inversed*value; }
		}
		//! ワールド右手位置 
		public Loc WorldRightHand
		{
			get { return Origin * LocalRightHand; }
			set { LocalRightHand = Origin.Inversed*value; }
		}
	}
}
