using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructable : Interactable
{
    [SerializeField] private ItemType itemNeeded;
    [SerializeField] private GameObject objectPrefab;

    public override void Interact()
    {
        if (PlayerController.Instance.GetCurrentItem().GetItemType() == itemNeeded)
        {
            PlayerController.Instance.ClearHand();
            Transform constructedObject =  Instantiate(objectPrefab).transform;
            constructedObject.position = transform.position;
            Destroy(this.gameObject);
        }
    }
}
