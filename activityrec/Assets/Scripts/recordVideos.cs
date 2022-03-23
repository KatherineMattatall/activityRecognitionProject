using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;

public class recordVideos : MonoBehaviour
{
    //public GameObject activities;
    //public GameObject randomizer;
    Animator anim;
    public GameObject LeonardChar;
    public GameObject ElizabethChar;
    public GameObject LewisChar;
    public GameObject SophieChar;
    public GameObject ArguingChar;
    public string activity;
    public GameObject StreetRoad;
    float zVal;
    //float randomZ;

    // Start is called before the first frame update
    void Start()
    {
        placeCharacter();
    }

    void placeCharacter()
    {
        //call randomize in randomizer script to access the chosen character
        int myChar = StreetRoad.GetComponent<randomizer>().randomize();
        activity = StreetRoad.GetComponent<activities>().chooseActivity();
        Debug.Log(myChar);

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
            float randomX = Random.Range(94,83.7f);
            if (leftOrRight == 1) {
                // left side of street
                zVal = 93.7f;
            }
            else{
                //randomZ = Random.Range(-13,-21);
                zVal = 106.3f;
            }
            //tempPrefab.transform.position = new Vector3(-0.85f,0.125f,randomZ);
            tempPrefab.transform.position = new Vector3(randomX,0.225f,zVal);
            //int rotateLeftOrRight = Random.Range(1,3);
            int faceFrontOrBack = Random.Range(1,3);
            if (faceFrontOrBack == 1) {
                //face front
                tempPrefab.transform.Rotate(0,270,0);
            }
            else{
                //face back
                tempPrefab.transform.Rotate(0,90,0);
            }
            if (activity == "arguing"){
                if (faceFrontOrBack == 1){
                    //pedestrian needs to face back and be further ahead if character is facing frontwards
                    Instantiate(ArguingChar, new Vector3(randomX-1,0.225f,zVal), Quaternion.Euler(0,90,0));
                }
                else{;
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
                // left side of street
                zVal = 81.5f;
                tempPrefab.transform.rotation = Quaternion.identity;
            }
            else{
                zVal = 118.5f;
                tempPrefab.transform.Rotate(0,180,0);
            }
            tempPrefab.transform.position = new Vector3(randomX,0.225f,zVal);
        anim = tempPrefab.GetComponent<Animator>();
        anim.SetBool(activity, true);
        }
        if (activity == "hitByCar" || activity == "jaywalking") {
            var tempPrefab = Selection.activeGameObject;
            int leftOrRight = Random.Range(1,3);
            if (leftOrRight == 1) {
                tempPrefab.transform.position = new Vector3(-0.85f,0.125f,Random.Range(11,-0.55f));
            }
            else{
                tempPrefab.transform.position = new Vector3(-0.85f,0.125f,Random.Range(-13,-21));
            }
            tempPrefab.transform.Rotate(0,270,0);
        anim = tempPrefab.GetComponent<Animator>();
        anim.SetBool(activity, true);
        }
        if (activity == "stealingCar"){
            var tempPrefab = Selection.activeGameObject;
            tempPrefab.transform.position = new Vector3(1.4f,0.125f,-5.61f);
            tempPrefab.transform.rotation = Quaternion.identity;
        anim = tempPrefab.GetComponent<Animator>();
        anim.SetBool(activity, true);
        }
        StartCoroutine(waiter());
    }

    // CODE FOR VIDEO RECORDER FOUND HERE https://forum.unity.com/threads/control-unity-recorder-from-script.840946/#post-5559976
    //void Record()
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
            OutputWidth = 320,
            OutputHeight = 320
        };
        
        videoRecorder.AudioInputSettings.PreserveAudio = true;
        videoRecorder.OutputFile = "C:\\Users\\matta\\Downloads\\activityVideoTester\\"+activity+"\\"+Random.Range(0,100000);
        
        controllerSettings.AddRecorderSettings(videoRecorder);
        //controllerSettings.SetRecordModeToFrameInterval(0, 300);
        //controllerSettings.FrameRate = 30;
        
        RecorderOptions.VerboseMode = false;
        TestRecorderController.PrepareRecording();
        TestRecorderController.StartRecording();
        yield return new WaitForSeconds(10);
        TestRecorderController.StopRecording();
        controllerSettings.RemoveRecorder(videoRecorder);
        FindObjectOfType<GameManager>().Restart();

        
    }

}
