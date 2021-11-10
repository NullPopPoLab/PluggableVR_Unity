/*!	@file
	@brief HierarchyDumper: キャラ監視 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System.Collections.Generic;
using UnityEngine;
using NullPopPoSpecial;
using HierarchyDumper;

namespace PluggableVR_KKS
{
	//! キャラ監視 
	internal class CharaObserver
	{
		internal GameObject Target{get;private set;}

		internal CharaObserver(GameObject target){
			Target=target;
		}
	}
}
