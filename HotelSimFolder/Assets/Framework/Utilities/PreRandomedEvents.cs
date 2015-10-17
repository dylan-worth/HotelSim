using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreRandomedEvents : MonoBehaviour {

    [System.NonSerialized]
    public List<GeneratedEvent> listOfEvents = new List<GeneratedEvent>();
    [SerializeField]
    GeneratedEvent eventOne;
    [SerializeField]
    GeneratedEvent eventTwo;
    [SerializeField]
    GeneratedEvent eventThree;
    [SerializeField]
    GeneratedEvent eventFour;
    [SerializeField]
    GeneratedEvent eventFive;
    [SerializeField]
    GeneratedEvent eventSix;
    [SerializeField]
    GeneratedEvent eventSeven;
    [SerializeField]
    GeneratedEvent eventEight;
    [SerializeField]
    GeneratedEvent eventNine;
    [SerializeField]
    GeneratedEvent eventTen;

    public int[] eventDates = new int[10];
    int[] numberOfdaysPerMonths = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    void Awake() 
    {
        listOfEvents.Add(eventOne); listOfEvents.Add(eventTwo); listOfEvents.Add(eventThree);
        listOfEvents.Add(eventFour); listOfEvents.Add(eventFive); listOfEvents.Add(eventSix);
        listOfEvents.Add(eventSeven); listOfEvents.Add(eventEight); listOfEvents.Add(eventNine);
        listOfEvents.Add(eventTen);
    }

    void Start() 
    {
        RandomizeEvent();
    }

    void RandomizeEvent() 
    {
        for(int i = 0; i < listOfEvents.Count; i++)
        {
            if (listOfEvents[i].probableMonths.Length >= 12) //if lenght is 12 event can happen anytime of the year.
            {
                //try and random a date that sisnt already used.
                int randomDate;
                bool same;
                do
                {
                    randomDate = Random.Range(1, 366);
                    same = false;
                    for (int j = 0; j < eventDates.Length; j++)
                    {
                        if (randomDate == eventDates[j])
                        {
                            same = true;
                            break;
                        }
                    }
                } while (same);
                eventDates[i] = randomDate;
            }
            else if (listOfEvents[i].probableMonths.Length != 0)
            {
                
                int randomDate;
                int randomMonth;
                int dayOfTheYear;
                bool same;
                do
                {
                    dayOfTheYear = 0;
                    randomMonth = Random.Range(0, listOfEvents[i].probableMonths.Length);
                    for (int l = 0; l < randomMonth; l++)
                    {
                        dayOfTheYear += numberOfdaysPerMonths[l];
                    }
                    randomDate = Random.Range(dayOfTheYear, dayOfTheYear + numberOfdaysPerMonths[randomMonth]);
                    same = false;
                    for (int j = 0; j < eventDates.Length; j++)
                    {
                        if (randomDate == eventDates[j])
                        {
                            same = true;
                            break;
                        }
                    }
                } while (same);
                eventDates[i] = randomDate;
            }
            //if event has 0 for size it will not be added to the calendar.
        }
        for (int h = 0; h < eventDates.Length; h++)
        { Debug.Log(eventDates[h]); }
           
    }

    public GeneratedEvent GetEvent(int date) 
    {
        for (int i = 0; i < eventDates.Length; i++)
        {
            if (eventDates[i] == date)
            {
                return listOfEvents[i];
            }
        }
        return null;
    }
}
