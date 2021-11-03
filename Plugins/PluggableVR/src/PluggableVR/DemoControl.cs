/*!	@file
	@brief PluggableVR: デモ用VR操作構造 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using NullPopPoSpecial;

namespace PluggableVR
{
	//! デモ用VR操作構造 
	public class DemoControl: VRControl
	{
		public Loc Origin; //!< 移動基準点(主に足元) 
		public Loc LocalPivot; //!< 回転基準位置 
		public Loc LocalEye; //!< 基準点からの目位置差分 
		public Loc LocalLeftHand; //!< 基準点からの左手位置差分 
		public Loc LocalRightHand; //!< 基準点からの右手位置差分 

		//! ワールド回転基準位置 
		public Loc WorldPivot
		{
			get { return Origin * LocalPivot; }
			set { LocalPivot = Origin.Inversed * value; }
		}
		//! ワールド目位置 
		public Loc WorldEye
		{
			get { return Origin * LocalEye; }
			set { LocalEye = Origin.Inversed * value; }
		}
		//! ワールド左手位置 
		public Loc WorldLeftHand
		{
			get { return Origin * LocalLeftHand; }
			set { LocalLeftHand = Origin.Inversed * value; }
		}
		//! ワールド右手位置 
		public Loc WorldRightHand
		{
			get { return Origin * LocalRightHand; }
			set { LocalRightHand = Origin.Inversed * value; }
		}
	}
}
