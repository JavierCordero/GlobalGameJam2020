using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructable : Interactable
{
    [SerializeField] private ItemType itemNeeded;
    [SerializeField] public GameObject objectPrefab;
    [SerializeField] public ActionType actionPerformed;

    public override void Interact()
    {
        if (PlayerController.Instance.HasItem() && PlayerController.Instance.GetCurrentItem().GetItemType() == itemNeeded)
        {
            LevelManager.Instance.PerformAction(actionPerformed);

            PlayerController.Instance.ClearHand();
			objectPrefab.SetActive(true);

            ChangeMyZoneScript zone = objectPrefab.GetComponent<ChangeMyZoneScript>();
            if(zone != null)
                zone.poblateZone();
            
            Destroy(this.gameObject);
        }
    }

    public void EnableObject()
    {
        transform.gameObject.SetActive(true);
    }

    public void DisableObject()
    {
        transform.gameObject.SetActive(false);
    }
}
