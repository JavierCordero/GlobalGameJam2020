﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCircleInOut : MonoBehaviour
{
    public float speedIn = 2.5f;
    public float speedOut = 2.5f;
    public float scaleLimit = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("circleOut");
    }

    public void sceneOut()
    {
        StartCoroutine("circleIn");
    }    

    IEnumerator circleOut()
    {
        yield return new WaitForSeconds(1f);

        while (transform.localScale.x < scaleLimit)
        {
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * speedOut;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();        
    }

    IEnumerator circleIn()
    {
        yield return new WaitForSeconds(0.5f);

        while (transform.localScale.x > 0)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * speedIn;
            yield return new WaitForEndOfFrame();
        }

        transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(0.5f);

        //LevelManager.Instance.changeScene();
    }
}
