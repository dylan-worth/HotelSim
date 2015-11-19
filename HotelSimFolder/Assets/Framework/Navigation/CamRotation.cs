using UnityEngine;
using UnityEngine.EventSystems;

public class CamRotation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public GameObject cam;

	public bool isRight = true;

	public void OnPointerDown(PointerEventData eventData)
	{
		if(isRight){
		cam.GetComponent<CameraFollow>().rRight = true;
		}
		else if(!isRight){
		cam.GetComponent<CameraFollow>().rLeft = true;
		}
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		if(isRight){
			cam.GetComponent<CameraFollow>().rRight = false;
		}
		else if(!isRight){
			cam.GetComponent<CameraFollow>().rLeft = false;
		}
	}

}
