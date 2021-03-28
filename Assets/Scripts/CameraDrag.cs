using UnityEngine;

public class CameraDrag : MonoBehaviour {
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    //private float distance = 0.0f;

    void Update() {
        yaw += speedH * Input.GetAxis("Mouse X") * Time.deltaTime * 100;
        pitch -= speedV * Input.GetAxis("Mouse Y") * Time.deltaTime * 100;
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}

   