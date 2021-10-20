/*!	@file
	@brief PluggableVR: 型別の情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableVR
{
	//! 型別の情報取得 
	public class SpecialDumper
	{
		//! 型別動作 
		/*!	@param obj 取得対象
			@param indent インデント文字列
			@return 生成された文字列
		*/
		public delegate string CB_Dumper(object obj, string indent);
		public static Dictionary<string, CB_Dumper> Selector;

		private static SpecialDumper _instance;

		public SpecialDumper()
		{
			Selector = new Dictionary<string, CB_Dumper>();
			Selector["Animator"] = (o, i) => new Dumper_Animator(o).Dump(i);
			Selector["Avatar"] = (o, i) => new Dumper_Avatar(o).Dump(i);
			Selector["Button"] = (o, i) => new Dumper_Button(o).Dump(i);
			Selector["Camera"] = (o, i) => new Dumper_Camera(o).Dump(i);
			Selector["Canvas"] = (o, i) => new Dumper_Canvas(o).Dump(i);
			Selector["CanvasScaler"] = (o, i) => new Dumper_CanvasScaler(o).Dump(i);
			Selector["Dropdown"] = (o, i) => new Dumper_Dropdown(o).Dump(i);
			Selector["InputField"] = (o, i) => new Dumper_InputField(o).Dump(i);
			Selector["Light"] = (o, i) => new Dumper_Light(o).Dump(i);
			Selector["LineRenderer"] = (o, i) => new Dumper_LineRenderer(o).Dump(i);
			Selector["MeshRenderer"] = (o, i) => new Dumper_MeshRenderer(o).Dump(i);
			Selector["RectTransform"] = (o, i) => new Dumper_RectTransform(o).Dump(i);
			Selector["Renderer"] = (o, i) => new Dumper_Renderer(o).Dump(i);
			Selector["SkinnedMeshRenderer"] = (o, i) => new Dumper_SkinnedMeshRenderer(o).Dump(i);
			Selector["Slider"] = (o, i) => new Dumper_Slider(o).Dump(i);
			Selector["Text"] = (o, i) => new Dumper_Text(o).Dump(i);
			Selector["Toggle"] = (o, i) => new Dumper_Toggle(o).Dump(i);
			Selector["Transform"] = (o, i) => new Dumper_Transform(o).Dump(i);
		}

		//! 型別の動作を登録 
		public static void Register(string name, CB_Dumper cb)
		{
			if (_instance == null) _instance = new SpecialDumper();
			Selector[name] = cb;
		}

		public static string Dump(object obj, string indent = "")
		{
			if (_instance == null) _instance = new SpecialDumper();
			var name = obj.GetType().Name;
			if (!Selector.ContainsKey(name)) return "";
			return Selector[name](obj, indent);
		}
	}
}
