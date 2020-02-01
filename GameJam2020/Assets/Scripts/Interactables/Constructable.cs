using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructable : Interactable
{
    [SerializeField] private ItemType itemNeeded;
    [SerializeField] public GameObject objectPrefab;

    public override void Interact()
    {
        if (PlayerController.Instance.HasItem() && PlayerController.Instance.GetCurrentItem().GetItemType() == itemNeeded)
        {
            if (PlayerController.Instance.GetCurrentItem().GetItemType() == ItemType.TreeSappling)
                LevelManager.Instance.PerformAction(ActionType.PlantTree);

            PlayerController.Instance.ClearHand();
			objectPrefab.SetActive(true);
			objectPrefab.GetComponent<ChangeMyZoneScript>().poblateZone();
            
            Destroy(this.gameObject);
        }
    }
}
