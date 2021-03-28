using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZControl : MonoBehaviour
{
    public float speedD = 5f;
    [SerializeField] float distance = -8f;

    void Update()
    {
        distance += speedD * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 100;
        transform.localPosition = new Vector3(0, 0, distance);
    }
}
