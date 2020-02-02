using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleFadeOut : MonoBehaviour
{
    private RectTransform rectTrans;
    public Sprite fullBlackIMG;
    public float speedIn;
    // Start is called before the first frame update
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
    }


    internal void exit()
    {
        StartCoroutine(ExitAnim());
    }
    IEnumerator ExitAnim()
    {
        LevelSelectorManager.instance.activePlayer(false);

        yield return new WaitForSeconds(0.25f);

        while (rectTrans.localScale.x > 1.5f)
        {
            rectTrans.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * speedIn;
            if(rectTrans.localScale.x < 1)
                rectTrans.localScale = new Vector3(1, 1, 1);

            yield return new WaitForEndOfFrame();
        }

        GetComponent<Image>().sprite = fullBlackIMG;
        rectTrans.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(1f);

        LevelSelectorManager.instance.changeScene();
    }
}
