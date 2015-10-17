using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class FloorSelectionTool : MonoBehaviour {



	List <GameObject> floors = new List<GameObject>();

	public Text txtFloor;
	public GameObject mainCam;

	public GameObject f1;
	public GameObject f2;
	public GameObject f3;
	public GameObject f4;
	public GameObject f5;
	public GameObject f6;
	public GameObject f7;
	public GameObject f8;
	public GameObject f9;
	public GameObject f10;
	public GameObject f11;
	public GameObject f12;


	int camPOS = 6;

	void Start()
	{
		floors.Clear ();
		if(f1 != null)
			floors.Add (f1);
		if(f2 != null)
			floors.Add (f2);
		if(f3 != null)
			floors.Add (f3);
		if(f4 != null)
			floors.Add (f4);
		if(f5 != null)
			floors.Add (f5);
		if(f6 != null)
			floors.Add (f6);
		if(f7 != null)
			floors.Add (f7);
		if(f8 != null)
			floors.Add (f8);
		if(f9 != null)
			floors.Add (f9);
		if(f10 != null)
			floors.Add (f10);
		if(f11 != null)
			floors.Add (f11);
		if(f12 != null)
			floors.Add (f12);
		gameObject.GetComponent<Slider>().maxValue = floors.Count;
	}
	void Update()
	{
		if(Input.GetAxis("Mouse ScrollWheel") <= -0.1f && camPOS > 1)
		{
			mainCam.GetComponent<CameraFollow> ().ResetView(camPOS-1);
			gameObject.GetComponent<Slider>().value = camPOS-1;
		}
		if(Input.GetAxis("Mouse ScrollWheel") >= 0.1f && camPOS < floors.Count)
		{
			mainCam.GetComponent<CameraFollow> ().ResetView(camPOS+1);
			gameObject.GetComponent<Slider>().value = camPOS+1;
		}
	}
	public void ChangeFloor(Slider selector)
	{
		camPOS = (int)selector.value;
		mainCam.GetComponent<CameraFollow> ().ResetView ((int)selector.value);

		for (int i = 0; i < floors.Count; i++) 
		{
			if(i < (int)selector.value)
			{
				floors[i].SetActive(true);
			}
			else
			{
				floors [i].SetActive (false);
			}
		}
		if(selector.value == 1)
		{
			txtFloor.text = "Lobby";
		}
		else if(selector.value == selector.maxValue)
		{
			txtFloor.text = "Roof";
		}
		else
		{
			txtFloor.text = "Floor "+selector.value.ToString ();
		}
	}

}
