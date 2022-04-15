using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activities : MonoBehaviour
{
    public string chosenAnim;
    string[] possibleActivities = new string[]{"tripping","runningFromBuilding","seizing","arguing"}; //add all activities here

    //chooses a random activity from the list of possible activities and returns it
    public string chooseActivity()
    {
        int index = Random.Range(0,possibleActivities.Length);
        chosenAnim = possibleActivities[index];
        return chosenAnim;
        
    }
}

