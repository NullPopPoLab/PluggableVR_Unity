/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

//! 手順遷移 開始時 
public class Flow_Startup : Flow
{
	protected override Flow OnUpdate()
	{
		// メインカメラ生成待ち 
		var mc = Camera.main;
		if (mc == null) return null;

		// 操作開始 
		var scale = 1.0f;
		var loc = Loc.FromWorldTransform(mc.transform);
		var avatar = new DemoAvatar(loc,scale);
		var player = new DemoPlayer(avatar,scale);

		var mng = VRManager.Instance;
		mng.SetPlayer(player);

		// カメラ設定 
		player.Camera.Reset(mc);
		avatar.SetLayer(0);

		// カメラに連動するコンポーネント移設 
		// 移設先で生成されたコンポーネントが Awake() を呼ばないように一旦Active外しとく 
		player.Camera.Active = false;
		player.Camera.Posess<FlareLayer>();
		player.Camera.Active = true;

		// 遷移終了 
		Terminate();
		return null;
	}
}
