using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Every room will have this basic functionality
public class RoomBehaviour : MonoBehaviour 
{
	//Variables (Setup)
	[SerializeField] protected GameObject roomMenu;	//Which object on the canvas does this room trigger

	void OnMouseUp()
	{

		//Ignore if mouse is currently over a UI element:
		if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			return;

		Vector3 mouseLocation = (Input.mousePosition);
		mouseLocation.y += 120f;
		if (roomMenu != null) {
			roomMenu.SetActive (true);

		}

		//this.SendMessage("HandleMenu");
		this.gameObject.GetComponent<RoomStats> ().HandleMenu (mouseLocation);
	}
}
