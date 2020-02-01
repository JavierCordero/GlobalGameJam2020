using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ActionType
{
    PlantTree,
    BuildBridge,
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

    [SerializeField] private LevelAction[] levelActions;

    private List<LevelAction> actionsList;
    private int treesToPlant = 0;
    private int treesPlanted = 0;

    private int cowsToSpawn = 0;
    private int cowsSpawned = 0;

    void Awake()
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
                    case ActionType.SpawnCow:
                        cowsSpawned++;
                        break;
                }

                action.functionsWhenFinished.Invoke();
                CheckIfLevelCompleted();
            }
        }
    }

    public void TestFuntion()
    {
        Debug.Log("Test");
    }

    public void CheckIfLevelCompleted()
    {
        if (treesPlanted >= treesToPlant && cowsSpawned >= cowsToSpawn)
        {
            Debug.Log("LevelFinished!");
        }
    }
}
