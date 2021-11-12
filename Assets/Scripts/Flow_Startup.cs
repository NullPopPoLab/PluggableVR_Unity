/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

//! 手順遷移 開始時 
public class Flow_Startup : FlowBase
{
	protected override void OnStart()
	{
		base.OnStart();

		// 初期設定 
		DemoAvatar.UseStandardCollider = true;
		DemoAvatar.ShowColliderShape = true;

		var scale = 1.0f;
		var avatar = new DemoAvatar(Loc.Identity, scale);
		var player = new DemoPlayer(avatar, scale);
		VRManager.Instance.SetPlayer(player);

		// 既存のAudioListener封印 
		GameObject.Find("/Main Camera").GetComponent<AudioListener>().enabled = false;
	}

	protected override FlowBase OnUpdate()
	{
		// メインカメラ認識待ち 
		var mc = Camera.main;
		if (mc == null) return null;

		// プレイヤーをカメラ位置に移す 
		var mng = VRManager.Instance;
		var player = mng.Player;
		player.SetCamera(mc);

		// カメラに連動するコンポーネント移設 
		// 移設先で生成されたコンポーネントが Awake() を呼ばないように一旦Active外しとく 
		player.Camera.Active = false;
		player.Camera.Possess<FlareLayer>();
		player.Camera.Possess<Light>();
		player.Camera.Active = true;

		// アバター表示Layerをカメラの表示対象内で選択 
		mng.Avatar.SetLayer(0);

		// 遷移終了 
		Terminate();
		return null;
	}
}
