using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectorManager : MonoBehaviour
{
    public static LevelSelectorManager instance;

    int maxUnlockedIndex;

    public SmoothCamera camera;
    public Transform player;

    public Vector3 offset;

    // Para la colocación del player tras terminar el nivel
    public LevelSelector[] originalPositions;

    // Para la actualizacion del resto
    public Transform[] restPositions;


    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        maxUnlockedIndex = PlayerPrefs.GetInt("maxUnlockedIndex");

        player.transform.position = originalPositions[maxUnlockedIndex].transform.position + offset;

        if (PlayerPrefs.GetInt("lastLevelDone") == 1)
        {
            // Animacion
        }
        else
        {
            for (int i = 0; i<maxUnlockedIndex; i++)            
                originalPositions[i].setState(levelState.done);
            
            for (int i = 0; i < maxUnlockedIndex + 1; i++)            
                originalPositions[i].setState(levelState.available);

            for (int i = maxUnlockedIndex + 1; i < originalPositions.Length; i++)
                originalPositions[i].setState(levelState.blocked);
        }

    }

    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
