/*!	@file
	@brief PluggableVR: Oculus用コントローラ入力 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR
*/
using UnityEngine;
using NullPopPoSpecial;

#if UNITY_2017_2_OR_NEWER
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;
using Settings = UnityEngine.XR.XRSettings;
#else
using InputTracking = UnityEngine.VR.InputTracking;
using Node = UnityEngine.VR.VRNode;
using Settings = UnityEngine.VR.VRSettings;
#endif

namespace PluggableVR.Oculus
{

	//! 頭の入力状態 
	public class InputHead : PluggableVR.InputHead
	{
		//! 目位置 
		public override Loc GetEyeTracking()
		{
			var t = new Loc();
			t.Pos = InputTracking.GetLocalPosition(Node.CenterEye);
			t.Rot = InputTracking.GetLocalRotation(Node.CenterEye);
			return t;
		}
	}

	//! 手の入力状態(左コントローラ) 
	public class InputHandLeft : PluggableVR.InputHandFixed
	{
		//! 手位置 
		public override Loc GetHandTracking()
		{
			var t = new Loc();
			t.Pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
			t.Rot = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
			t.Rot *= RotUt.RotY(+90 * Mathf.Deg2Rad);
			t.Rot *= RotUt.RotX(-90 * Mathf.Deg2Rad);
			return t;
		}

		//! スティック載せ状態 
		public override bool IsStickTouching()
		{
			return OVRInput.Get(OVRInput.RawTouch.LThumbstick);
		}

		//! スティック押し込み状態 
		public override bool IsStickPressed()
		{
			return OVRInput.Get(OVRInput.RawButton.LThumbstick);
		}

		//! スティック倒し状態 
		public override Vector2 GetStickTilting()
		{
			return OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
		}

		//! 掌トリガ押し状態 
		public override float GetHandPressing() {
			return OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);
		}
		//! 掌トリガ押し込み状態 
		public override bool IsHandPressed()
		{
			return OVRInput.Get(OVRInput.RawButton.LHandTrigger);
		}
		//! 指トリガ押し状態 
		public override float GetIndexPressing()
		{
			return OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
		}
		//! 指トリガ載せ状態 
		public override bool IsIndexTouching()
		{
			return OVRInput.Get(OVRInput.RawTouch.LIndexTrigger);
		}
		//! 指トリガ押し込み状態 
		public override bool IsIndexPressed()
		{
			return OVRInput.Get(OVRInput.RawButton.LIndexTrigger);
		}

		//! ボタン1載せ状態 
		public override bool IsButton1Touching()
		{
			return OVRInput.Get(OVRInput.RawTouch.X);
		}
		//! ボタン1押し込み状態 
		public override bool IsButton1Pressed() {
			return OVRInput.Get(OVRInput.RawButton.X);
		}
		//! ボタン2載せ状態 
		public override bool IsButton2Touching()
		{
			return OVRInput.Get(OVRInput.RawTouch.Y);
		}
		//! ボタン2押し込み状態 
		public override bool IsButton2Pressed() {
			return OVRInput.Get(OVRInput.RawButton.Y);
		}
	}

	//! 手の入力状態(右コントローラ) 
	public class InputHandRight : PluggableVR.InputHandFixed
	{
		//! 手位置 
		public override Loc GetHandTracking()
		{
			var t = new Loc();
			t.Pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
			t.Rot = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
			t.Rot *= RotUt.RotY(-90 * Mathf.Deg2Rad);
			t.Rot *= RotUt.RotX(-90 * Mathf.Deg2Rad);
			return t;
		}

		//! スティック載せ状態 
		public override bool IsStickTouching()
		{
			return OVRInput.Get(OVRInput.RawTouch.RThumbstick);
		}

		//! スティック押し込み状態 
		public override bool IsStickPressed()
		{
			return OVRInput.Get(OVRInput.RawButton.RThumbstick);
		}

		//! スティック倒し状態 
		public override Vector2 GetStickTilting()
		{
			return OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
		}

		//! 掌トリガ押し状態 
		public override float GetHandPressing()
		{
			return OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);
		}
		//! 掌トリガ押し込み状態 
		public override bool IsHandPressed()
		{
			return OVRInput.Get(OVRInput.RawButton.RHandTrigger);
		}
		//! 指トリガ載せ状態 
		public override bool IsIndexTouching()
		{
			return OVRInput.Get(OVRInput.RawTouch.RIndexTrigger);
		}
		//! 指トリガ押し状態 
		public override float GetIndexPressing()
		{
			return OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
		}
		//! 指トリガ押し込み状態 
		public override bool IsIndexPressed()
		{
			return OVRInput.Get(OVRInput.RawButton.RIndexTrigger);
		}

		//! ボタン1載せ状態 
		public override bool IsButton1Touching()
		{
			return OVRInput.Get(OVRInput.RawTouch.A);
		}
		//! ボタン1押し込み状態 
		public override bool IsButton1Pressed()
		{
			return OVRInput.Get(OVRInput.RawButton.A);
		}
		//! ボタン2載せ状態 
		public override bool IsButton2Touching()
		{
			return OVRInput.Get(OVRInput.RawTouch.B);
		}
		//! ボタン2押し込み状態 
		public override bool IsButton2Pressed()
		{
			return OVRInput.Get(OVRInput.RawButton.B);
		}
	}

	//! 手の入力状態(主コントローラ) 
	public class InputHandPrimary : PluggableVR.InputHandSwitchable
	{
		//! スティック指載せ状態 
		public override bool IsStickTouching()
		{
			return OVRInput.Get(OVRInput.Touch.PrimaryThumbstick);
		}

		//! スティック押し込み状態 
		public override bool IsStickPressed()
		{
			return OVRInput.Get(OVRInput.Button.PrimaryThumbstick);
		}

		//! スティック倒し状態 
		public override Vector2 GetStickTilting()
		{
			return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
		}

		//! 掌トリガ押し状態 
		public override float GetHandPressing()
		{
			return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
		}
		//! 掌トリガ押し込み状態 
		public override bool IsHandPressed()
		{
			return OVRInput.Get(OVRInput.Button.PrimaryHandTrigger);
		}
		//! 指トリガ載せ状態 
		public override bool IsIndexTouching()
		{
			return OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger);
		}
		//! 指トリガ押し状態 
		public override float GetIndexPressing()
		{
			return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
		}
		//! 指トリガ押し込み状態 
		public override bool IsIndexPressed()
		{
			return OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
		}
	}

	//! 手の入力状態(副コントローラ) 
	public class InputHandSecondary : PluggableVR.InputHandSwitchable
	{
		//! スティック指載せ状態 
		public override bool IsStickTouching()
		{
			return OVRInput.Get(OVRInput.Touch.SecondaryThumbstick);
		}

		//! スティック押し込み状態 
		public override bool IsStickPressed()
		{
			return OVRInput.Get(OVRInput.Button.SecondaryThumbstick);
		}

		//! スティック倒し状態 
		public override Vector2 GetStickTilting()
		{
			return OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
		}

		//! 掌トリガ押し状態 
		public override float GetHandPressing()
		{
			return OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
		}
		//! 掌トリガ押し込み状態 
		public override bool IsHandPressed() {
			return OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);
		}
		//! 指トリガ載せ状態 
		public override bool IsIndexTouching() {
			return OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger);
		}
		//! 指トリガ押し状態 
		public override float GetIndexPressing()
		{
			return OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
		}
		//! 指トリガ押し込み状態 
		public override bool IsIndexPressed() {
			return OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
		}
	}

	//! GUI入力 
	public class InputGUI : PluggableVR.InputGUI
	{
		private OVRGazePointer _ogp;

		public new VREventSystem ES{ 
			get{ return base.ES as VREventSystem; }
			protected set { base.ES = value; }
		}

		public new static InputGUI Setup(){
			var t=new InputGUI();
			t.ES=new VREventSystem();
			return t;
		}

		protected override bool OnSetCursor(VRCursor src)
		{
			if (!base.OnSetCursor(src)) return false;

			var gobj=src.Ctrl.gameObject;
			gobj.SetActive(false);
			_ogp = gobj.GetComponent<OVRGazePointer>();
			if (_ogp == null) _ogp = gobj.AddComponent<OVRGazePointer>();
			ES.Cursor = src;
			gobj.SetActive(true);

			return true;
		}

		protected override bool OnSetPointer(Transform src) {

			if (!base.OnSetPointer(src)) return false;

//			Cursor.View.Pointer = src;
			ES.Pointer = src;
			if (_ogp != null) _ogp.rayTransform = src;

			return true; 
		}

		protected override PluggableVR.VRCanvas OnCreateCanvas(PluggableVR.VRCanvas.Placing place) { return VRCanvas.Create(place); }

		protected override void OnUpdate() {
			base.OnUpdate();
		}
	}

	//! 入力機能基底 
	public class Input : PluggableVR.Input
	{
		public new static Input Setup()
		{
			var t = new Input();
			t.Head = new InputHead();
			t.HandLeft = new InputHandLeft();
			t.HandRight = new InputHandRight();
			t.HandPrimary = new InputHandPrimary();
			t.HandSecondary = new InputHandSecondary();
			t.GUI=InputGUI.Setup();
			return t;
		}

		//! トラッキングのリセット 
		public override void Reset()
		{
			InputTracking.Recenter();
		}

		//! 物理フレーム毎の更新 
		public override void FixedUpdate()
		{
			OVRInput.FixedUpdate();
		}
		//! 描画フレーム毎の更新 
		public override void Update()
		{
			OVRInput.Update();
			base.Update();
		}
		//! アニメーション処理後の更新 
		public override void LateUpdate()
		{
			OVRHaptics.Process();
		}
	}
}
