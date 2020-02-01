using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlayerController : MonoBehaviour
{
    [SerializeField] private float aimSensitivity = 10f;
    [Range(0f, 1f)] [SerializeField] private float buildUpRot;
    [SerializeField] private float minInput;    

    // Aim
    private Vector3 aimTarget;
    private Vector3 forward;
    private Vector3 right;

    private PlayerInputHandler playerInput;    

    // Items
    private Transform playerHand;
    private Item currentItem;

    #region Gets
    public float GetMinInput() { return minInput; }
    public float GetBuildUpRot() { return buildUpRot; }

    private string levelToChange = null;

    public float GetItemWeight()
    {
        if (currentItem == null) return 0;
        else return currentItem.GetWeight();
    }

    #endregion

    void Awake()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        playerInput = GetComponent<PlayerInputHandler>();
        playerHand = transform.Find("Hand");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LevelSelector result))
        {
            levelToChange = result.getLevel();
            Debug.Log(levelToChange);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LevelSelector>() != null)
        {
            levelToChange = null;
            Debug.Log(levelToChange);
        }
    }

    public void chooseLevel()
    {
        if(levelToChange != null)
        {
            Debug.Log("Cambio al nivel: " + levelToChange);
            SceneManager.LoadScene(levelToChange);
        }
        else
        {
            Debug.Log("levelToChange es null");
        }
    }
}
