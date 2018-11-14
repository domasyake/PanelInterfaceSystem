using PanelInterfaceSystem;
using UnityEngine;

namespace Example{
	public class Changer2 : BasePanelsController{
		public Vector2 startPoint;
		public ListCreateOption listCreateOption;
		public PanelEventSystem panelES;
		public Changer changer;

		private void Start(){
			Init<SamplePanel2>(panelES);
			panelES.SetLoopAble = true;
		}


		protected override void Focus(ISelectablePanel i_selectable_panel){
			var panel = i_selectable_panel as SamplePanel2;
			Debug.Log("SecondController focused-" + panel.name);
		}

		protected override void Submit(ISelectablePanel i_selectable_panel){
			var panel = i_selectable_panel as SamplePanel2;
			Debug.Log("SecondController selected-" + panel.name);
			changer.Run();
		}

		protected override void ActivatePanels(){
			panelEventSystem.CreateActivePanelsList<SamplePanel2>(listCreateOption);
		}

		public void Run(){
			base.Launch(startPoint);
		}
	}
}