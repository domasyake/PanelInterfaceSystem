using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace PanelInterfaceSystem{
	public enum CursoleOver{
		Top,Under,Right,Left,
	}
	public enum ListCreateOption{
		Horizontal,Vertical,
	}
	/// <summary>
	/// This class provides control of selectable panels.
	/// You can receive each event and process it.
	/// When using this system, specify panels that you want to operate with CreateActivePanelsList() and Then call Boot().
	/// This system becomes non active every time you select it.
	/// If you want to select a panel of the same type again, you should call ReBoot().
	/// </summary>
	public class PanelEventSystem : MonoBehaviour{
		[SerializeField] private bool isActive;
		[SerializeField] private bool loopAble;public bool SetLoopAble{set{ loopAble = value; }}

		private Vector2 selectedPoint;public Vector2 GetSelectedPoint{get{return selectedPoint;}}
		
		private List<ISelectablePanel> iSelectablePanels;
		private readonly List<List<ISelectablePanel>> activeIselectables=new List<List<ISelectablePanel>>();
		
		private readonly Subject<ISelectablePanel> focusStream=new Subject<ISelectablePanel>();
		public UniRx.IObservable<ISelectablePanel> FocusEvent{get{ return focusStream; }}
		
		private readonly Subject<ISelectablePanel> enterStream=new Subject<ISelectablePanel>();
		public UniRx.IObservable<ISelectablePanel> EnterEvent{get{ return enterStream; }}
		
		private readonly Subject<CursoleOver> overStream=new Subject<CursoleOver>();
		public UniRx.IObservable<CursoleOver> OverEvent{get{ return overStream; }}

		
		private void Awake (){
			//Get a static ISelectablePanels.
			//If you added the panel dynamically, please call RegistrationSelectablePanels().
			iSelectablePanels=transform.GetComponentsInChildren<ISelectablePanel>().ToList();
		}
		
		//Recieve key inputs.
		public void Up(){
			SelectMove(Vector2.down);
		}

		public void Down(){
			SelectMove(Vector2.up);
		}

		public void Right(){
			SelectMove(Vector2.right);
		}

		public void Left(){
			SelectMove(Vector2.left);
		}

		public void Enter(){
			Submit();
		}

		//public methods.
		
		/// <summary>
		/// Activate panels. It needs to be called before call Boot().
		/// </summary>
		/// <param name="structure">This creates a selection range.</param>
		/// <typeparam name="TYpe">Type of panel to activate.</typeparam>
		public void CreateActivePanelsList<TYpe>(int[] structure)where TYpe : ISelectablePanel{
			activeIselectables.Clear();

			//Activate panels of type of argument that inherits ISelectablePanel
			var target_selectables = new List<ISelectablePanel>();
			foreach (var item in iSelectablePanels){
				if (item is TYpe){
					item.IsActive = true;
					target_selectables.Add(item);
				}else{
					item.IsActive = false;
				}
			}
			
			foreach (var i in Enumerable.Range(0,structure.Length)){
				var brekafg = false;
				var list_row = new List<ISelectablePanel>();
				activeIselectables.Add(list_row);
				IEnumerable<int> repeat;
				try{
					repeat = Enumerable.Range(0,structure[i]);
				} catch (ArgumentOutOfRangeException e){
					Debug.Log("The structure contains a negative number.(index,num)=("+i+","+structure[i]+")");
					break;
				}
				
				foreach (var nouse in repeat){
					list_row.Add(target_selectables.Pop());
					if (target_selectables.Count == 0){
						brekafg = true;
						break;
					}
				}
				if(brekafg){break;}
			}
		}

		/// <summary>
		/// Activate a horizontal or vertical panels. 
		/// </summary>
		/// <param name="option">horizontal or vertical</param>
		/// <typeparam name="TYpe">Type of panel to activate.</typeparam>
		public void CreateActivePanelsList<TYpe>(ListCreateOption option) where TYpe : ISelectablePanel{
			switch (option){
				case ListCreateOption.Horizontal:
					CreateActivePanelsList<TYpe>(new int[]{1000});
					break;
				case ListCreateOption.Vertical:
					CreateActivePanelsList<TYpe>(Enumerable.Repeat(1,1000).ToArray());
					break;
				default:
					throw new ArgumentOutOfRangeException("option", option, null);
			}
		}

		/// <summary>
		/// Start controlling panel.
		/// </summary>
		/// <param name="x">First selection point</param>
		/// <param name="y">First selection point</param>
		public void Boot(float x=0,float y=0){
			foreach (var i in Enumerable.Range(0,activeIselectables.Count)){
				activeIselectables[i] = activeIselectables[i].Where(n => n.IsActive).ToList();
			}
			activeIselectables.RemoveAll(n => n.Count == 0);
			foreach (var item in activeIselectables){
				foreach (var jtem in item){
					jtem.RemoveSelect();
				}
			}
			selectedPoint= new Vector2(x,y);
			Select(selectedPoint);
			isActive = true;
		}
		
		/// <summary>
		/// Restart controlling.
		/// </summary>
		public void ReBoot(){
			isActive = true;
		}
		
		/// <summary>
		/// Stopped controlling.
		/// Stopping the movement but keeping the active panel.
		/// </summary>
		public void Freeze(){
			isActive = false;
		}
	

		/// <summary>
		/// Add panel to controll panels.
		/// </summary>
		/// <param name="selectable">The panel You want to add.</param>
		public void RegistrationSelectablePanels(ISelectablePanel selectable){
			if (iSelectablePanels.Contains(selectable)){
				Debug.Log(selectable+" is contain in mylist");
				return;
			}
			iSelectablePanels.Add(selectable);
		}
		
		/// <summary>
		/// Add panels to controll panels.
		/// </summary>
		/// <param name="selectables">The panels You want to add.</param>
		public void RegistrationSelectablePanels(IEnumerable<ISelectablePanel> selectables){
			foreach (var item in selectables){
				RegistrationSelectablePanels(item);
			}
		}		
		
		//private methods.
		
		private void Submit(){
			if (!isActive){
				return;
			}
			var target = activeIselectables[(int) selectedPoint.y][(int) selectedPoint.x];
			target.Submit();
			isActive = false;
			enterStream.OnNext(target);
		}

		private void Select(Vector2 point){
			try{
				var target = activeIselectables[(int) point.y][(int) point.x];
				target.OnSelect();
				focusStream.OnNext(target);
			}catch (ArgumentOutOfRangeException e){
				Debug.Log(e);
			}
		}

		private void SelectMove(Vector2 movement){
			if (!isActive){
				return;
			}
			activeIselectables[(int) selectedPoint.y][(int) selectedPoint.x].RemoveSelect();
			
			selectedPoint+=movement;
			
			if (selectedPoint.y < 0){
				if (loopAble){
					selectedPoint.y = activeIselectables.Count - 1;
				} else{
					overStream.OnNext(CursoleOver.Top);
					selectedPoint.y = 0;
				}
			}else if (selectedPoint.y > activeIselectables.Count - 1){
				if (loopAble){
					selectedPoint.y = 0; 
				}else{
					overStream.OnNext(CursoleOver.Under);
					selectedPoint.y = activeIselectables.Count-1;	
				}
			}

			if (selectedPoint.x < 0){
				if (loopAble){
					selectedPoint.x = activeIselectables[(int) selectedPoint.y].Count - 1;
				} else{
					overStream.OnNext(CursoleOver.Left);
					selectedPoint.x = 0;
				}
			}else if (selectedPoint.x > activeIselectables[(int) selectedPoint.y].Count - 1){
				if (loopAble){ selectedPoint.x = 0; } else{
					overStream.OnNext(CursoleOver.Right);
					selectedPoint.x = activeIselectables[(int) selectedPoint.y].Count - 1;
				}
			}

			Select(selectedPoint);
		}
	}

	/// <summary>
	/// This class provides List's extension method.
	/// </summary>
	public static class ListExtensions{
		public static T Pop<T>(this IList<T> self){
			var res = self[0];
			self.RemoveAt(0);
			return res;
		}
	}
}
