using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenuUI : MonoBehaviour {

    Dictionary<int, string> loggingInfo = new Dictionary<int,string>();
	
    void Start()
    {
        loggingInfo.Add(100975584, "password");
    }

	public void LoadMainLevel()
	{
		Application.LoadLevel ("Hotel");
        Debug.Log("click");
	}

    public void StudentLogging() 
    {
        GameObject logPan = GameObject.FindGameObjectWithTag("UI").transform.FindChild("MainUI").transform.FindChild("LoggingPanel").gameObject;
        int studentID =  int.Parse( logPan.transform.FindChild("Input_ID").gameObject.GetComponent<InputField>().text);
        string studentPass = logPan.transform.FindChild("Input_Password").gameObject.GetComponent<InputField>().text;

        if (loggingInfo.ContainsKey(studentID))
        {
            if (studentPass == loggingInfo[studentID]) 
            {
                LoadMainLevel();
            }
        }
        else 
        {
            logPan.transform.FindChild("txt_Errors").GetComponent<Text>().text = "Student ID or password incorrect.";
        }

      
    }
}


