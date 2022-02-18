using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class randomizer : MonoBehaviour
{
    //public GameObject light;
    //Light light = GetComponent<Light>();
    public Light sunLight;

    public GameObject LeonardChar;
    public GameObject ElizabethChar;
    public GameObject LewisChar;
    public GameObject SophieChar;

    // Start is called before the first frame update
    void Start()
    {
        //random light rotation and intensity
        sunLight.transform.rotation = Random.rotation;
        sunLight.intensity = Random.Range(0.0f,1.0f);

        //instatiate a random character
        int chosenChar = Random.Range(1,5);
        if (chosenChar == 1) {
            Selection.activeObject = PrefabUtility.InstantiatePrefab(LeonardChar);
            var tempPrefab = Selection.activeGameObject;
            tempPrefab.transform.position = Vector3.zero;
            tempPrefab.transform.rotation = Quaternion.identity;
        }
        if (chosenChar == 2) {
            //Instantiate(ElizabethChar, new Vector3(0,0,0), Quaternion.identity);
            Selection.activeObject = PrefabUtility.InstantiatePrefab(ElizabethChar);
            var tempPrefab = Selection.activeGameObject;
            tempPrefab.transform.position = Vector3.zero;
            tempPrefab.transform.rotation = Quaternion.identity;
        }
        if (chosenChar == 3) {
            //Instantiate(LewisChar, new Vector3(0,0,0), Quaternion.identity);
            Selection.activeObject = PrefabUtility.InstantiatePrefab(LewisChar);
            var tempPrefab = Selection.activeGameObject;
            tempPrefab.transform.position = Vector3.zero;
            tempPrefab.transform.rotation = Quaternion.identity;
        }
        if (chosenChar == 4) {
            //Instantiate(SophieChar, new Vector3(0,0,0), Quaternion.identity);
            Selection.activeObject = PrefabUtility.InstantiatePrefab(SophieChar);
            var tempPrefab = Selection.activeGameObject;
            tempPrefab.transform.position = Vector3.zero;
            tempPrefab.transform.rotation = Quaternion.identity;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
