using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;

// CODE FOUND HERE https://forum.unity.com/threads/control-unity-recorder-from-script.840946/#post-5559976
public class recordVideos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
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
        videoRecorder.OutputFile = "C:\\Users\\kathe\\Downloads\\videoClipTests"; // Change this to change the output file name (no extension)
        
        controllerSettings.AddRecorderSettings(videoRecorder);
        controllerSettings.SetRecordModeToFrameInterval(0, 500); // 2s @ 30 FPS
        //controllerSettings.FrameRate = 30;
        
        RecorderOptions.VerboseMode = false;
        TestRecorderController.PrepareRecording();
        TestRecorderController.StartRecording();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
