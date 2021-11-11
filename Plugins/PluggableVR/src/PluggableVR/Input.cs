﻿/*!	@file
	@brief PluggableVR: 入力機能基底 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR
{
	//! 頭の入力状態 
	public class InputHead
	{
		//! 目位置 
		/*!	@note +x=右 +y=上 +z=前
		*/
		public virtual Loc GetEyeTracking() { return Loc.Identity; }
	}

	//! 手の入力状態(左右可換) 
	public class InputHandSwitchable
	{
		//! スティック載せ状態 
		public virtual bool IsStickTouching() { return false; }
		//! スティック押し込み状態 
		public virtual bool IsStickPressed() { return false; }

		//! スティック倒し状態 
		/*!	@note +x=右 +y=前
		*/
		public virtual Vector2 GetStickTilting() { return new Vector2(); }

		//! 掌トリガ押し状態 
		public virtual float GetHandPressing() { return 0.0f; }
		//! 掌トリガ押し込み状態 
		public virtual bool IsHandPressed() { return false; }
		//! 指トリガ載せ状態 
		public virtual bool IsIndexTouching() { return false; }
		//! 指トリガ押し状態 
		public virtual float GetIndexPressing() { return 0.0f; }
		//! 指トリガ押し込み状態 
		public virtual bool IsIndexPressed() { return false; }
	}

	//! 手の入力状態(左右固定) 
	public class InputHandFixed : InputHandSwitchable
	{
		//! 手位置 
		/*!	@note 左手は -x=先 +y=甲 +z=親指 @n
				右手は +x=先 +y=甲 +z=親指 @n
		*/
		public virtual Loc GetHandTracking() { return Loc.Identity; }

		//! ボタン1載せ状態 
		public virtual bool IsButton1Touching() { return false; }
		//! ボタン1押し込み状態 
		public virtual bool IsButton1Pressed() { return false; }
		//! ボタン2載せ状態 
		public virtual bool IsButton2Touching() { return false; }
		//! ボタン2押し込み状態 
		public virtual bool IsButton2Pressed() { return false; }
	}

	//! 入力機能基底 
	public class Input
	{
		public InputHead Head; //!< 頭 
		public InputHandFixed HandLeft; //!< 左コントローラ 
		public InputHandFixed HandRight; //!< 右コントローラ 
		public InputHandSwitchable HandPrimary; //!< 主コントローラ(利き手と逆のコントローラ) 
		public InputHandSwitchable HandSecondary; //!< 副コントローラ(利き手のコントローラ) 

		//! コンストラクタで初期化動作書くの無駄なので使うべきでない宣言 
		protected Input() { }

		//! 代わりにこっちで 
		public static Input Setup()
		{
			var t = new Input();
			t.Head = new InputHead();
			t.HandPrimary = t.HandLeft = new InputHandFixed();
			t.HandSecondary = t.HandRight = new InputHandFixed();
			return t;
		}

		//! トラッキングのリセット 
		public virtual void Reset(){}

		//! 物理フレーム毎の更新 
		public virtual void FixedUpdate(){}
		//! 描画フレーム毎の更新 
		public virtual void Update(){}
		//! アニメーション処理後の更新 
		public virtual void LateUpdate(){}
	}
}