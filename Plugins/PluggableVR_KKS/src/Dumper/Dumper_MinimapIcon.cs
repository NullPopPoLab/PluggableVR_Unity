/*!	@file
	@brief HierarchyDumper: MinimapIcon 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using HierarchyDumper;
using ActionGame;

namespace PluggableVR_KKS
{
	//! MinimapIcon 情報取得 
	public struct Dumper_MinimapIcon
	{
		private MinimapIcon _obj;

		public Dumper_MinimapIcon(object obj) { _obj = obj as MinimapIcon; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "Size: " + _obj.Size+ "\n";

			return s;
		}
	}
}
