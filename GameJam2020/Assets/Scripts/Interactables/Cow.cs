using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Interactable
{
    [SerializeField] private GameObject bonePrefab;

    private bool constructed = false;
    private GameObject constructModel;
    private GameObject aliveModel;

    void Start()
    {
        constructModel = transform.GetChild(0).gameObject;
        aliveModel = transform.GetChild(1).gameObject;
    }

    public override void Interact()
    {
        if (!constructed && PlayerController.Instance.HasItem() && PlayerController.Instance.GetCurrentItem().GetItemType() == ItemType.Cow)
        {
            ConstructCow();
        }
    }

    void ConstructCow()
    {
        constructed = true;
        PlayerController.Instance.ClearHand();
        constructModel.SetActive(false);
        aliveModel.SetActive(true);

        LevelManager.Instance.PerformAction(ActionType.SpawnCow);

        if (!LevelManager.Instance.AllTreesArePlanted())
            StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        // ToDo: DIE Animation

        yield return new WaitForSeconds(2f);

        Transform bone =  Instantiate(bonePrefab).transform;
        bone.position = transform.position - transform.forward;
        bone.parent = null;

        LevelManager.Instance.PerformAction(ActionType.CowDied);
       
        aliveModel.SetActive(false);
        constructModel.SetActive(true);
        constructed = false;

        yield return null;
    }
}
