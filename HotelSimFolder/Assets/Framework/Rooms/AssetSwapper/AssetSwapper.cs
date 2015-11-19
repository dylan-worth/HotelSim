using UnityEngine;
using System.Collections.Generic;
public class AssetSwapper : MonoBehaviour {

   

    [SerializeField]
    [Tooltip("Repair billboard")]
    GameObject repairBillboard;

    [SerializeField]
    [Tooltip("Upgrade billboard")]
    GameObject upgradeBillboard;

    List<GameObject> activeBillboards = new List<GameObject>();//current billboard being displayed.
    List<int> activeDurations = new List<int>();//parallel list of billboard durations.

    public void SwapRoomLayout(GameObject room, string oldLayout ,  string newLayout)
    {
        room.transform.FindChild(oldLayout).gameObject.SetActive(false);
        room.transform.FindChild(newLayout).gameObject.SetActive(true);
        room.transform.FindChild(newLayout).FindChild("Clean").gameObject.SetActive(true);
        room.transform.FindChild(newLayout).FindChild("Occupied").gameObject.SetActive(false);
        room.transform.FindChild(newLayout).FindChild("Dirty").gameObject.SetActive(false);
    }

    public void SwapRoomStatus(GameObject room, string currentLayout, string currentStatus)
    {
        switch (currentStatus)
        {
            case "Clean":
                room.transform.FindChild(currentLayout).FindChild("Clean").gameObject.SetActive(true);
                room.transform.FindChild(currentLayout).FindChild("Occupied").gameObject.SetActive(false);
                room.transform.FindChild(currentLayout).FindChild("Dirty").gameObject.SetActive(false);
                break;
            case "Occupied":
                room.transform.FindChild(currentLayout).FindChild("Clean").gameObject.SetActive(false);
                room.transform.FindChild(currentLayout).FindChild("Occupied").gameObject.SetActive(true);
                room.transform.FindChild(currentLayout).FindChild("Dirty").gameObject.SetActive(false);
                break;
            case "Dirty":
                room.transform.FindChild(currentLayout).FindChild("Clean").gameObject.SetActive(false);
                room.transform.FindChild(currentLayout).FindChild("Occupied").gameObject.SetActive(false);
                room.transform.FindChild(currentLayout).FindChild("Dirty").gameObject.SetActive(true);
                break;
        }
        
    }

    public void AddBillboard(Transform trans, int type, int duration)//types: 0 = repair, 1 = upgrade
    {

        switch (type)
        {
            case 0:
                GameObject repairBill = Instantiate(repairBillboard, trans.position, Quaternion.identity) as GameObject;
                activeBillboards.Add(repairBill);
                activeDurations.Add(duration);
                break;
            case 1:
                GameObject upgradeBill = Instantiate(repairBillboard, trans.position, Quaternion.identity) as GameObject;
                activeBillboards.Add(upgradeBill);
                activeDurations.Add(duration);
                break;
        }
       
    }

    public void TickDown()
    {
        
        for (int i = 0; i < activeDurations.Count; i++)
        {
            activeDurations[i]--;
            if (activeDurations[i] <= 0)
            {
                Destroy(activeBillboards[i]);
                activeBillboards.RemoveAt(i);
                activeDurations.RemoveAt(i);
            }
        }
    }
}
