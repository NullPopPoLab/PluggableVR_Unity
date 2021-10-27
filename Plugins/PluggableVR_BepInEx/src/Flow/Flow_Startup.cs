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
	public class Flow_Startup : Flow
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");

			// Unityシーンロードに連動する遷移 
			Global.Transit["ADV"] = () => new Flow_ADV_Prepare();
//			Global.Transit["CharaCustom"] = () => new Flow_CharaCustom_Prepare();
			Global.Transit["Home"] = () => new Flow_Home_Prepare();
			Global.Transit["HScene"] = () => new Flow_HScene_Prepare();
			Global.Transit["Title"] = () => new Flow_Title_Prepare();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
		}

		protected override Flow OnUpdate()
		{
			// メインカメラ生成待ち 
			Global.MainCamera = Camera.main;
			if (Global.MainCamera == null) return null;

			// 操作開始 
			var scale=8.0f;
			var sc = Global.MainCamera;
			var loc = Loc.FromWorldTransform(sc.transform);
			var avatar = new DemoAvatar(loc,scale);
			var player = new DemoPlayer(avatar,scale);

			var mng = VRManager.Instance;
			mng.SetPlayer(player);

			// レイヤ0が表示対象外になるので4に変更 
			var layer = 4;
			avatar.Head.layer = layer;
			avatar.View.transform.Find("Neck").gameObject.layer = layer;
			avatar.View.transform.Find("Shoulder").gameObject.layer = layer;
			avatar.UpFromHead.layer = layer;
			avatar.ForeFromHead.layer = layer;

			// 手の軸表示を消す 
			avatar.LeftHand.SetActive(false);
			avatar.RightHand.SetActive(false);

			return new Flow_Title_Prepare();
		}
	}
}