using PanelInterfaceSystem;
using UnityEngine;

namespace Example{
	/// <summary>
	/// This class exists because it can not reference the same multiple class from ButtonComponent.
	/// </summary>
	public class InputLap : MonoBehaviour{
		public PanelEventSystem panelEventSystem;
		

		public void Up(){
			panelEventSystem.Up();
		}

		public void Down(){
			panelEventSystem.Down();
		}

		public void Right(){
			panelEventSystem.Right();
		}

		public void Left(){
			panelEventSystem.Left();
		}

		public void Enter(){
			panelEventSystem.Enter();
		}

		public void ReBoot(){
			panelEventSystem.ReBoot();
		}
	}
}