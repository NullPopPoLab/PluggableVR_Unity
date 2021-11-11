/*!	@file
	@brief HierarchyDumper: ActionGame.Chara.Player 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using HierarchyDumper;
using ActionGame;

namespace PluggableVR_KKS
{
	//! ActionGame.Chara.Player 情報取得 
	public struct Dumper_Chara_Player
	{
		private ActionGame.Chara.Player _obj;

		public Dumper_Chara_Player(object obj) { _obj = obj as ActionGame.Chara.Player; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = new Dumper_Chara_Base(_obj).Dump(indent);
			s += indent + "IsAction: " + _obj.isAction + "\n";
			s += indent + "StandPos: " + DumpForm.From(_obj.standPos) + "\n";

			return s;
		}
	}
}
