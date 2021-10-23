/*!	@file
	@brief PluggableVR: 手順遷移 シーンロード検知後 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using PluggableVR;
using NullPopPoSpecial;

namespace PluggableVR.CS
{
	//! 手順遷移 シーンロード検知後 
	public class Flow_SceneLoaded : Flow
	{
		private Loc _cameraLoc;

		protected override void OnStart()
		{
			_cameraLoc = Loc.FromWorldTransform(Global.MainCamera.transform);
		}

		protected override Flow OnUpdate()
		{
			// メインカメラ位置変更を待って位置リセット 
			var loc = Loc.FromWorldTransform(Global.MainCamera.transform);
			if (loc.Pos == _cameraLoc.Pos && loc.Rot == _cameraLoc.Rot) return null;

			VRManager.Instance.Reloc(loc);
			return new Flow_Edit();
		}
	}
}