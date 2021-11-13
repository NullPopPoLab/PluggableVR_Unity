﻿/*!	@file
	@brief HierarchyDumper: 追加Dumperクラス登録 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;

namespace PluggableVR_CS2
{
	//! 追加Dumperクラス登録 
	internal static class Dumper
	{
		internal static void Register(){
			SpecialDumper.Register("ChaControl", (o, i) => new Dumper_ChaControl(o).Dump(i));
			SpecialDumper.Register("DynamicBone",(o,i)=>new Dumper_DynamicBone(o).Dump(i));
			SpecialDumper.Register("DynamicBoneCollider",(o,i)=>new Dumper_DynamicBoneCollider(o).Dump(i));
		}
	}
}
