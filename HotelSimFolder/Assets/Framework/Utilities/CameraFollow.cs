using UnityEngine;
using System.Collections;


public class CameraFollow : MonoBehaviour{

	
	public float dampTime = 0.15f;
	public Transform target;

	public Transform lvl1;
	public Transform lvl2;
	public Transform lvl3;
	public Transform lvl4;
	public Transform lvl5;

	public Transform rotationPoint;

	public float smoothing = 2f;        
	public float rotationSpeed = 10f;

	public bool rLeft = false;
	public bool rRight = false;
	bool isResetting = false;

	float startTime;
	float journeyLength;
	Quaternion orinigalRotation;

	//int camPOS = 6;

	void Start()
	{
		orinigalRotation = transform.rotation;
		target = lvl5;
	}

	void Update ()
	{
		if(isResetting)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, orinigalRotation, Time.deltaTime * 2f);

			float distCovered = (Time.time - startTime) * 7f;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp (transform.position, target.position, fracJourney);
		}
		else
		{
				transform.position = Vector3.Lerp (transform.position, new Vector3(transform.position.x,target.position.y,transform.position.z)
                                                           , smoothing * Time.deltaTime);
		}
		if(rLeft || Input.GetAxis("Horizontal") < -0.5f)
		{
			RotateAroundCenter(1f);
		}
		else if(rRight || Input.GetAxis("Horizontal") > 0.5f)
		{
			RotateAroundCenter(-1f);
		}
	}
	public void CenterView()
	{
		target = lvl5;
		startTime = Time.time;
		journeyLength = Vector3.Distance(transform.position, target.position);
		isResetting = true;
		StartCoroutine(ViewReset());
	}
	IEnumerator ViewReset()
	{
		yield return new WaitForSeconds(4f);
		isResetting = false;
		Debug.Log("stop");
	}

	public void RotateAroundCenter(float direction)
	{
		transform.RotateAround(rotationPoint.position, Vector3.up, direction * rotationSpeed* Time.deltaTime);
	}

	public void ResetView(int pos)
	{
		//camPOS = pos;
		switch(pos)
		{
		case 1:
			target = lvl1;
			break;
			
		case 2:
			target = lvl2;
			break;
			
		case 3:
			target = lvl3;
			break;
			
		case 4:
			target = lvl4;
			break;
				
		case 5:
			target = lvl5;
			break;

		case 6:
			target = lvl5;
			break;
		}
	}
}
