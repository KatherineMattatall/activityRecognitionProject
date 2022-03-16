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
    float randomZ;

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
            if (leftOrRight == 1) {
                randomZ = Random.Range(11,-0.55f);
            }
            else{
                randomZ = Random.Range(-13,-21);
            }
            tempPrefab.transform.position = new Vector3(-0.85f,0.125f,randomZ);
            int rotateLeftOrRight = Random.Range(1,3);
            if (rotateLeftOrRight == 1) {
                //face left
                tempPrefab.transform.rotation = Quaternion.identity;
            }
            else{
                //face right
                tempPrefab.transform.rotation = new Quaternion(0,180,0,0);
            }
            if (activity == "arguing"){
                if (rotateLeftOrRight == 1){
                    //pedestrian needs to face right and be further left if character is facing left
                    Instantiate(ArguingChar, new Vector3(-0.85f,0.125f,randomZ+1), new Quaternion(0,180,0,0));
                }
                else{
                    Instantiate(ArguingChar, new Vector3(-0.85f,0.125f,randomZ-1), Quaternion.identity);
                }

            }
        anim = tempPrefab.GetComponent<Animator>();
        anim.SetBool(activity, true);
        }
        if (activity == "runningFromBuilding") {
            var tempPrefab = Selection.activeGameObject;
            tempPrefab.transform.position = new Vector3(13,0.125f,8.5f);
            tempPrefab.transform.Rotate(0,270,0);
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
            //tempPrefab.transform.rotation = new Quaternion(0,270,0,0);
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
        //Record();
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
            OutputWidth = 640,
            OutputHeight = 480
        };
        
        videoRecorder.AudioInputSettings.PreserveAudio = true;
        videoRecorder.OutputFile = "C:\\Users\\matta\\Downloads\\activityVideos\\"+activity+"\\"+Random.Range(0,100000);
        
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

       // StartCoroutine(waiter());
        
    }

}
