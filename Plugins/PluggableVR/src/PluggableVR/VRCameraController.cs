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
	public class VRCameraController : HierarchyScope
	{
		public enum EPostproc
		{
			None,
			Feedback,
			Reloc,
		}
		public EPostproc Postproc { get; protected set; }

		protected override void OnTerminate()
		{
			Postproc = EPostproc.None;
			base.OnTerminate();
		}
	}

	//! VRカメラ側を主とする基本動作 
	public class VRCameraActive : VRCameraController
	{

		protected override void OnStart()
		{
			base.OnStart();
			Postproc = EPostproc.Feedback;
		}
	}

	//! VRカメラ側を従とする基本動作 
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
		}
	}
}
