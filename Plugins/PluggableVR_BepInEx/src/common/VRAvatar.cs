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
		public GameObject AxisY { get; private set; }
		public GameObject AxisZ { get; private set; }

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
			Head.transform.SetParent(View.transform);
			Loc.Identity.ToLocalTransform(Head.transform);
			Head.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

			AxisY = CreateChildObject("AxisY", Head.transform, Loc.Identity, false);
			var ly = AxisY.AddComponent<LineRenderer>();
			ly.useWorldSpace = false;
			ly.receiveShadows = false;
			ly.SetPositions(new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0.2f, 0) });
			ly.material = new Material(Shader.Find("Sprites/Default"));
			ly.startColor = ly.endColor = new Color(0, 255, 0);
			ly.numCapVertices = 5;
			ly.widthMultiplier = 0.05f;

			AxisZ = CreateChildObject("AxisZ", Head.transform, Loc.Identity, false);
			var lz = AxisZ.AddComponent<LineRenderer>();
			lz.useWorldSpace = false;
			lz.receiveShadows = false;
			lz.SetPositions(new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0.2f) });
			lz.material = new Material(Shader.Find("Sprites/Default"));
			lz.startColor = lz.endColor = new Color(128, 255, 128);
			lz.numCapVertices = 5;
			lz.widthMultiplier = 0.05f;
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
//			t.LocalLeftHand = inv * Loc.FromWorldTransform(LeftHand);
//			t.LocalRightHand = inv * Loc.FromWorldTransform(RightHand);
			return t;
		}

		//! 操作構造反映 
		internal void UpdateControl(AvatarControl cs)
		{
			cs.Origin.ToWorldTransform(Origin);
			cs.LocalEye.ToLocalTransform(Eye);
//			cs.LocalLeftHand.ToLocalTransform(LeftHand);
//			cs.LocalRightHand.ToLocalTransform(RightHand);
		}
	}
}
