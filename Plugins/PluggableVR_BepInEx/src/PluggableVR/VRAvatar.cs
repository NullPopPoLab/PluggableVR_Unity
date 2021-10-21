/*!	@file
	@brief PluggableVR: VR操作先 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PluggableVR
{
	//! VR操作先 
	internal class VRAvatar : PlugCommon
	{
		public Transform Origin { get; private set; }
		public Transform Eye { get; private set; }
		public GameObject View { get; private set; }
		public GameObject Head { get; private set; }
		public GameObject LeftHand { get; private set; }
		public GameObject RightHand { get; private set; }
		public GameObject Up { get; private set; }
		public GameObject Fore { get; private set; }

		internal VRAvatar(Loc loc)
		{
			// ひとまず、指定位置から1m下を配置位置とする 
			Origin = CreateRootObject("VRAvatar", loc - new Vector3(0, 1, 0)).transform;
			GameObject.DontDestroyOnLoad(Origin.gameObject);
			// 目を指定位置に合わせる 
			Eye = CreateChildObject("Eye", Origin, loc, true).transform;
			View = CreateChildObject("View", Eye, new Loc(new Vector3(0, 0, -0.1f), Quaternion.identity), false);

			// 表示部 
			Head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			Head.name = "Head";
			GameObject.Destroy(Head.GetComponent<SphereCollider>());
			Head.transform.SetParent(View.transform);
			Loc.Identity.ToLocalTransform(Head.transform);
			Head.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			_addAxes(Head.transform);

			var neck = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			neck.name = "Neck";
			GameObject.Destroy(Head.GetComponent<CapsuleCollider>());
			neck.transform.SetParent(View.transform);
			neck.transform.localPosition = new Vector3(0.0f, -0.075f, 0.0f);
			neck.transform.localRotation = Quaternion.identity;
			neck.transform.localScale = new Vector3(0.05f, 0.075f, 0.05f);

			var shoulder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			shoulder.name = "Shoulder";
			GameObject.Destroy(Head.GetComponent<CapsuleCollider>());
			shoulder.transform.SetParent(View.transform);
			shoulder.transform.localPosition = new Vector3(0.0f, -0.15f, 0.0f);
			shoulder.transform.localRotation = RotUt.RotZ(Mathf.Deg2Rad * 90);
			shoulder.transform.localScale = new Vector3(0.05f, 0.075f, 0.05f);

			LeftHand = CreateChildObject("LeftHand", Origin, Loc.Identity, false);
			_addAxes(LeftHand.transform);

			RightHand = CreateChildObject("RightHand", Origin, Loc.Identity, false);
			_addAxes(RightHand.transform);

			Head.transform.Find("AxisX").gameObject.SetActive(false);
			Up = Head.transform.Find("AxisY").gameObject;
			Fore = Head.transform.Find("AxisZ").gameObject;
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

		private const float _axisLen = 0.4f;
		private static _AxisParam[] _axisParam ={
			new _AxisParam("AxisX", new Vector3(0.4f, 0, 0),new Color(128, 0, 128)),
			new _AxisParam("AxisY", new Vector3(0,0.4f), new Color(0, 255, 0)),
			new _AxisParam("AxisZ", new Vector3(0,0,0.4f), new Color(0, 128, 255)),
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
				lx.widthMultiplier = 0.01f;
			}
		}

		//! 操作構造生成 
		/*!	@note 位置参照の正確性および書き込み手順の正当性を確保するため、
				直接transformへのアクセスをさせるべきではない。
		*/
		internal AvatarControl CreateControl()
		{
			var t = new AvatarControl();
			t.Origin = Loc.FromWorldTransform(Origin);
			var inv = t.Origin.Inversed;
			t.LocalEye = inv * Loc.FromWorldTransform(Eye);
			t.LocalLeftHand = inv * Loc.FromWorldTransform(LeftHand.transform);
			t.LocalRightHand = inv * Loc.FromWorldTransform(RightHand.transform);
			return t;
		}

		//! 操作構造反映 
		internal void UpdateControl(AvatarControl cs)
		{
			cs.Origin.ToWorldTransform(Origin);
			cs.LocalEye.ToLocalTransform(Eye);
			cs.LocalLeftHand.ToLocalTransform(LeftHand.transform);
			cs.LocalRightHand.ToLocalTransform(RightHand.transform);
		}
	}
}
