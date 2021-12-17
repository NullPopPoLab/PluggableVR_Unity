/*!	@file
	@brief PluggableVR: Raycast 発生元サンプル 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

//! Raycast 発生元サンプル 
public class TestAimer : Aimer
{
	public static TestAimer Create(){
		var t=new TestAimer();
		t.Axis=new Vector3(0,0,1);
		t.Layers=1<<8;
		return t;
	}

	protected override void OnHit(Aimee target){
		base.OnHit(target);
		Debug.Log(""+this+": Hit "+target);
	}

	protected override void OnContinue(Aimee target){
		base.OnContinue(target);
	}

	protected override void OnMiss(Aimee target){
		base.OnMiss(target);
		Debug.Log(""+this+": Miss "+target);
	}

	protected override void OnHitOther(RaycastHit info){
		base.OnHitOther(info);
		Debug.Log(""+this+": hit "+info.transform);
	}

	protected override void OnMissOther(RaycastHit info){
		base.OnMissOther(info);
		Debug.Log(""+this+": miss "+info.transform);
	}
}
