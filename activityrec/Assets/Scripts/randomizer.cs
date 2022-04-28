using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class randomizer : MonoBehaviour
{
    public Light sunLight;
    public Camera camera1;
    public Camera camera2;

    void Start() {
        //enable random camera
        int chosenCam = Random.Range(1,3);
        if (chosenCam == 1) {
            camera1.enabled = true;
            camera2.enabled = false;
        }
        else {
            camera1.enabled = false;
            camera2.enabled = true;

        }
    }
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
