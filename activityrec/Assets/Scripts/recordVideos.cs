using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;

public class recordVideos : MonoBehaviour
{
    Animator anim;
    public GameObject LeonardChar;
    public GameObject ElizabethChar;
    public GameObject LewisChar;
    public GameObject SophieChar;
    public GameObject ArguingChar;
    public string activity;
    public GameObject StreetRoad;
    float zVal;

    // Start is called before the first frame update
    void Start()
    {
        placeCharacter();
    }

    void placeCharacter()
    {
        //call randomize in randomizer script to access the chosen character number
        int myChar = StreetRoad.GetComponent<randomizer>().randomize();
        //call chooseActivity in activities script to access the chosen activity
        activity = StreetRoad.GetComponent<activities>().chooseActivity();

        if (myChar == 1) {
            Selection.activeObject = PrefabUtility.InstantiatePrefab(LeonardChar);
        }
        if (myChar == 2) {
            Selection.activeObject = PrefabUtility.InstantiatePrefab(ElizabethChar);
        }
        if (myChar == 3) {
            Selection.activeObject = PrefabUtility.InstantiatePrefab(LewisChar);
        }
        if (myChar == 4) {
            Selection.activeObject = PrefabUtility.InstantiatePrefab(SophieChar);
        }

        if (activity == "tripping" || activity == "seizing" || activity == "arguing") {
            var tempPrefab = Selection.activeGameObject;
            int leftOrRight = Random.Range(1,3);
            float randomX = Random.Range(94,83.7f); //random x location within frame
            if (leftOrRight == 1) {
                //left side of street
                zVal = 93.7f;
            }
            else{
                //right side of street
                zVal = 106.3f;
            }
            tempPrefab.transform.position = new Vector3(randomX,0.225f,zVal); //this is where the character will be instantiated
            int faceFrontOrBack = Random.Range(1,3);
            if (faceFrontOrBack == 1) {
                //face front
                tempPrefab.transform.Rotate(0,270,0);
            }
            else{
                //face back
                tempPrefab.transform.Rotate(0,90,0);
            }
            if (activity == "arguing"){ //need to instatiate a pedestrian for the character to argue with
                if (faceFrontOrBack == 1){
                    //pedestrian needs to face back and be further ahead if character is facing frontwards
                    Instantiate(ArguingChar, new Vector3(randomX-1,0.225f,zVal), Quaternion.Euler(0,90,0));
                }
                else{
                    Instantiate(ArguingChar, new Vector3(randomX+1,0.225f,zVal), Quaternion.Euler(0,270,0));
                }

            }
        anim = tempPrefab.GetComponent<Animator>();
        anim.SetBool(activity, true);
        }

        if (activity == "runningFromBuilding") {
            var tempPrefab = Selection.activeGameObject;
            int leftOrRight = Random.Range(1,3);
            float randomX = Random.Range(94,83.7f);
            if (leftOrRight == 1) {
                //left side of street
                zVal = 81.5f;
                tempPrefab.transform.rotation = Quaternion.identity; //character faces towards right (right)
            }
            else{
                //right side of street
                zVal = 118.5f;
                tempPrefab.transform.Rotate(0,180,0); //character faces towards road (left)
            }
            tempPrefab.transform.position = new Vector3(randomX,0.225f,zVal); //character will be instantiated in this location
        anim = tempPrefab.GetComponent<Animator>();
        anim.SetBool(activity, true);
        }

        //add hitByCar or jaywalking to possibleActivities in activities script in order to this to be chosen
        if (activity == "hitByCar" || activity == "jaywalking") {
            var tempPrefab = Selection.activeGameObject;
            int leftOrRight = Random.Range(1,3);
            if (leftOrRight == 1) {
                tempPrefab.transform.position = new Vector3(-0.85f,0.125f,Random.Range(11,-0.55f)); //start on sidewalk facing road in random x location within frame
            }
            else{
                tempPrefab.transform.position = new Vector3(-0.85f,0.125f,Random.Range(-13,-21));
            }
            tempPrefab.transform.Rotate(0,270,0);
        anim = tempPrefab.GetComponent<Animator>();
        anim.SetBool(activity, true);
        }

        //add stealingCar to activities script in order for this to be chosen
        if (activity == "stealingCar"){
            var tempPrefab = Selection.activeGameObject;
            tempPrefab.transform.position = new Vector3(1.4f,0.125f,-5.61f); //would need to change this location to be close to a stationary vehicle
            tempPrefab.transform.rotation = Quaternion.identity;
        anim = tempPrefab.GetComponent<Animator>();
        anim.SetBool(activity, true);
        }
        StartCoroutine(waiter());
    }

    // MOST OF THE CODE FOR THIS FUNCTION WAS FOUND HERE https://forum.unity.com/threads/control-unity-recorder-from-script.840946/#post-5559976
    IEnumerator waiter()
    {
        var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        var TestRecorderController = new RecorderController(controllerSettings);
        
        var videoRecorder = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        videoRecorder.name = "My Video Recorder";
        videoRecorder.Enabled = true;
        videoRecorder.VideoBitRateMode = VideoBitrateMode.High;
        
        videoRecorder.ImageInputSettings = new GameViewInputSettings
        {
            OutputWidth = 320, //change to desired size
            OutputHeight = 320
        };
        
        videoRecorder.AudioInputSettings.PreserveAudio = true;
        videoRecorder.OutputFile = "C:\\Users\\matta\\Downloads\\activityVideoClips\\"+activity+"\\"+Random.Range(0,100000); //change this to the path to the folder where you want to save the videos
        
        controllerSettings.AddRecorderSettings(videoRecorder);
        
        RecorderOptions.VerboseMode = false;
        TestRecorderController.PrepareRecording();
        TestRecorderController.StartRecording();
        yield return new WaitForSeconds(10);
        TestRecorderController.StopRecording();
        controllerSettings.RemoveRecorder(videoRecorder);
        FindObjectOfType<GameManager>().Restart();

        
    }

}
