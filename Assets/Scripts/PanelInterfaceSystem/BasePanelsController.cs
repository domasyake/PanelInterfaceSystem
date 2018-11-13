using System;
using System.Diagnostics;
using UniRx;
using UnityEngine;

namespace PanelInterfaceSystem{
	public abstract class BasePanelsController:MonoBehaviour{
		protected PanelEventSystem panelEventSystem;

		protected void Init<TYpe>(PanelEventSystem panel_event_system)where TYpe: class, ISelectablePanel{
			panelEventSystem = panel_event_system;
			panelEventSystem.EnterEvent
				.Where(n=>n is TYpe)
				.Subscribe(Submit);
			panelEventSystem.FocusEvent
				.Where(n=>n is TYpe)
				.Subscribe(Focus);
			panel_event_system.OverEvent
				.Subscribe(CheckCursoleOver);
		}

		protected abstract void CreateList();
		
		protected virtual void Launch(Vector2 point){
			CreateList();
			panelEventSystem.Boot(point.x,point.y);
		}
		
		protected virtual void Launch(){
			Launch(Vector2.zero);	
		}

		protected abstract void Submit(ISelectablePanel i_selectable_panel);

		protected abstract void Focus(ISelectablePanel i_selectable_panel);

		protected virtual void CheckCursoleOver(CursoleOver cursole_over){}
	}
}