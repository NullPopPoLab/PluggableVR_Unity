/*!	@file
	@brief HierarchyDumper: ChaControl 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using HierarchyDumper;
using AIChara;

namespace PluggableVR_HS2
{
	//! ChaControl 情報取得 
	public struct Dumper_ChaControl
	{
		private ChaControl _obj;

		public Dumper_ChaControl(object obj) { _obj = obj as ChaControl; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = new Dumper_ChaInfo(_obj).Dump(indent);

			return s;
		}
	}
}
