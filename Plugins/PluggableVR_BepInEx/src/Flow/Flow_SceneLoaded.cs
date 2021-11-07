/*!	@file
	@brief PluggableVR: 手順遷移 シーンロード検知後 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.CS2
{
	//! 手順遷移 シーンロード検知後 
	internal class Flow_SceneLoaded : Flow
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
			// 元カメラ位置変更待ち 
			var mng = VRManager.Instance;
			var player = mng.Player;
			var cam = player.Camera;
			var loc = Loc.FromWorldTransform(cam.Source.transform);
			if (loc == _cameraLoc) return null;

			// 元カメラ位置に移動 
			VRManager.Instance.Reloc(Loc.FromWorldTransform(cam.Source.transform));

			return new Flow_Edit();
		}
	}
}