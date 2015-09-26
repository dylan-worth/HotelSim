using UnityEngine;
using System.Collections;

public class DestroyedByTime : MonoBehaviour {

	public float timer = 2f;

	// Use this for initialization
	void Start () {
		StartCoroutine(RemoveFromTheGame());
	}
	IEnumerator RemoveFromTheGame()
	{
		yield return new WaitForSeconds(timer);
		Destroy(gameObject);
	}

}
