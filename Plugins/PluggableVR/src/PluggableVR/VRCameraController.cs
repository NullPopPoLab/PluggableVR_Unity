/*!	@file
	@brief PluggableVR: VRカメラ操作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using System.Collections.Generic;
using NullPopPoSpecial;

namespace PluggableVR
{
	//! VRカメラ操作 
	public class VRCameraController : GameObjectScope
	{
		public enum EPostproc
		{
			None,
			Feedback,
			Reloc,
		}
		public EPostproc Postproc { get; protected set; }

		public Loc CameraPlace { get; protected set; }

		protected override void OnTerminate()
		{
			Postproc = EPostproc.None;
			base.OnTerminate();
		}
	}

	//! VRカメラ側を主とする基本動作 
	/*!	@note VRカメラ位置を元カメラに書き戻す
	*/
	public class VRCameraActive : VRCameraController
	{

		protected override void OnStart()
		{
			base.OnStart();
			Postproc = EPostproc.Feedback;
		}
	}

	//! VRカメラ側を従とする基本動作 
	/*!	@note 元カメラの移動が止まった時点でVRカメラを移動させる
	*/
	public class VRCameraPassive : VRCameraController
	{

		private Chaser _chaser;

		protected override void OnStart()
		{
			base.OnStart();
			_chaser = new Chaser(Transform);
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();
			Postproc = _chaser.Update() ? EPostproc.Reloc : EPostproc.None;
			CameraPlace = _chaser.Loc;
		}
	}
}
