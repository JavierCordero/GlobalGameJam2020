using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectorManager : MonoBehaviour
{
    public static LevelSelectorManager instance;

    public int numNiveles = 6;

    int maxUnlockedIndex;

    public SmoothCamera camera;
    public MenuPlayerMovement menuPlayerMovement;
    public CircleInOut inOut;

    private string sceneToChange = "";

    public Vector3 offset;

    // Para la colocación del player tras terminar el nivel
    public LevelSelector[] originalPositions;

    // Para la actualizacion del resto
    public LevelSelector[] restPositions0;
    public LevelSelector[] restPositions1;
    public LevelSelector[] restPositions2;
    public LevelSelector[] restPositions3;
    public LevelSelector[] restPositions4;
    public LevelSelector[] restPositions5;

    List<LevelSelector[]> restPositions;

    private bool exiting = false;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        restPositions = new List<LevelSelector[]>();
        
        restPositions.Insert(0, restPositions5);
        restPositions.Insert(0, restPositions4);
        restPositions.Insert(0, restPositions3);
        restPositions.Insert(0, restPositions2);
        restPositions.Insert(0, restPositions1);
        restPositions.Insert(0, restPositions0);

        maxUnlockedIndex = PlayerPrefs.GetInt("maxUnlockedIndex");

        if (maxUnlockedIndex >= originalPositions.Length)
            maxUnlockedIndex = originalPositions.Length - 1;

        menuPlayerMovement.transform.position = originalPositions[maxUnlockedIndex].transform.position + offset;

        for (int i = 0; i < maxUnlockedIndex; i++)
        {
            originalPositions[i].setState(levelState.done);

            for (int j = 0; j< restPositions0.Length; j++)
            {
                restPositions[i][j].setState(levelState.done);
            }
        }

        originalPositions[maxUnlockedIndex].setState(levelState.available);
        for (int j = 0; j < restPositions0.Length; j++)
        {
            restPositions[maxUnlockedIndex][j].setState(levelState.available);
        }

        for (int i = maxUnlockedIndex + 1; i < originalPositions.Length; i++)
        {
            originalPositions[i].setState(levelState.blocked);
            for (int j = 0; j < restPositions0.Length; j++)
            {
                restPositions[i][j].setState(levelState.blocked);
            }
        }

        if (PlayerPrefs.GetInt("lastLevelDone") == 1)
        {
            // Animacion
            PlayerPrefs.SetInt("lastLevelDone", 0);
            PlayerPrefs.Save();

            menuPlayerMovement.setActive(false);

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
        originalPositions[maxUnlockedIndex].activeParticles(particles.green);

        if (maxUnlockedIndex < originalPositions.Length)
        {
            yield return new WaitForSeconds(1f);
            
            camera.setTarget(originalPositions[maxUnlockedIndex + 1].transform, false);

            yield return new WaitForSeconds(1f);

            originalPositions[maxUnlockedIndex+1].setState(levelState.available);
            originalPositions[maxUnlockedIndex + 1].activeParticles(particles.red);
        }

        yield return new WaitForSeconds(1f);

        camera.setTarget(menuPlayerMovement.transform, false);

        yield return new WaitForSeconds(0.5f);

        menuPlayerMovement.setActive(true);
        camera.setLooking();
        camera.setSpeed(smooth);        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            maxUnlockedIndex += 1;
            if (maxUnlockedIndex == originalPositions.Length)
                maxUnlockedIndex = originalPositions.Length - 1;
            PlayerPrefs.SetInt("maxUnlockedIndex",maxUnlockedIndex);
            PlayerPrefs.Save();
            Debug.Log(maxUnlockedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.SetInt("lastLevelDone", 1);
            PlayerPrefs.Save();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitToMenu();
        }
    }

    public void loadScene(string name)
    {
        sceneToChange = name;
        inOut.sceneOut();
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneToChange);
    }
    
    public void activePlayer(bool v)
    {
        menuPlayerMovement.setActive(v);
    }

    public void exitToMenu()
    {
        if (!exiting)
        {
            exiting = true;
            sceneToChange = "Menu";
            activePlayer(false);
            menuPlayerMovement.launch();
            camera.setLooking(false);
            camera.setActive(false);
            inOut.setExit();
            inOut.sceneOut();
        }
    }
}
