﻿/*!	@file
	@brief PluggableVR: VR操作元 基底 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR
{
	//! VR操作元 
	public class VRPlayer : PlugCommon
	{
		//! カメラ 
		public VRCamera Camera { get; protected set; }
		//! 操作先 
		public VRAvatar Avatar { get; protected set; }

		public VRPlayer()
		{
		}
		public void Update() { OnUpdate(); }
		public void Repos(Vector3 pos) { OnRepos(pos); }
		public void Rerot(Quaternion rot) { OnRerot(rot); }
		public void Reloc(Loc loc) { OnReloc(loc); }

		public void SetCamera(Camera cam) {
			Camera.Reset(cam);
			if (cam == null) return;
			Reloc(Loc.FromWorldTransform(cam.transform)); 
		}

		protected virtual void OnUpdate() { }
		protected virtual void OnRepos(Vector3 pos) { }
		protected virtual void OnRerot(Quaternion rot) { }
		protected virtual void OnReloc(Loc loc) { }
	}
}