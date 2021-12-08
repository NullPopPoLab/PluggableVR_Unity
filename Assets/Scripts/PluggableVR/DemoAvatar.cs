/*!	@file
	@brief PluggableVR: デモ用VR操作先 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR
{
	//! デモ用VR操作先 
	public class DemoAvatar : VRAvatar
	{
		public static float HeadToEye = 0.1f; //!< 目と頭の距離 
		public static float EyeHeight = 1.5f; //!< 仮想的な目の高さ 
		public static float HeadScale = 0.1f; //!< 頭の大きさ 
		public static float NeckLength = 0.15f; //!< 首の長さ 
		public static float NeckWidth = 0.05f; //!< 首の太さ 
		public static float ShoulderLength = 0.3f; //!< 肩の長さ 
		public static float ShoulderWidth = 0.05f; //!< 肩の太さ 
		public static float AxisLength = 0.4f; //!< 回転軸表示の長さ 
		public static float AxisWidth = 0.01f; //!< 回転軸表示の太さ 
		public static bool UseStandardCollider = false; //!< 標準Collider生成 
		public static bool ShowColliderShape = false; //!< Collider形状表示 

		//! 肩の高さ 
		public static float ShoulderHeight { get { return EyeHeight - NeckLength; } }

		public class AxesView
		{
			public GameObject Node;
			public GameObject X, Y, Z;
		}

		public class HeadView
		{
			public GameObject Node;
			public GameObject View;
			public AxesView Axes;
			public GameObject Head;
			public GameObject Neck;
			public GameObject Shoulder;
			public GameObject Collider;
		}

		public class HandView
		{
			public GameObject Node;
			public AxesView Axes;
			public GameObject Collider;
			public GameObject Raycaster;
		}


		//! 拡大率 
		public float Scale { get; private set; }

		public Transform Origin { get; private set; }
		public Transform Pivot { get; private set; }
		public Transform Eye { get; private set; }
		public HeadView Head { get; private set; }
		public HandView LeftHand { get; private set; }
		public HandView RightHand { get; private set; }

		public DemoAvatar(Loc loc, float scale = 1.0f)
		{
			Scale = scale;
			// 頭 
			var loc_head = loc * new Loc(new Vector3(0, 0, -HeadToEye) * scale, Quaternion.identity);
			// 配置位置 
			var loc_orig = loc_head * new Loc(new Vector3(0, -EyeHeight, 0) * scale, Quaternion.identity);
			loc_orig.Rot = RotUt.ReturnY(loc_orig.Rot);
			Origin = CreateRootObject("VRAvatar", loc_orig).transform;
			GameObject.DontDestroyOnLoad(Origin.gameObject);
			// 回転基準 
			Pivot = CreateChildObject("Pivot", Origin, Loc.Identity, false).transform;
			// 目(=指定位置) 
			Eye = CreateChildObject("Eye", Origin, loc, true).transform;

			// 表示部 
			Head = new HeadView();
			Head.Node = CreateChildObject("Head", Eye, loc_head, true);
			Head.View = CreateChildObject("View", Head.Node.transform, Loc.Identity, false);
			Head.Head = CreateChildPrimitive(PrimitiveType.Sphere, false, "Head", Head.View.transform, Loc.Identity, false);
			Head.Head.transform.localScale = new Vector3(HeadScale, HeadScale, HeadScale) * scale;
			Head.Axes = _addAxes(Head.View.transform);
			_shadowOff(Head.Head.GetComponent<Renderer>());

			Head.Neck = CreateChildPrimitive(PrimitiveType.Cylinder, false, "Neck", Head.View.transform, new Loc(new Vector3(0, -NeckLength * 0.5f, 0) * scale, Quaternion.identity), false);
			Head.Neck.transform.localScale = new Vector3(NeckWidth, NeckLength * 0.5f, NeckWidth) * scale;
			_shadowOff(Head.Neck.GetComponent<Renderer>());

			Head.Shoulder = CreateChildPrimitive(PrimitiveType.Cylinder, false, "Shoulder", Head.View.transform, new Loc(new Vector3(0, -NeckLength, 0) * scale, RotUt.RotZ(Mathf.Deg2Rad * 90)), false);
			Head.Shoulder.transform.localScale = new Vector3(ShoulderWidth, ShoulderLength * 0.5f, ShoulderWidth) * scale;
			_shadowOff(Head.Shoulder.GetComponent<Renderer>());

			Head.Collider = ShowColliderShape ?
				CreateChildPrimitive(PrimitiveType.Capsule, false, "Collider", Head.Node.transform, new Loc(new Vector3(0, 0f, 0) * scale, Quaternion.identity), false):
				CreateChildObject("Collider", Head.Node.transform, new Loc(new Vector3(0, 0, 0) * scale, Quaternion.identity), false);
			Head.Collider.transform.localScale = new Vector3(0.2f, 0.15f, 0.2f);

			LeftHand = new HandView();
			LeftHand.Node = CreateChildObject("LeftHand", Origin, Loc.Identity, false);
			LeftHand.Axes = _addAxes(LeftHand.Node.transform);
			LeftHand.Collider = ShowColliderShape ?
				CreateChildPrimitive(PrimitiveType.Capsule, false, "Collider", LeftHand.Node.transform, new Loc(new Vector3(0.025f, 0, -0.01f) * scale, Quaternion.identity), false):
				CreateChildObject("Collider", LeftHand.Node.transform, new Loc(new Vector3(0.025f, 0, -0.01f) * scale, Quaternion.identity), false);
			LeftHand.Collider.transform.localScale = new Vector3(0.15f, 0.03f, 0.15f);
			LeftHand.Raycaster = CreateChildObject("Raycaster", LeftHand.Node.transform, new Loc(new Vector3(), RotUt.RotY(-Rot2D.Right)), false);

			RightHand = new HandView();
			RightHand.Node = CreateChildObject("RightHand", Origin, Loc.Identity, false);
			RightHand.Axes = _addAxes(RightHand.Node.transform);
			RightHand.Collider = ShowColliderShape ?
				CreateChildPrimitive(PrimitiveType.Capsule, false, "Collider", RightHand.Node.transform, new Loc(new Vector3(-0.025f, 0, -0.01f) * scale, Quaternion.identity), false):
				CreateChildObject("Collider", RightHand.Node.transform, new Loc(new Vector3(-0.025f, 0, -0.01f) * scale, Quaternion.identity), false);
			RightHand.Collider.transform.localScale = new Vector3(0.15f, 0.03f, 0.15f);
			RightHand.Raycaster = CreateChildObject("Raycaster", RightHand.Node.transform, new Loc(new Vector3(), RotUt.RotY(Rot2D.Right)), false);

			if (UseStandardCollider){
				_addCollider(Head.Collider);
				_addCollider(LeftHand.Collider);
				_addCollider(RightHand.Collider);
			}

			Head.Axes.X.SetActive(false);
		}

		private struct _AxisParam
		{
			public string Name;
			public Vector3 Dir;
			public Color Col;
			public Action<AxesView, GameObject> Recept;

			public _AxisParam(string n, Vector3 d, Color c, Action<AxesView, GameObject> r)
			{
				Name = n;
				Dir = d;
				Col = c;
				Recept = r;
			}
		}

		private static _AxisParam[] _axisParam ={
			new _AxisParam("X", new Vector3(AxisLength, 0, 0),new Color(128, 0, 128),(v,o)=>{ v.X=o; }),
			new _AxisParam("Y", new Vector3(0,AxisLength), new Color(0, 255, 0),(v,o)=>{ v.Y=o; }),
			new _AxisParam("Z", new Vector3(0,0,AxisLength), new Color(0, 128, 255),(v,o)=>{ v.Z=o; }),
		};

		private void _shadowOff(Renderer t){
			t.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			t.receiveShadows = false;
		}

		private AxesView _addAxes(Transform parent)
		{
			var t = new AxesView();
			t.Node = CreateChildObject("Axes", parent, Loc.Identity, false);

			var il = _axisParam.Length;
			for (var i = 0; i < il; ++i)
			{
				var prm = _axisParam[i];

				var ax = CreateChildObject(prm.Name, t.Node.transform, Loc.Identity, false);
				var lx = ax.AddComponent<LineRenderer>();
				lx.useWorldSpace = false;
				lx.receiveShadows = false;
				lx.SetPositions(new Vector3[] { new Vector3(0, 0, 0), prm.Dir * Scale });
				lx.material = new Material(Shader.Find("Sprites/Default"));
				lx.startColor = lx.endColor = prm.Col;
				lx.numCapVertices = 5;
				lx.widthMultiplier = AxisWidth;
				_shadowOff(lx);
				prm.Recept(t, ax);
			}
			return t;
		}

		private void _addCollider(GameObject target)
		{
			var c = target.AddComponent<CapsuleCollider>();
			c.radius = 0.5f;
			c.height = 2.0f;
			c.direction = 1;

			var r = target.AddComponent<Rigidbody>();
			r.useGravity = false;
			r.isKinematic = true;
			r.drag = Mathf.Infinity;
			r.angularDrag = Mathf.Infinity;
		}

		protected override void OnChangeLayer(int layer)
		{
			Head.Head.layer = layer;
			Head.Neck.layer = layer;
			Head.Shoulder.layer = layer;
			Head.Axes.Y.layer = layer;
			Head.Axes.Z.layer = layer;
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
			t.LocalLeftHand = inv * Loc.FromWorldTransform(LeftHand.Node.transform);
			t.LocalRightHand = inv * Loc.FromWorldTransform(RightHand.Node.transform);
			return t;
		}

		//! 操作構造反映 
		public void UpdateControl(DemoControl cs)
		{
			cs.Origin.ToWorldTransform(Origin);
			cs.LocalPivot.ToLocalTransform(Pivot);
			cs.LocalEye.ToLocalTransform(Eye);
			cs.LocalLeftHand.ToLocalTransform(LeftHand.Node.transform);
			cs.LocalRightHand.ToLocalTransform(RightHand.Node.transform);
		}
	}
}