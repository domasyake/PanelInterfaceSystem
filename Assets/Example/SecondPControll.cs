using PanelInterfaceSystem;
using UnityEngine;

namespace Example{
	public class SecondPControll:BasePanelsController{
		public Vector2 startPoint;
		public PanelEventSystem panelEventSystem;
		
		private void Start(){
			Init<SamplePanel2>(panelEventSystem);
			panelEventSystem.SetLoopAble = true;
			base.Launch(startPoint);
		}


		protected override void Focus(ISelectablePanel i_selectable_panel){
			var panel = i_selectable_panel as SamplePanel2;
			Debug.Log("SecondController focused-" + panel.name);
		}
		
		protected override void Submit(ISelectablePanel i_selectable_panel){
			var panel = i_selectable_panel as SamplePanel2;
			Debug.Log("SecondController selected-" + panel.name);
		}
		
		protected override void CreateList(){
			panelEventSystem.CreateActivePanelsList<SamplePanel2>(ListCreateOption.Vertical);
		}

		public void Run(){
			base.Launch(startPoint);
		}
	}
}