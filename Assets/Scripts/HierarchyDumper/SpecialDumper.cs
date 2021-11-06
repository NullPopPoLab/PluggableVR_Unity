/*!	@file
	@brief HierarchyDumper: 型別の情報取得 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchyDumper
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
			Selector["UnityEngine.Animator"] = (o, i) => new Dumper_Animator(o).Dump(i);
			Selector["UnityEngine.Avatar"] = (o, i) => new Dumper_Avatar(o).Dump(i);
			Selector["UnityEngine.BoxCollider"] = (o, i) => new Dumper_BoxCollider(o).Dump(i);
			Selector["UnityEngine.Button"] = (o, i) => new Dumper_Button(o).Dump(i);
			Selector["UnityEngine.Camera"] = (o, i) => new Dumper_Camera(o).Dump(i);
			Selector["UnityEngine.Canvas"] = (o, i) => new Dumper_Canvas(o).Dump(i);
			Selector["UnityEngine.CanvasScaler"] = (o, i) => new Dumper_CanvasScaler(o).Dump(i);
			Selector["UnityEngine.CapsuleCollider"] = (o, i) => new Dumper_CapsuleCollider(o).Dump(i);
			Selector["UnityEngine.Dropdown"] = (o, i) => new Dumper_Dropdown(o).Dump(i);
			Selector["UnityEngine.InputField"] = (o, i) => new Dumper_InputField(o).Dump(i);
			Selector["UnityEngine.Light"] = (o, i) => new Dumper_Light(o).Dump(i);
			Selector["UnityEngine.LineRenderer"] = (o, i) => new Dumper_LineRenderer(o).Dump(i);
			Selector["UnityEngine.MeshCollider"] = (o, i) => new Dumper_MeshCollider(o).Dump(i);
			Selector["UnityEngine.MeshRenderer"] = (o, i) => new Dumper_MeshRenderer(o).Dump(i);
			Selector["UnityEngine.RectTransform"] = (o, i) => new Dumper_RectTransform(o).Dump(i);
			Selector["UnityEngine.Renderer"] = (o, i) => new Dumper_Renderer(o).Dump(i);
			Selector["UnityEngine.Rigidbody"] = (o, i) => new Dumper_Rigidbody(o).Dump(i);
			Selector["UnityEngine.SkinnedMeshRenderer"] = (o, i) => new Dumper_SkinnedMeshRenderer(o).Dump(i);
			Selector["UnityEngine.Slider"] = (o, i) => new Dumper_Slider(o).Dump(i);
			Selector["UnityEngine.SphereCollider"] = (o, i) => new Dumper_SphereCollider(o).Dump(i);
			Selector["UnityEngine.Text"] = (o, i) => new Dumper_Text(o).Dump(i);
			Selector["UnityEngine.Toggle"] = (o, i) => new Dumper_Toggle(o).Dump(i);
			Selector["UnityEngine.Transform"] = (o, i) => new Dumper_Transform(o).Dump(i);
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
			var type = obj.GetType();
			var name = type.Name;
			if (!String.IsNullOrEmpty(type.Namespace)) name = type.Namespace + "." + name;
			if (!Selector.ContainsKey(name)) return "";
			return Selector[name](obj, indent);
		}
	}
}
