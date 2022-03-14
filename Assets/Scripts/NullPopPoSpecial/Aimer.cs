/*!	@file
	@brief NullPopPoSpecial: Raycast 発生元 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NullPopPoSpecial
{
	//! Raycast 発生元 
	public class Aimer
	{
		public Transform Src;
		public Vector3 Axis;
		public int Layers;
		public float Range=Mathf.Infinity;
		public RaycastHit? Info{get;private set;}

		private Dictionary<int,Aimee> _dst=new Dictionary<int,Aimee>();
		private Aimee _hit;

		public static T Create<T>(Transform src,Vector3 axis,int layers) where T:Aimer,new(){
			var t=new T();
			t.Src=src;
			t.Axis=axis;
			t.Layers=layers;
			return t;
		}

		public void Include(Aimee dst){
			var id=dst.ID;
			if(_dst.ContainsKey(id))_dst.Remove(id);
			_dst.Add(id,dst);
		}

		public void Exclude(Aimee dst){
			var id=dst.ID;
			if(_hit!=null){
				if(id==_hit.ID)_miss();
			}
			if(_dst.ContainsKey(id))_dst.Remove(id);
		}

		private void _try(int id,RaycastHit info){

			if (!_dst.ContainsKey(id)){
				OnHitOther(info);
				return;
			}

			_hit = _dst[id];
			_hit.Hit(info);
			OnHit(_hit);
		}

		private void _miss(){
			if(_hit==null)return;
			OnMiss(_hit);
			_hit.Miss();
			_hit=null;
		}

		public void Clear(){
			_miss();

			if(Info==null)return;
			OnMissOther(Info.Value);
			Info=null;
		}

		public bool Update(){

			if(Layers==0 || Src==null){
				// Raycast 無効 
				Clear();
				return false;
			}

			RaycastHit info;
			if(!Physics.Raycast(Src.position, Src.rotation * Axis, out info, Range, Layers)){
				// Raycast 失敗 
				Clear();
				return false;
			}

			var id =info.transform.gameObject.GetInstanceID();
			if (_hit==null){
				if(Info==null){
					// 新規hit 
					_try(id,info);
				}
				else if(info.transform==Info.Value.transform){
					// 未登録hit継続中 
					OnContinueOther(info);
				}
				else{
					// 未登録hitからの変更 
					Clear();
					_try(id,info);
				}
			}
			else if(id==_hit.ID){
				// hit継続中 
				_hit.Continue(info);
				OnContinue(_hit);
			}
			else{
				// hit変更 
				_miss();
				_try(id,info);
			}
			Info=info;
			return true;
		}
		protected virtual void OnHit(Aimee target){}
		protected virtual void OnContinue(Aimee target){}
		protected virtual void OnMiss(Aimee target){}

		protected virtual void OnHitOther(RaycastHit info){}
		protected virtual void OnContinueOther(RaycastHit info){}
		protected virtual void OnMissOther(RaycastHit info){}
	}
}
