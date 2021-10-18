/*!	@file
	@brief PluggableVR: VR操作元 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

//! VR操作元 
public class VRPlayer : MonoBehaviour
{
	[SerializeField, Tooltip("VRカメラ")]
	public Transform Camera;
	[SerializeField, Tooltip("操作対象")]
	public VRAvatar Target;

	//! 入力機能 
	private PluggableVR.Input _input;
	//! 操作対象 
	private PluggableVR.AvatarControl _target;
	//! 操作対象の基準位置からリグ位置までのオフセット 
	private PluggableVR.Loc _offset;
	//! スティック操作中 
	private bool _sticking = false;
	//! 縦移動中 
	private bool _elevating = false;
	//! スティック押下変化捕捉 
	private PluggableVR.RelativeBool _push_pstk = new PluggableVR.RelativeBool();

	// 軸表示 
	private GameObject _head_x;
	private GameObject _head_y;
	private GameObject _head_z;

	//! 通常の初期化タイミングは Awake() だよ Start() じゃないよ 
	void Awake()
	{
		// メインカメラを乗っ取り、CameraとAudioListenerを無効化 
		var cam = UnityEngine.Camera.main;
		cam.GetComponent<Camera>().enabled = false;
		cam.GetComponent<AudioListener>().enabled = false;
		// VR用のカメラを有効化 
		Camera.GetComponent<Camera>().enabled = true;
		Camera.GetComponent<AudioListener>().enabled = true;

		// VR入力機能開始 
		_input = PluggableVR.Oculus.Input.Setup();
		_target = Target.CreateControl();
		ResetRig();

		// 初期状態では頭を非表示とする 
		// 俯瞰操作の間だけ表示 
		Target.Head.gameObject.SetActive(false);

		// xは常に非表示 
		// y,zは _elevating で切り替える 
		_head_x = Target.Head.Find("Axis/X").gameObject;
		_head_y = Target.Head.Find("Axis/Y").gameObject;
		_head_z = Target.Head.Find("Axis/Z").gameObject;
		_head_x.SetActive(false);
	}

	private void _showElevating(){

		_head_y.SetActive(_elevating);
		_head_z.SetActive(!_elevating);
	}

	// 描画フレーム毎 physics,inputより後の動作 
	void Update()
	{
		// スティックの指載せで操作の主観/俯瞰を切り替える 
		var stk1 = _input.HandPrimary.IsStickTouching();
		var stk2 = _input.HandSecondary.IsStickTouching();
		if (!_sticking)
		{
			if (stk1 || stk2)
			{
				_sticking = true;
				Target.Head.gameObject.SetActive(true);
				_showElevating();
			}
		}
		else
		{
			if (!(stk1 || stk2))
			{
				_sticking = false;
				ResetRig();
				Target.Head.gameObject.SetActive(false);
			}
		}

		// 上下/前後 移動切り替え 
		_push_pstk.Update(_input.HandPrimary.IsStickPushing());
		if (_sticking)
		{
			if (_push_pstk.Delta > 0)
			{
				_elevating = !_elevating;
				_showElevating();
			}
		}
		else _elevating = false;

		// スティック回転 
		if (stk2)
		{
			var tilt = _input.HandSecondary.GetStickTilting();
			_target.Origin.Rot *= PluggableVR.RotUt.RotY(90.0f * Mathf.Deg2Rad * tilt.x * Time.deltaTime);
		}

		// スティック移動 
		if (stk1)
		{
			var tilt = _input.HandPrimary.GetStickTilting();
			if (_elevating)
			{
				// xy平面上のz軸2D回転 
				var dir = PluggableVR.RotUt.PlaneXY(Camera.transform.rotation);
				// スティック方向を回転 ついでに移動スピードも掛ける 
				var mzx = dir * new Vector2(tilt.x, tilt.y) * Time.deltaTime;
				_target.Origin.Pos += new Vector3(mzx.x, mzx.y, 0);
			}
			else
			{
				// zx平面上のy軸2D回転 
				var dir = PluggableVR.RotUt.PlaneZX(Camera.transform.rotation);
				// スティック方向を回転 ついでに移動スピードも掛ける 
				var mzx = dir * new Vector2(tilt.y, tilt.x) * Time.deltaTime;
				_target.Origin.Pos += new Vector3(mzx.y, 0, mzx.x);
			}
		}

		// アバター位置反映 
		var ofs = _target.Origin * _offset;
		_target.WorldEye = ofs * _input.Head.GetEyeTracking();
		_target.WorldLeftHand = ofs * _input.HandLeft.GetHandTracking();
		_target.WorldRightHand = ofs * _input.HandRight.GetHandTracking();
		Target.UpdateControl(_target);
	}

	//! VRカメラを所定の位置に戻す 
	public void ResetRig()
	{
		// カメラ位置 
		var ce = PluggableVR.Loc.FromWorldTransform(Camera);
		// トラッキング位置 
		var re = _input.Head.GetEyeTracking();
		// 操作対象の目位置 
		var ve = _target.WorldEye;

		// 回転Y軸を真上に戻した状態で判定 
		ce.Rot = PluggableVR.RotUt.ReturnY(ce.Rot);
		re.Rot = PluggableVR.RotUt.ReturnY(re.Rot);
		ve.Rot = PluggableVR.RotUt.ReturnY(ve.Rot);

		// リグを操作対象の目位置に合わせる 
		ve.ToWorldTransform(transform);
		// カメラリセット 
		_input.Reset();

		// 入力オフセット 
		_offset = ve / _target.Origin;
	}
}
