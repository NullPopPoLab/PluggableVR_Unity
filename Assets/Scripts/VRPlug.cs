/*!	@file
	@brief PluggableVR: VRプラグ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using PluggableVR;
using NullPopPoSpecial;

#pragma warning disable 0414 // 未使用メンバ警告抑制 

//! VRプラグ 
public class VRPlug : MonoBehaviour
{
	[SerializeField, Tooltip("メインカメラの扱い(初期設定)")] VRCamera.ESourceMode _cameraMode;
	[SerializeField, Tooltip("Camera ModeがdisableのときVRカメラ位置をメインカメラに書き戻す")] bool _cameraFeedback;

#if UNITY_EDITOR
	[SerializeField,Tooltip("Hierarchy状態をファイルに書き出す")] private bool _dumpHierarchy;

	[SerializeField,Space(10)] private Transform _teleportTarget;
	[SerializeField,Tooltip("Teleport Target 位置に移動")] private bool _repos;
	[SerializeField,Tooltip("Teleport Target 向きを合わせる")] private bool _rerot;
	[SerializeField,Tooltip("Teleport Target 位置に移動して向きを合わせる")] private bool _reloc;

	[SerializeField,Space(10)] private Vector3 _eyePos;
	[SerializeField, Range(-1, 1)] private float _eyeRotXx;
	[SerializeField, Range(-1, 1)] private float _eyeRotXy;
	[SerializeField, Range(-1, 1)] private float _eyeRotXz;
	[SerializeField, Range(-1, 1)] private float _eyeRotYx;
	[SerializeField, Range(-1, 1)] private float _eyeRotYy;
	[SerializeField, Range(-1, 1)] private float _eyeRotYz;
	[SerializeField, Range(-1, 1)] private float _eyeRotZx;
	[SerializeField, Range(-1, 1)] private float _eyeRotZy;
	[SerializeField, Range(-1, 1)] private float _eyeRotZz;

	[SerializeField,Space(10)] private Vector3 _leftPos;
	[SerializeField, Range(-1, 1)] private float _leftRotXx;
	[SerializeField, Range(-1, 1)] private float _leftRotXy;
	[SerializeField, Range(-1, 1)] private float _leftRotXz;
	[SerializeField, Range(-1, 1)] private float _leftRotYx;
	[SerializeField, Range(-1, 1)] private float _leftRotYy;
	[SerializeField, Range(-1, 1)] private float _leftRotYz;
	[SerializeField, Range(-1, 1)] private float _leftRotZx;
	[SerializeField, Range(-1, 1)] private float _leftRotZy;
	[SerializeField, Range(-1, 1)] private float _leftRotZz;
	[SerializeField] private bool _leftStickTouch;
	[SerializeField] private bool _leftStickPressed;
	[SerializeField,Range(-1,1)] private float _leftStickTiltingX;
	[SerializeField,Range(-1,1)] private float _leftStickTiltingY;
	[SerializeField] private bool _leftIndexTouch;
	[SerializeField] private bool _leftIndexPressed;
	[SerializeField,Range(0,1)] private float _leftIndexPressing;
	[SerializeField] private bool _leftHandPressed;
	[SerializeField,Range(0,1)] private float _leftHandPressing;
	[SerializeField] private bool _leftButton1Touch;
	[SerializeField] private bool _leftButton1Pressed;
	[SerializeField] private bool _leftButton2Touch;
	[SerializeField] private bool _leftButton2Pressed;

	[SerializeField,Space(10)] private Vector3 _rightPos;
	[SerializeField, Range(-1, 1)] private float _rightRotXx;
	[SerializeField, Range(-1, 1)] private float _rightRotXy;
	[SerializeField, Range(-1, 1)] private float _rightRotXz;
	[SerializeField, Range(-1, 1)] private float _rightRotYx;
	[SerializeField, Range(-1, 1)] private float _rightRotYy;
	[SerializeField, Range(-1, 1)] private float _rightRotYz;
	[SerializeField, Range(-1, 1)] private float _rightRotZx;
	[SerializeField, Range(-1, 1)] private float _rightRotZy;
	[SerializeField, Range(-1, 1)] private float _rightRotZz;
	[SerializeField] private bool _rightStickTouch;
	[SerializeField] private bool _rightStickPressed;
	[SerializeField,Range(-1,1)] private float _rightStickTiltingX;
	[SerializeField,Range(-1,1)] private float _rightStickTiltingY;
	[SerializeField] private bool _rightIndexTouch;
	[SerializeField] private bool _rightIndexPressed;
	[SerializeField,Range(0,1)] private float _rightIndexPressing;
	[SerializeField] private bool _rightHandPressed;
	[SerializeField,Range(0,1)] private float _rightHandPressing;
	[SerializeField] private bool _rightButton1Touch;
	[SerializeField] private bool _rightButton1Pressed;
	[SerializeField] private bool _rightButton2Touch;
	[SerializeField] private bool _rightButton2Pressed;

	[SerializeField,Space(10)] private bool _primaryStickTouch;
	[SerializeField] private bool _primaryStickPressed;
	[SerializeField,Range(-1,1)] private float _primaryStickTiltingX;
	[SerializeField,Range(-1,1)] private float _primaryStickTiltingY;
	[SerializeField] private bool _primaryRest;
	[SerializeField] private bool _primaryIndexTouch;
	[SerializeField] private bool _primaryIndexPressed;
	[SerializeField,Range(0,1)] private float _primaryIndexPressing;
	[SerializeField] private bool _primaryHandPressed;
	[SerializeField,Range(0,1)] private float _primaryHandPressing;

	[SerializeField,Space(10)] private bool _secondaryStickTouch;
	[SerializeField] private bool _secondaryStickPressed;
	[SerializeField,Range(-1,1)] private float _secondaryStickTiltingX;
	[SerializeField,Range(-1,1)] private float _secondaryStickTiltingY;
	[SerializeField] private bool _secondaryRest;
	[SerializeField] private bool _secondaryIndexTouch;
	[SerializeField] private bool _secondaryIndexPressed;
	[SerializeField,Range(0,1)] private float _secondaryIndexPressing;
	[SerializeField] private bool _secondaryHandPressed;
	[SerializeField,Range(0,1)] private float _secondaryHandPressing;
#endif

	private VRManager _vrmng = new VRManager();

	//! 初期設定 
	protected void Awake()
	{
		VRCamera.Revision = VRCamera.ERevision.Legacy;
		VRCamera.SourceMode = _cameraMode;

		_vrmng.Initialize(new Flow_Startup());
		OVRPlugin.SetTrackingOriginType(OVRPlugin.TrackingOrigin.EyeLevel);
	}

	//! 終了 
	protected void OnDestroy()
	{
		_vrmng.Shutdown();
	}

	//! 物理フレーム毎の更新 
	protected void FixedUpdate()
	{
		_vrmng.FixedUpdate();
	}

	//! 描画フレーム毎の更新 
	protected void Update()
	{
#if UNITY_EDITOR
		if(_repos){
			_repos=false;
			_vrmng.Repos(_teleportTarget.position);
		}
		if(_rerot){
			_rerot=false;
			_vrmng.Rerot(_teleportTarget.rotation);
		}
		if(_reloc){
			_reloc=false;
			_vrmng.Reloc(Loc.FromWorldTransform(_teleportTarget));
		}
#endif

		_vrmng.Update();
		if(_cameraFeedback)_vrmng.Camera.Feedback();

#if UNITY_EDITOR
		var inp = VRManager.Input;
		var eye = inp.Head.GetEyeTracking();
		_eyePos = eye.Pos;
		_eyeRotXx = RotUt.Xx(eye.Rot);
		_eyeRotXy = RotUt.Xy(eye.Rot);
		_eyeRotXz = RotUt.Xz(eye.Rot);
		_eyeRotYx = RotUt.Yx(eye.Rot);
		_eyeRotYy = RotUt.Yy(eye.Rot);
		_eyeRotYz = RotUt.Yz(eye.Rot);
		_eyeRotZx = RotUt.Zx(eye.Rot);
		_eyeRotZy = RotUt.Zy(eye.Rot);
		_eyeRotZz = RotUt.Zz(eye.Rot);

		var lh = inp.HandLeft.GetHandTracking();
		_leftPos = lh.Pos;
		_leftRotXx = RotUt.Xx(lh.Rot);
		_leftRotXy = RotUt.Xy(lh.Rot);
		_leftRotXz = RotUt.Xz(lh.Rot);
		_leftRotYx = RotUt.Yx(lh.Rot);
		_leftRotYy = RotUt.Yy(lh.Rot);
		_leftRotYz = RotUt.Yz(lh.Rot);
		_leftRotZx = RotUt.Zx(lh.Rot);
		_leftRotZy = RotUt.Zy(lh.Rot);
		_leftRotZz = RotUt.Zz(lh.Rot);

		var rh = inp.HandRight.GetHandTracking();
		_rightPos = rh.Pos;
		_rightRotXx = RotUt.Xx(rh.Rot);
		_rightRotXy = RotUt.Xy(rh.Rot);
		_rightRotXz = RotUt.Xz(rh.Rot);
		_rightRotYx = RotUt.Yx(rh.Rot);
		_rightRotYy = RotUt.Yy(rh.Rot);
		_rightRotYz = RotUt.Yz(rh.Rot);
		_rightRotZx = RotUt.Zx(rh.Rot);
		_rightRotZy = RotUt.Zy(rh.Rot);
		_rightRotZz = RotUt.Zz(rh.Rot);

		var ls=inp.HandLeft.GetStickTilting();
		_leftStickTiltingX=ls.x;
		_leftStickTiltingY=ls.y;
		_leftStickTouch=inp.HandLeft.IsStickTouching();
		_leftStickPressed=inp.HandLeft.IsStickPressed();
		_leftIndexTouch=inp.HandLeft.IsIndexTouching();
		_leftIndexPressed=inp.HandLeft.IsIndexPressed();
		_leftIndexPressing=inp.HandLeft.GetIndexPressing();
		_leftHandPressed=inp.HandLeft.IsHandPressed();
		_leftHandPressing=inp.HandLeft.GetHandPressing();
		_leftButton1Touch=inp.HandLeft.IsButton1Touching();
		_leftButton1Pressed=inp.HandLeft.IsButton1Pressed();
		_leftButton2Touch=inp.HandLeft.IsButton2Touching();
		_leftButton2Pressed=inp.HandLeft.IsButton2Pressed();

		var rs=inp.HandRight.GetStickTilting();
		_rightStickTiltingX=rs.x;
		_rightStickTiltingY=rs.y;
		_rightStickTouch=inp.HandRight.IsStickTouching();
		_rightStickPressed=inp.HandRight.IsStickPressed();
		_rightIndexTouch=inp.HandRight.IsIndexTouching();
		_rightIndexPressed=inp.HandRight.IsIndexPressed();
		_rightIndexPressing=inp.HandRight.GetIndexPressing();
		_rightHandPressed=inp.HandRight.IsHandPressed();
		_rightHandPressing=inp.HandRight.GetHandPressing();
		_rightButton1Touch=inp.HandRight.IsButton1Touching();
		_rightButton1Pressed=inp.HandRight.IsButton1Pressed();
		_rightButton2Touch=inp.HandRight.IsButton2Touching();
		_rightButton2Pressed=inp.HandRight.IsButton2Pressed();

		var ps=inp.HandPrimary.GetStickTilting();
		_primaryStickTiltingX=ps.x;
		_primaryStickTiltingY=ps.y;
		_primaryStickTouch=inp.HandPrimary.IsStickTouching();
		_primaryStickPressed=inp.HandPrimary.IsStickPressed();
		_primaryIndexTouch=inp.HandPrimary.IsIndexTouching();
		_primaryIndexPressed=inp.HandPrimary.IsIndexPressed();
		_primaryIndexPressing=inp.HandPrimary.GetIndexPressing();
		_primaryHandPressed=inp.HandPrimary.IsHandPressed();
		_primaryHandPressing=inp.HandPrimary.GetHandPressing();

		var ss=inp.HandSecondary.GetStickTilting();
		_secondaryStickTiltingX=ss.x;
		_secondaryStickTiltingY=ss.y;
		_secondaryStickTouch=inp.HandSecondary.IsStickTouching();
		_secondaryStickPressed=inp.HandSecondary.IsStickPressed();
		_secondaryIndexTouch=inp.HandSecondary.IsIndexTouching();
		_secondaryIndexPressed=inp.HandSecondary.IsIndexPressed();
		_secondaryIndexPressing=inp.HandSecondary.GetIndexPressing();
		_secondaryHandPressed=inp.HandSecondary.IsHandPressed();
		_secondaryHandPressing=inp.HandSecondary.GetHandPressing();

		// Hierarchy書き出し 
		if (_dumpHierarchy)
		{
			_dumpHierarchy = false;
			HierarchyDumper.Dumper.Dump2File("Hierarchy");
		}
#endif
	}

	//! アニメーション処理後の更新 
	protected void LateUpdate()
	{
		_vrmng.LateUpdate();
	}
}
