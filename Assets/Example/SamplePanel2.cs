﻿using PanelInterfaceSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Example{
	public class SamplePanel2:MonoBehaviour,ISelectablePanel{
		private Image img;

		private void Awake(){
			img = this.GetComponent<Image>();
		}

		public bool IsActive{ get; set; }
		
		public void OnSelect(){
			img.color=Color.green;
		}

		public void RemoveSelect(){
			img.color=Color.white;
		}

		public void Submit(){
			img.color=Color.yellow;
		}

	}
}