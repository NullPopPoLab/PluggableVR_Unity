/*!	@file
	@brief HierarchyDumper: HomeMenu 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using HierarchyDumper;
using ActionGame;

namespace PluggableVR_KKS
{
	//! HomeMenu 情報取得 
	public struct Dumper_HomeMenu
	{
		private HomeMenu _obj;

		public Dumper_HomeMenu(object obj) { _obj = obj as HomeMenu; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "IsEnd: " + _obj.isEnd + "\n";
			s += indent + "Visible: " + _obj.visible + "\n";

			return s;
		}
	}
}
