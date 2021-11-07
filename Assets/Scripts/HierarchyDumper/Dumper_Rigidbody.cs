/*!	@file
	@brief HierarchyDumper: Rigidbody 情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace HierarchyDumper
{
	//! Rigidbody 情報取得 
	public struct Dumper_Rigidbody
	{
		private Rigidbody _obj;

		public Dumper_Rigidbody(object obj) { _obj = obj as Rigidbody; }

		public string Dump(string indent = "")
		{
			if (_obj == null) return DumpForm.TypeMismatch;

			var s = "";
			s += indent + "IsKinematic: " + _obj.isKinematic + "\n";
			s += indent + "UseGravity: " + _obj.useGravity + "\n";
			s += indent + "Position: " + DumpForm.From(_obj.position) + "\n";
			s += indent + "RotationX: " + DumpForm.From(RotUt.AxisX(_obj.rotation)) + "\n";
			s += indent + "RotationY: " + DumpForm.From(RotUt.AxisY(_obj.rotation)) + "\n";
			s += indent + "RotationZ: " + DumpForm.From(RotUt.AxisZ(_obj.rotation)) + "\n";
			s += indent + "DetectCollisions: " + _obj.detectCollisions + "\n";
			s += indent + "CollisionDetectionMode: " + _obj.collisionDetectionMode + "\n";
			s += indent + "Constraints: " + _obj.constraints + "\n";
			s += indent + "Mass: " + _obj.mass + "\n";
			s += indent + "CenterOfMass: " + DumpForm.From(_obj.centerOfMass) + "\n";
			s += indent + "Drag: " + _obj.drag + "\n";
			s += indent + "AngularDrag: " + _obj.angularDrag + "\n";
			s += indent + "Velocity: " + DumpForm.From(_obj.velocity) + "\n";
			s += indent + "AngularVelocity: " + DumpForm.From(_obj.angularVelocity) + "\n";
			s += indent + "MaxAngularVelocity: " + _obj.maxAngularVelocity + "\n";
			s += indent + "MaxDepenetrationVelocity: " + _obj.maxDepenetrationVelocity + "\n";
			s += indent + "FreezeRotation: " + _obj.freezeRotation + "\n";
			s += indent + "InertiaTensor: " + DumpForm.From(_obj.inertiaTensor) + "\n";
			s += indent + "InertiaTensorRotationX: " + DumpForm.From(RotUt.AxisZ(_obj.inertiaTensorRotation)) + "\n";
			s += indent + "InertiaTensorRotationY: " + DumpForm.From(RotUt.AxisZ(_obj.inertiaTensorRotation)) + "\n";
			s += indent + "InertiaTensorRotationZ: " + DumpForm.From(RotUt.AxisZ(_obj.inertiaTensorRotation)) + "\n";

			return s;
		}
	}
}
