/*!	@file
	@brief PluggableVR: Oculus用コントローラ入力 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR
*/
using UnityEngine;

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

		//! 指休め状態 
		public override bool IsResting()
		{
			return OVRInput.Get(OVRInput.RawTouch.LThumbRest);
		}

		//! スティック指載せ状態 
		public override bool IsStickTouching()
		{
			return OVRInput.Get(OVRInput.RawTouch.LThumbstick);
		}

		//! スティック押し状態 
		public override bool IsStickPushing()
		{
			return OVRInput.Get(OVRInput.RawButton.LThumbstick);
		}

		//! スティック倒し状態 
		public override Vector2 GetStickTilting()
		{
			return OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
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

		//! 指休め状態 
		public override bool IsResting()
		{
			return OVRInput.Get(OVRInput.RawTouch.RThumbRest);
		}

		//! スティック指載せ状態 
		public override bool IsStickTouching()
		{
			return OVRInput.Get(OVRInput.RawTouch.RThumbstick);
		}

		//! スティック押し状態 
		public override bool IsStickPushing()
		{
			return OVRInput.Get(OVRInput.RawButton.LThumbstick);
		}

		//! スティック倒し状態 
		public override Vector2 GetStickTilting()
		{
			return OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
		}
	}

	//! 手の入力状態(主コントローラ) 
	public class InputHandPrimary : PluggableVR.InputHandSwitchable
	{
		//! 指休め状態 
		public override bool IsResting()
		{
			return OVRInput.Get(OVRInput.Touch.PrimaryThumbRest);
		}

		//! スティック指載せ状態 
		public override bool IsStickTouching()
		{
			return OVRInput.Get(OVRInput.Touch.PrimaryThumbstick);
		}

		//! スティック押し状態 
		public override bool IsStickPushing()
		{
			return OVRInput.Get(OVRInput.Button.PrimaryThumbstick);
		}

		//! スティック倒し状態 
		public override Vector2 GetStickTilting()
		{
			return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
		}
	}

	//! 手の入力状態(副コントローラ) 
	public class InputHandSecondary : PluggableVR.InputHandSwitchable
	{
		//! 指休め状態 
		public override bool IsResting()
		{
			return OVRInput.Get(OVRInput.Touch.SecondaryThumbRest);
		}

		//! スティック指載せ状態 
		public override bool IsStickTouching()
		{
			return OVRInput.Get(OVRInput.Touch.SecondaryThumbstick);
		}

		//! スティック押し状態 
		public override bool IsStickPushing()
		{
			return OVRInput.Get(OVRInput.Button.SecondaryThumbstick);
		}

		//! スティック倒し状態 
		public override Vector2 GetStickTilting()
		{
			return OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
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
		}
		//! アニメーション処理後の更新 
		public override void LateUpdate()
		{
			OVRHaptics.Process();
		}
	}
}
