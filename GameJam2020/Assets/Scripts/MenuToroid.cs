using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToroid : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerTransform;

    public Vector3 offset;

    private void OnTriggerEnter(Collider other)
    {
        cameraTransform.transform.position += offset;
        playerTransform.transform.position += offset;
    }
}
