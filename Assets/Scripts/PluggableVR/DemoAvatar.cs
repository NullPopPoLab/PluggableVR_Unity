/*!	@file
	@brief PluggableVR: デモ用VR操作先 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR
{
	//! デモ用VR操作先 
	internal class DemoAvatar : VRAvatar
	{
		public const float HeadToEye = 0.1f; //!< 目と頭の距離 
		public const float EyeHeight = 1.5f; //!< 仮想的な目の高さ 
		public const float HeadScale = 0.1f; //!< 頭の大きさ 
		public const float NeckLength = 0.15f; //!< 首の長さ 
		public const float NeckWidth = 0.05f; //!< 首の太さ 
		public const float ShoulderLength = 0.3f; //!< 肩の長さ 
		public const float ShoulderWidth = 0.05f; //!< 肩の太さ 
		public const float AxisLength = 0.4f; //!< 回転軸表示の長さ 
		public const float AxisWidth = 0.01f; //!< 回転軸表示の太さ 

		//! 肩の高さ 
		public static float ShoulderHeight { get { return EyeHeight - NeckLength; } }

		public Transform Origin { get; private set; }
		public Transform Pivot { get; private set; }
		public Transform Eye { get; private set; }
		public GameObject View { get; private set; }
		public GameObject Head { get; private set; }
		public GameObject LeftHand { get; private set; }
		public GameObject RightHand { get; private set; }
		public GameObject RightFromHead { get; private set; }
		public GameObject UpFromHead { get; private set; }
		public GameObject ForeFromHead { get; private set; }

		public DemoAvatar(Loc loc)
		{
			// 頭 
			var loc_head = loc * new Loc(new Vector3(0, 0, -HeadToEye), Quaternion.identity);
			// 配置位置 
			var loc_orig = loc_head * new Loc(new Vector3(0, -EyeHeight, 0), Quaternion.identity);
			loc_orig.Rot = RotUt.ReturnY(loc_orig.Rot);
			Origin = CreateRootObject("VRAvatar", loc_orig).transform;
			GameObject.DontDestroyOnLoad(Origin.gameObject);
			// 回転基準 
			Pivot = CreateChildObject("Pivot", Origin, Loc.Identity, false).transform;
			// 目(=指定位置) 
			Eye = CreateChildObject("Eye", Origin, loc, true).transform;

			// 表示部 
			View = CreateChildObject("View", Eye, loc_head, true);
			Head = CreateChildPrimitive(PrimitiveType.Sphere, false, "Head", View.transform, Loc.Identity, false);
			Head.transform.localScale = new Vector3(HeadScale, HeadScale, HeadScale);
			_addAxes(Head.transform);

			var neck = CreateChildPrimitive(PrimitiveType.Cylinder, false, "Neck", View.transform, new Loc(new Vector3(0, -NeckLength * 0.5f, 0), Quaternion.identity), false);
			neck.transform.localScale = new Vector3(NeckWidth, NeckLength * 0.5f, NeckWidth);

			var shoulder = CreateChildPrimitive(PrimitiveType.Cylinder, false, "Shoulder", View.transform, new Loc(new Vector3(0, -NeckLength, 0), RotUt.RotZ(Mathf.Deg2Rad * 90)), false);
			shoulder.transform.localScale = new Vector3(ShoulderWidth, ShoulderLength * 0.5f, ShoulderWidth);

			LeftHand = CreateChildObject("LeftHand", Origin, Loc.Identity, false);
			_addAxes(LeftHand.transform);

			RightHand = CreateChildObject("RightHand", Origin, Loc.Identity, false);
			_addAxes(RightHand.transform);

			RightFromHead = Head.transform.Find("AxisX").gameObject;
			UpFromHead = Head.transform.Find("AxisY").gameObject;
			ForeFromHead = Head.transform.Find("AxisZ").gameObject;

			RightFromHead.SetActive(false);
		}

		private struct _AxisParam
		{
			public string Name;
			public Vector3 Dir;
			public Color Col;

			public _AxisParam(string n, Vector3 d, Color c)
			{
				Name = n;
				Dir = d;
				Col = c;
			}
		}

		private static _AxisParam[] _axisParam ={
			new _AxisParam("AxisX", new Vector3(AxisLength, 0, 0),new Color(128, 0, 128)),
			new _AxisParam("AxisY", new Vector3(0,AxisLength), new Color(0, 255, 0)),
			new _AxisParam("AxisZ", new Vector3(0,0,AxisLength), new Color(0, 128, 255)),
		};

		private void _addAxes(Transform parent)
		{
			var il = _axisParam.Length;
			for (var i = 0; i < il; ++i)
			{
				var prm = _axisParam[i];

				var ax = CreateChildObject(prm.Name, parent, Loc.Identity, false);
				var lx = ax.AddComponent<LineRenderer>();
				lx.useWorldSpace = false;
				lx.receiveShadows = false;
				lx.SetPositions(new Vector3[] { new Vector3(0, 0, 0), prm.Dir });
				lx.material = new Material(Shader.Find("Sprites/Default"));
				lx.startColor = lx.endColor = prm.Col;
				lx.numCapVertices = 5;
				lx.widthMultiplier = AxisWidth;
			}
		}

		//! 操作構造生成 
		/*!	@note 位置参照の正確性および書き込み手順の正当性を確保するため、
				直接transformへのアクセスをさせるべきではない。
		*/
		public DemoControl CreateControl()
		{
			var t = new DemoControl();
			t.Origin = Loc.FromWorldTransform(Origin);
			var inv = t.Origin.Inversed;
			t.LocalPivot = inv * Loc.FromWorldTransform(Pivot);
			t.LocalEye = inv * Loc.FromWorldTransform(Eye);
			t.LocalLeftHand = inv * Loc.FromWorldTransform(LeftHand.transform);
			t.LocalRightHand = inv * Loc.FromWorldTransform(RightHand.transform);
			return t;
		}

		//! 操作構造反映 
		public void UpdateControl(DemoControl cs)
		{
			cs.Origin.ToWorldTransform(Origin);
			cs.LocalPivot.ToLocalTransform(Pivot);
			cs.LocalEye.ToLocalTransform(Eye);
			cs.LocalLeftHand.ToLocalTransform(LeftHand.transform);
			cs.LocalRightHand.ToLocalTransform(RightHand.transform);
		}
	}
}