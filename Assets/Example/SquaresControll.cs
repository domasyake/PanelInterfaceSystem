using PanelInterfaceSystem;
using UnityEngine;

namespace Example{
	public class SquaresControll : BasePanelsController{
		public Vector2 startPoint;
		public int[] structure=new int[]{3,3,2};

		private void Start(){
			Init<SamplePanel>(this.GetComponent<PanelEventSystem>());

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
			panelEventSystem.CreateActivePanelsList<SamplePanel>(structure);
		}

		public void Run(){
			base.Launch(startPoint);
		}
	}
}