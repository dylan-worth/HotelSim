using UnityEngine;

public class ReceptionMenu : MonoBehaviour 
{

	//Tries to book a room
	public void BookRoom()
	{
		Reception.RunSimulation(4);
	}
}
