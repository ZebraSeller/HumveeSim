using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowWiperController : MonoBehaviour
{
    [SerializeField] float rotationRange = 115f;
    [SerializeField] float rotationPeriod = 0.8f;
    private void Start() {
        ForwardRotation();
    }

    private void ForwardRotation() {
        LeanTween.rotateLocal(gameObject, new Vector3(0, rotationRange, 0), rotationPeriod);
        Invoke("BackwardRotation", rotationPeriod);
    }

    private void BackwardRotation() {
        LeanTween.rotateLocal(gameObject, new Vector3(0, 0, 0), rotationPeriod);
        Invoke("ForwardRotation", rotationPeriod);
    }
}
