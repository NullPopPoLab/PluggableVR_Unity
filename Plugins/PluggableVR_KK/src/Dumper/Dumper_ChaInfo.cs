/*!	@file
	@brief HierarchyDumper: ChaInfo 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using HierarchyDumper;
using ActionGame;

namespace PluggableVR_KK
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
			s += indent + "Sex: " + _obj.sex + "\n";
			s += indent + DumpForm.Enumrate(_obj.objAccessory, "ObjAccessory", (i) => indent + "  " + i, (v) => DumpForm.From(v) + "\n");
			s += indent + DumpForm.Enumrate(_obj.objParts, "ObjParts", (i) => indent + "  " + i, (v) => DumpForm.From(v) + "\n");
			s += indent + DumpForm.Enumrate(_obj.objClothes, "ObjClothes", (i) => indent + "  " + i, (v) => DumpForm.From(v) + "\n");
			s += indent + DumpForm.Enumrate(_obj.objHair, "ObjHair", (i) => indent + "  " + i, (v) => DumpForm.From(v) + "\n");
			return s;
		}
	}
}
