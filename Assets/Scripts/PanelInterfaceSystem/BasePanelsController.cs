using System;
using System.Diagnostics;
using UniRx;
using UnityEngine;

namespace PanelInterfaceSystem{
	/// <summary>
	/// This class is a controller template.
	/// This assumes only one type of panel control.
	/// If you create your own controller, this class is not needed.
	/// </summary>
	public abstract class BasePanelsController:MonoBehaviour{
		protected PanelEventSystem panelEventSystem;

		/// <summary>
		/// Define the event of PanelEventSystem.
		/// This method must be called by Start() or Awake().
		/// </summary>
		/// <param name="panel_event_system">PanelEventSystem's instance used by this controller</param>
		/// <typeparam name="TYpe">Types of panels controlled by this controller</typeparam>
		protected void Init<TYpe>(PanelEventSystem panel_event_system)where TYpe: class, ISelectablePanel{
			panelEventSystem = panel_event_system;
			panelEventSystem.EnterEvent
				.Where(n=>n is TYpe)
				.Subscribe(Submit);
			panelEventSystem.FocusEvent
				.Where(n=>n is TYpe)
				.Subscribe(Focus);
			panelEventSystem.OverEvent
				.Subscribe(CheckCursoleOver);
		}

		/// <summary>
		/// Activate controlled panels.
		/// If not overriding, all panels will be operation targets.
		/// </summary>
		protected virtual void ActivatePanels(){
			panelEventSystem.CreateActivePanelsList<ISelectablePanel>(ListCreateOption.Vertical);
		}
		
		/// <summary>
		/// Startup system.
		/// </summary>
		/// <param name="point">Initial selection coordinates.</param>
		protected virtual void Launch(Vector2 point){
			ActivatePanels();
			panelEventSystem.Boot(point.x,point.y);
		}
		
		/// <summary>
		/// when not specifying initial selection coordinates.
		/// </summary>
		protected virtual void Launch(){
			Launch(Vector2.zero);	
		}

		/// <summary>
		/// Process at decision.
		/// </summary>
		/// <param name="i_selectable_panel">Decided panel. Cast this and do the processing.</param>
		protected abstract void Submit(ISelectablePanel i_selectable_panel);

		/// <summary>
		/// Process when the selection target has changed.
		/// </summary>
		/// <param name="i_selectable_panel">Selected panel. Cast this and do the processing.</param>
		protected abstract void Focus(ISelectablePanel i_selectable_panel);

		/// <summary>
		/// Processing when the selected coordinates cross the edge.
		/// </summary>
		/// <param name="cursole_over">Direction beyond edge.</param>
		protected virtual void CheckCursoleOver(CursoleOver cursole_over){}
	}
}