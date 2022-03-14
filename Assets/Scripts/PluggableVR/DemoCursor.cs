/*!	@file
	@brief PluggableVR: デモ用VRカーソル 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR
{
	//! デモ用VRカーソル 
	public class DemoCursor: VRCursor
	{
		private LineRenderer _line;

		public override Color Color
		{
			set {
				base.Color = value;
				_updateColor();
			}
		}

		public override float Alpha
		{
			set {
				base.Alpha = value;
				_updateColor();
			}
		}


		public DemoCursor()
		{
			Ctrl = CreateRootObject("VRCursor", Loc.Identity).transform;
			View= CreateChildObject("GazeIcon", Ctrl, Loc.Identity, false).transform;
			View.gameObject.SetActive(false);

			_line = View.gameObject.AddComponent<LineRenderer>();
			_line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			_line.receiveShadows = false;
			_line.useWorldSpace = true;
			_line.startWidth = _line.endWidth = 0.02f;
			_line.material = BuiltIn.Load<Material>("Sprites-Default.mat");

			_line.numCapVertices = 5;
			_line.numCornerVertices = 3;
			_line.SetPositions(new Vector3[] { new Vector3(), new Vector3() });

			Color = new Color(1, 0, 0, 0.75f);
		}

		protected override void OnUpdate() {
			base.OnUpdate();

			_line.SetPosition(0, Ctrl.position);
			_line.SetPosition(1, (Pointer==null)?Ctrl.position:Pointer.position);
		}

		private void _updateColor(){

			var a = Color.a * Alpha;
			_line.startColor = new Color(Color.r, Color.g, Color.b, a);
			_line.endColor = new Color(Color.r, Color.g, Color.b, a * 0.5f);
		}
	}
}
