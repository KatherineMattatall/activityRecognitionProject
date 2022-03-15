using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activities : MonoBehaviour
{
    //public Animator anim;
    public string chosenAnim;
    string[] possibleActivities = new string[]{"tripping","runningFromBuilding","hitByCar","seizing","stealingCar","arguing"};

    //void Awake()
    //{
       // anim = GetComponent<Animator>();
    //}

    public string chooseActivity()
    {
        //AnimatorControllerParameter[] parameters = anim.parameters;
        //Debug.Log(anim.parameters);
        //int paramIndex = Random.Range(0,parameters.Length);
        int index = Random.Range(0,possibleActivities.Length);
        chosenAnim = possibleActivities[index];
        //Debug.Log(paramIndex);
        //Debug.Log("hello");
        //anim.SetBool(parameters[paramIndex].name, true);
        //Debug.Log(parameters[paramIndex].name);
        //chosenAnim = parameters[paramIndex].name;

        return chosenAnim;
        
    }
}

