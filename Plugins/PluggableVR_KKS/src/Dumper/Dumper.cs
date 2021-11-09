/*!	@file
	@brief HierarchyDumper: 追加Dumperクラス登録 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;

namespace PluggableVR_KKS
{
	//! 追加Dumperクラス登録 
	internal static class Dumper
	{
		internal static void Register(){
			SpecialDumper.Register("DynamicBoneCollider",(o,i)=>new Dumper_DynamicBoneCollider(o).Dump(i));
			SpecialDumper.Register("FadeCanvas", (o, i) => new Dumper_FadeCanvas(o).Dump(i));
			SpecialDumper.Register("HomeMenu", (o, i) => new Dumper_HomeMenu(o).Dump(i));
			SpecialDumper.Register("MinimapIcon", (o, i) => new Dumper_MinimapIcon(o).Dump(i));
		}
	}
}
