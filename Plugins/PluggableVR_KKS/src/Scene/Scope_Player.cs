/*!	@file
	@brief PluggableVR: Player スコープ付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! Player スコープ付随動作 
	internal class Scope_Player : ComponentScope<ActionGame.Chara.Player>
	{
		public bool Interrupt;

		private bool _movable;
		public bool IsMovable
		{
			get { return _movable; }
			private set
			{
				if (_movable == value) return;
				_movable = value;

				if (value)
				{
					Global.Logger.LogDebug(ToString() + " bgn");

					var mng = VRManager.Instance;
					mng.Camera.BeActive();
				}
				else
				{
					Global.Logger.LogDebug(ToString() + " end");
				}
			}
		}

		private Chaser _chaser;

		protected override void OnStart()
		{
			base.OnStart();

			_chaser = new Chaser(Transform);
		}

		protected override void OnTerminate()
		{
			base.OnTerminate();

			IsMovable = false;
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			IsMovable = !Interrupt;
			if (!IsMovable) return;

			// プレイヤー位置変更検知 
			if (_chaser.Update())
			{
				// 目の位置正しくとれるのか怪しいので頭関節位置から適当にずらしたところで設定 
				var mng = VRManager.Instance;
				var player = mng.Player;
				player.Reloc(Loc.FromWorldTransform(Target.Head) * new Loc(new Vector3(0, 0.1f, 0.1f), Quaternion.identity));
			}
		}
	}
}
