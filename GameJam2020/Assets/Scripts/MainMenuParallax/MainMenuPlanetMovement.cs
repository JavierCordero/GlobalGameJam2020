using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlanetMovement : MonoBehaviour
{
    public float velocity;
    void Update()
    {
        transform.Translate(Vector3.right * velocity * Time.deltaTime);
    }
}
