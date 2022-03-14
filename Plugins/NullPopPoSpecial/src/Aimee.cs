/*!	@file
	@brief NullPopPoSpecial: Raycast 標的 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;

namespace NullPopPoSpecial
{
	//! Raycast 標的 
	public class Aimee
	{
		public Transform Dst{get;protected set;}
		public int ID{get{return Dst.gameObject.GetInstanceID();}}
		public RaycastHit Info{get;private set;}

		public bool IsAimed{get;private set;}

		public static Aimee Create(Transform dst){
			var t=new Aimee();
			t.Dst=dst;
			return t;
		}

		public void Hit(RaycastHit info){
			Info=info;
			IsAimed=true;
			OnHit();
		}
		protected virtual void OnHit(){}

		public void Continue(RaycastHit info){
			Info=info;
			OnContinue();
		}
		protected virtual void OnContinue(){}

		public void Miss(){
			IsAimed=false;
			OnMiss();
		}
		protected virtual void OnMiss(){}
	}
}
