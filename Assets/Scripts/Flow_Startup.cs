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
		var loc = Loc.FromWorldTransform(mc.transform);
		var avatar = new DemoAvatar(loc);
		var player = new DemoPlayer(avatar);

		var mng = VRManager.Instance;
		mng.SetPlayer(player);

		// 本来のメインカメラは無効化 
		var cam = mc.GetComponent<Camera>();
		if (cam != null) cam.enabled = false;
		var lsn = mc.GetComponent<AudioListener>();
		if (lsn != null) lsn.enabled = false;

		// 遷移終了 
		Terminate();
		return null;
	}
}
