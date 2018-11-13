using PanelInterfaceSystem;
using UnityEngine;

namespace Example{
	public class SampleControll : BasePanelsController{
		public Vector2 startPoint;
		public ListCreateOption listCreateOption;
		public PanelEventSystem panelEventSystem;
		
		private void Start(){
			Init<SamplePanel>(panelEventSystem);
			
			base.Launch(startPoint);
		}


		protected override void Focus(ISelectablePanel i_selectable_panel){
			var panel = i_selectable_panel as SamplePanel;
			Debug.Log("Controller focused-" + panel.name);
		}
		
		protected override void Submit(ISelectablePanel i_selectable_panel){
			var panel = i_selectable_panel as SamplePanel;
			Debug.Log("Controller selected-" + panel.name);
		}
		
		protected override void CreateList(){
			panelEventSystem.CreateActivePanelsList<SamplePanel>(listCreateOption);
		}

		public void Run(){
			base.Launch(startPoint);
		}	
	}
}