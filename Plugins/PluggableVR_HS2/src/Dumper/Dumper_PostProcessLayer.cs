/*!	@file
	@brief HierarchyDumper: PostProcessLayer 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using HierarchyDumper;

namespace PluggableVR_HS2
{
	//! PostProcessLayer 情報取得 
	public struct Dumper_PostProcessLayer
	{
		private UnityEngine.Rendering.PostProcessing.PostProcessLayer _obj;

		public Dumper_PostProcessLayer(object obj) { _obj = obj as UnityEngine.Rendering.PostProcessing.PostProcessLayer; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "VolumeLayer: " + _obj.volumeLayer.value + "\n";
			s += indent + "AntialiasingMode: " + _obj.antialiasingMode + "\n";
			s += indent + "BreakBeforeColorGrading: " + _obj.breakBeforeColorGrading + "\n";
			s += indent + "FinalBlitToCameraTarget: " + _obj.finalBlitToCameraTarget + "\n";
			s += indent + "Fog Enabled: " + _obj.fog.enabled + "\n";
			s += indent + "Fog ExcludeSkybox: " + _obj.fog.excludeSkybox + "\n";

			return s;
		}
	}
}
