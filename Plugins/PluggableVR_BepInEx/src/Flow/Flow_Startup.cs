/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 開始時 
	internal class Flow_Startup : Flow_Common
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// Unityシーンロードに連動する遷移 
			Global.Transit["ADV"] = () => new Flow_ADV();
			Global.Transit["CharaCustom"] = () => new Flow_CharaCustom();
			Global.Transit["Home"] = () => new Flow_Home();
			Global.Transit["HScene"] = () => new Flow_HScene();
			Global.Transit["Title"] = () => new Flow_Title();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// メインカメラ生成待ち 
			Camera.Update();
			if (Camera.Camera == null) return null;

			// 操作開始 
			var scale = 8.0f;
			var sc = Camera.Camera;
			var loc = Loc.FromWorldTransform(sc.transform);
			var avatar = new DemoAvatar(loc, scale);
			var player = new DemoPlayer(avatar, scale);

			var mng = VRManager.Instance;
			mng.SetPlayer(player);

			// 手の軸表示を消す 
			avatar.LeftHand.SetActive(false);
			avatar.RightHand.SetActive(false);

			return new Flow_Title();
		}
	}
}