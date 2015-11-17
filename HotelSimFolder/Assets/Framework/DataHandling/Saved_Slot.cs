using UnityEngine;
using System.Collections;

public class Saved_Slot : MonoBehaviour {

    public static Saved_Slot singleton;	//create in mainmenu scene. Stays on scene transition.

    [SerializeField]
    string slot_One;
    [SerializeField]
    string slot_Two;
    [SerializeField]
    string slot_Three;

    string currentSlot = null;

    public string GetSlot()
    {
        return currentSlot;
    }
    public void SetCurrent(int i) //sets the string path to saved folder.
    {
        switch (i) 
        {
            case 1: currentSlot = slot_One;
                break;
            case 2: currentSlot = slot_Two;
                break;
            case 3: currentSlot = slot_Three;
                break;
        }
    }
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
   

}
