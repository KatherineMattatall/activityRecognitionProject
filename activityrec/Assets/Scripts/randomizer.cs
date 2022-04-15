using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class randomizer : MonoBehaviour
{
    public Light sunLight;

    public int randomize()
    {
        //random light rotation and intensity
        sunLight.transform.rotation = Random.rotation;
        sunLight.intensity = Random.Range(0.0f,1.0f);

        //choose a random character
        int chosenChar = Random.Range(1,5);
        return chosenChar;
        
    }

}
