/*!	@file
	@brief HierarchyDumper: Camera 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace HierarchyDumper
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
			s += indent + "CameraType: " + _obj.cameraType + "\n";
			s += indent + "StereoTargetEye: " + _obj.stereoTargetEye + "\n";
			s += indent + "ClearFlags: " + _obj.clearFlags + "\n";
			s += indent + "CullingMask: " + _obj.cullingMask.ToString("X") + "\n";
			s += indent + "ClipPlane: " + _obj.nearClipPlane + "~" + _obj.farClipPlane + "\n";
			s += indent + "Depth: " + _obj.depth + "\n";
			s += indent + "FieldOfView: " + _obj.fieldOfView + "\n";
			s += indent + "PixelRect: " + _obj.pixelRect + "\n";
			s += indent + "UseOcclusionCulling: " + _obj.useOcclusionCulling + "\n";
			s += indent + "TargetDisplay: " + _obj.targetDisplay + "\n";
			s += indent + "TargetTexture: " + ((_obj.targetTexture == null) ? "None" : _obj.targetTexture.ToString()) + "\n";

			return s;
		}
	}
}
