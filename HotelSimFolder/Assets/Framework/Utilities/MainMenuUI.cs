using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

	bool moveLogo = true;
	bool moveInstitute = true;
	bool moveNewGame = true;

	public Transform logoEnd;
	public Transform instituteEnd;
	public Transform newGameEnd;

	public GameObject logo;
	public GameObject institute;
	public GameObject newGame;

	Transform startLogo;
	Transform startInstitute;
	Transform startButton;

	float startTime;
	float journeyLength = 3.66f;

	void Start()
	{
		startTime = Time.time;
		startLogo = logo.transform;
		startButton = newGame.transform;
		startInstitute = institute.transform;
		Invoke ("StopMove", 3.66f);
	}

	void Update()
	{
		if (moveLogo)
			MoveTheLogo ();
		if (moveInstitute)
			MoveTheInstitute ();
		if (moveNewGame)
			MoveTheNewGame ();
	}
	void MoveTheLogo()
	{
		float fracJourney = (Time.time - startTime) / journeyLength;
		logo.transform.position = Vector3.Lerp(startLogo.position, logoEnd.position, fracJourney);
	}
	void MoveTheInstitute()
	{
		float fracJourney = (Time.time - startTime) / journeyLength;
		institute.transform.position = Vector3.Lerp(startInstitute.position, instituteEnd.position, fracJourney);
	}
	void MoveTheNewGame()
	{
		float fracJourney = (Time.time - startTime) / journeyLength;
		newGame.transform.position = Vector3.Lerp(startButton.position, newGameEnd.position, fracJourney);
	}
	void StopMove()
	{
		moveLogo = false;
		moveInstitute = false;
		moveNewGame = false;
	}
	public void LoadMainLevel()
	{
		Application.LoadLevel ("TestScene");
	}
}


