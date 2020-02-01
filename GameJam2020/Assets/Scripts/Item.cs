using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Wood,
    TreeSappling,
    Example3
}

public class Item : Interactable
{
    [Header("Item")]
    [SerializeField] private ItemType itemType;
    [SerializeField] private GameObject itemPrefab;
    [Range(0f, 10f)] [SerializeField] protected float weight;

    public ItemType GetItemType() { return itemType; }
    public float GetWeight() { return weight; }

    public override void Interact()
    {
        if (!PlayerController.Instance.HasItem())
        {
            Transform itemToHold = Instantiate(itemPrefab).transform;
            PlayerController.Instance.HoldItem(itemToHold);
            Destroy(this.gameObject);
        }
    }
}
