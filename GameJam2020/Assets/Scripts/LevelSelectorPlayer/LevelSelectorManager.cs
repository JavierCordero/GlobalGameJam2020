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

        for (int i = 0; i < maxUnlockedIndex; i++)
            originalPositions[i].setState(levelState.done);

        originalPositions[maxUnlockedIndex].setState(levelState.available);

        for (int i = maxUnlockedIndex + 1; i < originalPositions.Length; i++)
            originalPositions[i].setState(levelState.blocked);

        if (PlayerPrefs.GetInt("lastLevelDone") == 1)
        {
            // Animacion
            PlayerPrefs.SetInt("lastLevelDone", 0);
            PlayerPrefs.Save();

            StartCoroutine("unlockNextLevel");
        }
    }

    IEnumerator unlockNextLevel()
    {
        float smooth = camera.getSpeed();
        camera.setSpeed(0.1f);

        yield return new WaitForSeconds(1f);
        
        camera.setTarget(originalPositions[maxUnlockedIndex].transform, false);

        yield return new WaitForSeconds(1f);

        originalPositions[maxUnlockedIndex].setState(levelState.done);

        if (maxUnlockedIndex < originalPositions.Length)
        {
            yield return new WaitForSeconds(1f);
            
            camera.setTarget(originalPositions[maxUnlockedIndex + 1].transform, false);

            yield return new WaitForSeconds(1f);

            originalPositions[maxUnlockedIndex+1].setState(levelState.available);
        }

        yield return new WaitForSeconds(1f);

        camera.setTarget(player, false);

        yield return new WaitForSeconds(1f);

        camera.setTarget(player, true);
        camera.setSpeed(smooth);
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            maxUnlockedIndex += 1;
            PlayerPrefs.SetInt("maxUnlockedIndex",maxUnlockedIndex);
            PlayerPrefs.Save();
            Debug.Log(maxUnlockedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.SetInt("lastLevelDone", 1);
            PlayerPrefs.Save();
        }
    }

    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
