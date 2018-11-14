using PanelInterfaceSystem;
using UnityEngine;

namespace Example{
	public class SecondPControll:BasePanelsController{
		public Vector2 startPoint;
		public PanelEventSystem panelES;
		
		private void Start(){
			Init<SamplePanel2>(panelES);
			panelEventSystem.SetLoopAble = true;
			base.Launch(startPoint);
		}


		protected override void Focus(ISelectablePanel i_selectable_panel){
			var panel = i_selectable_panel as SamplePanel2;
			Debug.Log("Second Controller focused-" + panel.name);
		}
		
		protected override void Submit(ISelectablePanel i_selectable_panel){
			var panel = i_selectable_panel as SamplePanel2;
			Debug.Log("Second Controller selected-" + panel.name);
		}
		
		protected override void CheckCursoleOver(CursoleOver cursole_over){
			Debug.Log("Second CursoleOver-"+cursole_over);
		}

		
		protected override void ActivatePanels(){
			panelEventSystem.CreateActivePanelsList<SamplePanel2>(ListCreateOption.Vertical);
		}

		public void Run(){
			base.Launch(startPoint);
		}
	}
}