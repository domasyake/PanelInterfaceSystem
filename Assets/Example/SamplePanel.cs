﻿using PanelInterfaceSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Example{
	public class SamplePanel:MonoBehaviour,ISelectablePanel{
		private Image img;
		public bool IsActive{ get; set; }

		private void Awake(){
			img = this.GetComponent<Image>();
		}
		
		public void OnSelect(){
			img.color=Color.blue;
		}

		public void RemoveSelect(){
			img.color=Color.white;
		}

		public void Submit(){
			img.color=Color.red;
		}

	}
}