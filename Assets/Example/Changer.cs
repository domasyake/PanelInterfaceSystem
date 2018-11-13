﻿using PanelInterfaceSystem;
using UnityEngine;

namespace Example{
	public class Changer : BasePanelsController{
		public Vector2 startPoint;
		public ListCreateOption listCreateOption;
		public PanelEventSystem panelEventSystem;
		public Changer2 changer2;
		
		private void Start(){
			Init<SamplePanel>(panelEventSystem);
			panelEventSystem.SetLoopAble = true;
			base.Launch(startPoint);
		}


		protected override void Focus(ISelectablePanel i_selectable_panel){
			var panel = i_selectable_panel as SamplePanel;
			Debug.Log("Controller focused-" + panel.name);
		}

		protected override void Submit(ISelectablePanel i_selectable_panel){
			var panel = i_selectable_panel as SamplePanel;
			Debug.Log("Controller selected-" + panel.name);
			changer2.Run();
		}

		protected override void CreateList(){
			panelEventSystem.CreateActivePanelsList<SamplePanel>(listCreateOption);
		}

		public void Run(){
			base.Launch(startPoint);
		}
	}
}