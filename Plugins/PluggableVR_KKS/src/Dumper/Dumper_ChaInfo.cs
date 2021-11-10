/*!	@file
	@brief HierarchyDumper: ChaInfo 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using HierarchyDumper;
using ActionGame;

namespace PluggableVR_KKS
{
	//! ChaInfo 情報取得 
	public struct Dumper_ChaInfo
	{
		private ChaInfo _obj;

		public Dumper_ChaInfo(object obj) { _obj = obj as ChaInfo; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "LoadNo: " + _obj.loadNo + "\n";
			s += indent + "ChaID: " + _obj.chaID + "\n";
			s += indent + "FullName: " + _obj.fileParam.fullname + "\n";

			return s;
		}
	}
}
