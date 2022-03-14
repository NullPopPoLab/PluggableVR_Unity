/*!	@file
	@brief PluggableVR: Raycast 標的サンプル 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

//! Raycast 標的サンプル 
public class TestAimee : Aimee
{
	public static new TestAimee Create(Transform dst){
		var t=new TestAimee();
		t.Dst=dst;
		return t;
	}

	protected override void OnHit(){
		base.OnHit();
		Debug.Log(""+this+": hit ");
	}

	protected override void OnContinue(){
		base.OnContinue();
	}

	protected override void OnMiss(){
		base.OnMiss();
		Debug.Log(""+this+": miss ");
	}
}
