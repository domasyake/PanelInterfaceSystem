using System.Collections;
using System.Collections.Generic;
using Example;
using UnityEngine;
using UnityEngine.UI;

public class VecDisplay : MonoBehaviour{
	public SampleControll sampleControll;

	private Text txt;
	
	private void Start(){
		txt = this.GetComponent<Text>();
	}

	// Update is called once per frame
	void Update (){
		txt.text = sampleControll.listCreateOption.ToString();
	}
}
