using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Example1,
    Example2,
    Example3
}

public class Item : Interactable
{
    [Header("Item")]
    [SerializeField] private GameObject itemPrefab;
    [Range(0f, 10f)] [SerializeField] protected float weight;
    private ItemType itemType;

    protected void SetItemType(ItemType type) { itemType = type; }
    public ItemType GetItemType() { return itemType; }
    public float GetWeight() { return weight; }

    public override void Interact()
    {
        Debug.Log("Interact");
        Transform itemToHold = Instantiate(itemPrefab).transform;
        PlayerController.Instance.HoldItem(itemToHold);
        Destroy(this.gameObject);
    }
}
