/*!	@file
	@brief HierarchyDumper: ActionGame.Chara.Base 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using HierarchyDumper;
using ActionGame;

namespace PluggableVR_KKS
{
	//! ActionGame.Chara.Base 情報取得 
	public struct Dumper_Chara_Base
	{
		private ActionGame.Chara.Base _obj;

		public Dumper_Chara_Base(object obj) { _obj = obj as ActionGame.Chara.Base; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "Initialized: " + _obj.initialized + "\n";
			s += indent + "IsActive: " + _obj.isActive + "\n";

			return s;
		}
	}
}
