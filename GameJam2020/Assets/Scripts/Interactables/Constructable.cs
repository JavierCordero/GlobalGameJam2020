using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructable : Interactable
{
    [SerializeField] private ItemType itemNeeded;
    [SerializeField] [HideInInspector] public GameObject objectPrefab;

    public override void Interact()
    {
        if (PlayerController.Instance.GetCurrentItem().GetItemType() == itemNeeded)
        {
            PlayerController.Instance.ClearHand();
			objectPrefab.SetActive(true);
			objectPrefab.GetComponent<ChangeMyZoneScript>().poblateZone();
            Destroy(this.gameObject);
        }
    }
}
