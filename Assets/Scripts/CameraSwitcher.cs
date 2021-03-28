using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] Camera[] cameras;
    [SerializeField] string cameraSwitchKey = "c";
    [SerializeField] int defaultCameraIndex = 0;

    private int currentCamera;

    void Start() {
        currentCamera = defaultCameraIndex;
        for (int c1 = 0; c1 < cameras.Length; c1++) {
            if(c1 == currentCamera) {
                cameras[c1].enabled = true;
                cameras[c1].gameObject.GetComponent<AudioListener>().enabled = true;
            } else {
                cameras[c1].enabled = false;
                cameras[c1].gameObject.GetComponent<AudioListener>().enabled = false;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyUp(cameraSwitchKey)) {
            currentCamera++;
            if (currentCamera >= cameras.Length) {
                currentCamera = 0;
            }
            for (int c1 = 0; c1 < cameras.Length; c1++) {
                if (c1 == currentCamera) {
                    cameras[c1].enabled = true;
                    cameras[c1].gameObject.GetComponent<AudioListener>().enabled = true;
                } else {
                    cameras[c1].enabled = false;
                    cameras[c1].gameObject.GetComponent<AudioListener>().enabled = false;
                }
            }
        }
    }
}
