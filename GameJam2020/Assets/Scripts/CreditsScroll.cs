using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    public float scrollDelay = 2f;    
    public float scrollTime = 5f;

    public float scrollSpeed = 5f;


    public GameObject[] credits;
    public GameObject world;

    bool scrolling = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(startScroll), scrollDelay);
        Invoke(nameof(exitApp), scrollTime + scrollDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (scrolling)
        {
            float dst = scrollSpeed * Time.deltaTime;

            for (int i = 0; i<credits.Length; i++)
            {
                credits[i].transform.position += new Vector3(0, dst, 0);
            }
            world.transform.position += new Vector3(0, dst/10.0f, 0);
        }
    }

    void startScroll()
    {
        scrolling = true;
    }

    void exitApp()
    {
        scrolling = false;
        // Transicion
        Application.Quit();
    }

}
