using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum ActionType
{
    PlantTree,
    BuildBridge,
    WaterTree,
    SpawnCow
}

[System.Serializable]
struct LevelAction
{
    LevelAction(ActionType action)
    {
        actionType = action;
        functionsWhenFinished = new UnityEvent();;
    }

    public ActionType actionType;
    public UnityEvent functionsWhenFinished;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameCircleInOut inOut;

    [SerializeField] private UnityEvent actionsBeforeStart = new UnityEvent();
    [SerializeField] private LevelAction[] levelActions;

    private List<LevelAction> actionsList;

    private int treesToPlant = 0;
    private int treesPlanted = 0;

    private int bridgesToBuild = 0;
    private int bridgesBuilt = 0;

    private int cowsToSpawn = 0;
    private int cowsSpawned = 0;

    private int treesToWater = 0;
    private int treesWatered = 0;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        actionsList = new List<LevelAction>();
        foreach (var action in levelActions)
        {
            actionsList.Add(action);
            if (action.actionType == ActionType.PlantTree)
                treesToPlant++;
        }

        actionsBeforeStart.Invoke();
    }

    public void PerformAction(ActionType actionType)
    {
        if (actionsList.Count > 0)
        {
            if (actionType == actionsList[0].actionType)
            {
                LevelAction action = levelActions[0];
                actionsList.RemoveAt(0);

                switch (actionType)
                {
                    case ActionType.PlantTree:
                        treesPlanted++;
                        break;
                    case ActionType.BuildBridge:
                        bridgesBuilt++;
                        break;
                    case ActionType.WaterTree:
                        treesWatered++;
                        break;
                    case ActionType.SpawnCow:
                        cowsSpawned++;
                        break;
                }

                action.functionsWhenFinished.Invoke();
                CheckIfLevelCompleted();
            }
        }
    }

    public void CheckIfLevelCompleted()
    {
        if (treesPlanted >= treesToPlant && 
            treesWatered >= treesToWater &&
            bridgesBuilt >= bridgesToBuild &&
            cowsSpawned >= cowsToSpawn)
        {
            Debug.Log("LevelFinished!");

            Invoke(nameof(loadScene), 3f);
            unlockNextLevel();
        }
    }

    private void unlockNextLevel()
    {
        PlayerPrefs.SetInt("lastLevelDone", 1);
        PlayerPrefs.Save();
    }

    public void loadScene()
    {
        inOut.sceneOut();
    }

    public void changeScene()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}
