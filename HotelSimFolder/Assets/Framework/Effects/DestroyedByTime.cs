using UnityEngine;

public class DestroyedByTime : MonoBehaviour {//Add to a gameobject to remove after timer duration.

	public float timer = 2f;//delay before deleting the gamobject.

	// Use this for initialization
	void Start () {
		Destroy(gameObject, timer);
	}


}
