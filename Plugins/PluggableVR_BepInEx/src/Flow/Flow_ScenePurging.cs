/*!	@file
	@brief PluggableVR: 手順遷移 シーン初期化確認ダイアログ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.SN2
{
	//! 手順遷移 シーン初期化確認ダイアログ
	public class Flow_ScenePurging : Flow
	{
		private Loc _cameraLoc;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");

			_cameraLoc = Loc.FromWorldTransform(Global.MainCamera.transform);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
		}

		protected override Flow OnUpdate()
		{
			// 初期化せずに抜けたケースの検知 
			var gobj=GameObject.Find("/CheckScene");
			if(gobj==null)return new Flow_Edit();

			// メインカメラ位置変更を待って位置リセット 
			var loc = Loc.FromWorldTransform(Global.MainCamera.transform);
			if (loc.Pos == _cameraLoc.Pos && loc.Rot == _cameraLoc.Rot) return null;

			VRManager.Instance.Reloc(loc);
			return new Flow_Edit();
		}
	}
}