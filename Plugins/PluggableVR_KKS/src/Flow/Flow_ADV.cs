/*!	@file
	@brief PluggableVR: 手順遷移 会話 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! 手順遷移 会話
	internal class Flow_ADV : Flow_Common
	{
		private string _basescene;
		private Flow _prev;
		private Chaser _chaser;
		private CharaFinder _female;

		internal Flow_ADV(Flow prev, string basescene, int chafidx)
		{
			_basescene = basescene;
			_prev=prev;
			_female = new CharaFinder(false,chafidx);
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// メインカメラ追跡 
			var mng = VRManager.Instance;
			var cam = mng.Camera;
			_chaser = new Chaser(cam.Source.transform);
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
			if (!Global.Scene.GetSceneInfo(_basescene).isLoaded)
			{
				return new Flow_Delay(new Flow_Title());
			}
			// 終了検知 
			if (GameObject.Find("ADVScene")==null) return new Flow_Delay(_prev);

			// カメラ位置変更検知 
			if (_chaser.Update())
			{
				var mng = VRManager.Instance;
				var player = mng.Player;
				player.Reloc(_chaser.Loc);
			}

			// 追加されたキャラに対する処置 
			_female.Find(_femaleFound);

			return null;
		}

		private void _femaleFound(CharaObserver obs)
		{
			Global.Logger.LogDebug("female found: " + obs.Target.name);

			obs.AddPlayerColliders();
		}
	}
}
