/*!	@file
	@brief PluggableVR: 手順遷移 移動シーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using NullPopPoSpecial;
using PluggableVR;
using UnityEngine;

namespace PluggableVR_KKS
{
	//! 手順遷移 移動シーン 
	internal class Flow_Moving : Flow_Common
	{
		private Flow_Action_Main _main;
		private Camera _minimap;
		private ActionGame.Chara.Player _player;
		private Chaser _chaser;

		public Flow_Moving(Flow_Action_Main main){
			_main = main;
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			_player = GameObject.Find("/ActionScene/Player").GetComponent<ActionGame.Chara.Player>();
			_chaser = new Chaser(_player.transform);

			var mm2 = GameObject.Find("/ActionScene/UI/Minimap/MinimapCamera");
			_minimap = (mm2 == null) ? null : mm2.GetComponent<Camera>();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// 脱出検知 
			if (!Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/Action.unity").isLoaded) return new Flow_Delay(new Flow_Title());
			// 移動終了検知 
			if (!_minimap.enabled)
			{
				// この時点で追加のキャラ探索停止 
				_main.DontFind = true;
				return _main;
			}

			// メインカメラ位置更新 
			var mng = VRManager.Instance;
			mng.Camera.Feedback();

			if(!_main.DontFind){
				// 追加されたキャラに対する処置 
				_main.Male.Find(_maleFound);
				_main.Female.Find(_femaleFound);
			}

			// プレイヤー位置変更検知 
			if (_chaser.Update())
			{
				// 目の位置正しくとれるのか怪しいので頭関節位置から適当にずらしたところで設定 
				var player = mng.Player;
				player.Reloc(Loc.FromWorldTransform(_player.Head) * new Loc(new Vector3(0, 0.1f, 0.1f), Quaternion.identity));
			}

			return null;
		}

		private void _maleFound(CharaObserver obs)
		{
			Global.Logger.LogDebug("male found: " + obs.Target.name);
		}

		private void _femaleFound(CharaObserver obs)
		{
			Global.Logger.LogDebug("female found: " + obs.Target.name);

			// 移動中は無効らしい 
			//obs.AddPlayerColliders();
		}
	}
}