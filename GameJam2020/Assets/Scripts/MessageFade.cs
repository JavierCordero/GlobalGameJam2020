using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageFade : MonoBehaviour
{
    public float speed = 5f;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FadeIn()
    {
        StartCoroutine("fadeIn");
    }

    public void FadeOut()
    {
        StartCoroutine("fadeOut");
    }

    IEnumerator fadeIn()
    {
        Color c;

        while (spriteRenderer.color.a < 1)
        {
            c = spriteRenderer.color;
            c.a += Time.deltaTime * speed;
            spriteRenderer.color = c;
            yield return new WaitForEndOfFrame();
        }

        c = spriteRenderer.color;
        c.a = 1;
        spriteRenderer.color = c;
    }

    IEnumerator fadeOut()
    {
        Color c;

        while (spriteRenderer.color.a > 0)
        {
            c = spriteRenderer.color;
            c.a -= Time.deltaTime * speed;
            spriteRenderer.color = c;
            yield return new WaitForEndOfFrame();
        }

        c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
    }
}
