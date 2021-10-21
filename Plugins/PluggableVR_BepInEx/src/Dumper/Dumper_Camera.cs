﻿/*!	@file
	@brief PluggableVR: Camera 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace PluggableVR
{
	//! Camera 情報取得 
	public struct Dumper_Camera
	{
		private Camera _obj;

		public Dumper_Camera(object obj) { _obj = obj as Camera; }

		public string Dump(string indent = "")
		{

			if (_obj == null) return "!!! Type Mismatch !!!\n";

			var s = "";
			s += indent + "ClearFlags: " + _obj.clearFlags + "\n";
			s += indent + "CullingMask: " + _obj.cullingMask.ToString("X") + "\n";
			s += indent + "ClipPlane: " + _obj.nearClipPlane + "~" + _obj.farClipPlane + "\n";
			s += indent + "TargetDisplay: " + _obj.targetDisplay + "\n";

			return s;
		}
	}
}