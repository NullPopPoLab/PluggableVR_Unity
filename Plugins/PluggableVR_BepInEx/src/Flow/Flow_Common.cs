/*!	@file
	@brief PluggableVR: 手順遷移 シーン切り替え共通部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 共通機能
	internal class Flow_Common : Flow
	{
		internal ChangingCensor<string> LastLoadedScene;
		internal CameraControl Camera;

		protected override void OnStart()
		{
			base.OnStart();

			LastLoadedScene.Reset(Global.LastLoadedScene);
			Camera = new CameraControl();
		}

		//! カメラパラメータ更新 
		internal void UpdateCameraParam(int layer, Camera sc=null)
		{
			// メインカメラ位置に移動 
			if (sc == null) sc = Camera.Camera;
			if (sc == null) return;
			VRManager.Instance.Reloc(Loc.FromWorldTransform(sc.transform));

			// メインカメラのパラメータを反映 
			var dc = (VRManager.Instance.Player as DemoPlayer).Camera.GetComponent<Camera>();
			dc.clearFlags = sc.clearFlags;
			dc.cullingMask = sc.cullingMask;
			dc.farClipPlane = sc.farClipPlane;
			dc.nearClipPlane = 0.1f;
			// 変更不可らしい 
			//			dc.fieldOfView = sc.fieldOfView;

			// 本来のメインカメラは無効化…するとハマるのでculingMaskを外してみる 
			sc.cullingMask = 0;
			sc.clearFlags = CameraClearFlags.Nothing;
			//			sc.enabled = false;
			//			var lsn = sc.GetComponent<AudioListener>();
			//			if (lsn != null) lsn.enabled = false;

			// avatar表示レイヤ設定 
			var avatar = VRManager.Instance.Avatar as DemoAvatar;
			avatar.Head.layer = layer;
			avatar.View.transform.Find("Neck").gameObject.layer = layer;
			avatar.View.transform.Find("Shoulder").gameObject.layer = layer;
			avatar.UpFromHead.layer = layer;
			avatar.ForeFromHead.layer = layer;
		}

		//! カメラのコンポーネント移設 
		internal void Possess<T>(Camera sc=null) where T:Behaviour
		{
			if (sc == null) sc = Camera.Camera;
			if (sc == null) return;
			var dc = (VRManager.Instance.Player as DemoPlayer).Camera;
			ComponentUt.Possess(sc.gameObject.GetComponent<T>(), dc.gameObject);
		}

		//! カメラのコンポーネント除去 
		internal void Remove<T>() where T : Component
		{
			var dc = (VRManager.Instance.Player as DemoPlayer).Camera;
			GameObject.Destroy(dc.gameObject.GetComponent<T>());
		}

		//! 元カメラのコンポーネント無効化 
		internal void Suppress<T>(Camera sc = null) where T : Behaviour
		{
			if (sc == null) sc = Camera.Camera;
			if (sc == null) return;
			sc.gameObject.GetComponent<T>().enabled = false;
		}

		//! 通常のシーン切り替え遷移 
		internal Flow StepScene(){

			var scn = Global.LastLoadedScene;
			if (!LastLoadedScene.Update(scn)) return null;

			if (!Global.Transit.ContainsKey(scn)) return null;
			return Global.Transit[scn]();
		}
	}
}