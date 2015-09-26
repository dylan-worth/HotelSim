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

	int camPOS = 6;

	void Start()
	{
		floors.Clear ();
		floors.Add (f1);
		floors.Add (f2);
		floors.Add (f3);
		floors.Add (f4);
		floors.Add (f5);
		floors.Add (f6);

	}
	void Update()
	{
		if(Input.GetAxis("Mouse ScrollWheel") <= -0.1f && camPOS > 1)
		{
			mainCam.GetComponent<CameraFollow> ().ResetView(camPOS-1);
			gameObject.GetComponent<Slider>().value = camPOS-1;
		}
		if(Input.GetAxis("Mouse ScrollWheel") >= 0.1f && camPOS < 6)
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
