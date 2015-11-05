using UnityEngine;
using System.Collections;


public class CameraFollow : MonoBehaviour{

	
	public float dampTime = 0.15f;


	Vector3 target;
	[SerializeField][Tooltip("Coordinates of the centered view point.")]
	Vector3 centeredView = new Vector3(12f,10f,12.273f);

	public Transform rotationPoint;

	public float smoothing = 2f;        
	public float rotationSpeed = 10f;

	public bool rLeft = false;
	public bool rRight = false;

	float currentHeight = 10f;

	bool isResetting = false;

	float startTime;
	float journeyLength;
	Quaternion orinigalRotation;


	void Start()
	{
		orinigalRotation = transform.rotation;
		target = centeredView;
	}

	void Update ()
	{
		if(isResetting)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, orinigalRotation, Time.deltaTime * 2f);

			float distCovered = (Time.time - startTime) * 7f;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp (transform.position, target, fracJourney);
		}
		else
		{
				transform.position = Vector3.Lerp (transform.position, new Vector3(transform.position.x,currentHeight,transform.position.z)
                                                           , smoothing * Time.deltaTime);
		}
		if(rLeft || Input.GetAxis("Horizontal") < -0.5f)
		{
			if(!isResetting)
			RotateAroundCenter(1f);
		}
		else if(rRight || Input.GetAxis("Horizontal") > 0.5f)
		{
			if(!isResetting)
			RotateAroundCenter(-1f);
		}
	}
	public void CenterView()
	{
		target =  centeredView;
		startTime = Time.time;
		journeyLength = Vector3.Distance(transform.position, target);
		isResetting = true;
		StartCoroutine(ViewReset());
	}
	IEnumerator ViewReset()
	{
		yield return new WaitForSeconds(4f);
		isResetting = false;
	}

	public void RotateAroundCenter(float direction)
	{
		transform.RotateAround(rotationPoint.position, Vector3.up, direction * rotationSpeed* Time.deltaTime);
	}

	public void ResetView(int pos)
	{
		currentHeight = 5f+pos;
	
	}

}
