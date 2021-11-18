/*!	@file
	@brief HierarchyDumper: ObiFluidRenderer 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using HierarchyDumper;
using AIChara;

namespace PluggableVR_HS2
{
	//! ObiFluidRenderer 情報取得 
	public struct Dumper_ObiFluidRenderer
	{
		private Obi.ObiFluidRenderer _obj;

		public Dumper_ObiFluidRenderer(object obj) { _obj = obj as Obi.ObiFluidRenderer; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "BlurRadius: " + _obj.blurRadius + "\n";
			s += indent + "ThicknessCutoff: " + _obj.thicknessCutoff + "\n";
			s += indent + "ColorMaterial: " + DumpForm.From(_obj.colorMaterial) + "\n";
			s += indent + "FluidMaterial: " + DumpForm.From(_obj.fluidMaterial) + "\n";

			return s;
		}
	}
}
