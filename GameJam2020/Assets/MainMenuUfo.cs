using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUfo : MonoBehaviour
{
    public LevelLoader levelLoader;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(MenuAnimation());
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ExitAnimation());
        }
    }
    IEnumerator MenuAnimation()
    {

        animator.SetTrigger("Leave");
        yield return new WaitForSeconds(2f);
        levelLoader.LoadNextLevel();
    }

    IEnumerator ExitAnimation()
    {
        animator.SetTrigger("Exit");
        yield return new WaitForSeconds(2f);
        Debug.Log("HOLA");
    }
}
