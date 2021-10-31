/*!	@file
	@brief PluggableVR: 手順遷移 シーン初期化確認ダイアログ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.CS
{
	//! 手順遷移 シーン初期化確認ダイアログ
	public class Flow_ScenePurging : Flow
	{
		private Loc _cameraLoc;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");

			var mng = VRManager.Instance;
			var player = mng.Player;
			var cam = player.Camera;
			_cameraLoc = Loc.FromWorldTransform(cam.Source.transform);

			base.OnStart();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			// 初期化せずに抜けたケースの検知 
			var gobj=GameObject.Find("/CheckScene");
			if(gobj==null)return new Flow_Edit();

			// 元カメラ位置変更を待って位置リセット 
			var mng = VRManager.Instance;
			var player = mng.Player;
			var cam = player.Camera;
			var loc = Loc.FromWorldTransform(cam.Source.transform);
			if (loc == _cameraLoc) return null;

			VRManager.Instance.Reloc(loc);
			return new Flow_Edit();
		}
	}
}