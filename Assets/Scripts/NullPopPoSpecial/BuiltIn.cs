/*!	@file
	@brief NullPopPoSpecial: ビルトインリソース 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;

namespace NullPopPoSpecial
{
	//! ビルトインリソース 
	public static class BuiltIn
	{
		public static T Load<T>(string path) where T:UnityEngine.Object{
#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetBuiltinExtraResource<T>(path);
#else
			return Resources.GetBuiltinResource<T>(path);
#endif
		}
	}
}
