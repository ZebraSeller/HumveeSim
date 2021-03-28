using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerCameraDrag : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float upperPitchLimit = 75;
    public float lowerPitchLimit = -10;
    [SerializeField] Camera gunnerCamera;
    [SerializeField] AudioClip gunShotSound;
    [SerializeField] float gunShotPeriod = 0.125f;
    [SerializeField] AudioClip[] casingSounds = new AudioClip[4];

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private bool gunEnabled = false;
    void Update()
    {
        
        yaw += speedH * Input.GetAxis("Mouse X") * Time.deltaTime * 100;
        transform.localEulerAngles = new Vector3(-90, yaw, 0.0f);
        if (gunnerCamera.enabled == false) {
            return;
        }
        GunControl();
        pitch -= speedV * Input.GetAxis("Mouse Y") * Time.deltaTime * 100;
        if (pitch < -upperPitchLimit) {
            pitch = -upperPitchLimit;
        } else if (pitch > -lowerPitchLimit) {
            pitch = -lowerPitchLimit;
        }
            
            gameObject.transform.Find("M2 Browning").transform.localEulerAngles = new Vector3(pitch, 0, 0); 
    }

    private void GunControl() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            GameObject.Find("Browning Bullets").GetComponent<ParticleSystem>().Play();
            GameObject.Find("Casing Ejector Particle System").GetComponent<ParticleSystem>().Play();
            gunEnabled = true;
            GunSound();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            GameObject.Find("Browning Bullets").GetComponent<ParticleSystem>().Stop();
            GameObject.Find("Casing Ejector Particle System").GetComponent<ParticleSystem>().Stop();
            gunEnabled = false;
        }

    }
    private void GunSound() {
        gameObject.transform.Find("M2 Browning").GetComponent<AudioSource>().PlayOneShot(gunShotSound);
        int num = Random.Range(0, 3);
        GameObject.Find("Casing Ejector Particle System").GetComponent<AudioSource>().PlayOneShot(casingSounds[num]);
        if (!gunEnabled) {
            return;
        }
        Invoke("GunSound", gunShotPeriod);
    }
}
