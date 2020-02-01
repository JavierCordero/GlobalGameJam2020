using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Recipe
{
    public ItemType input;
    public GameObject output;
}

public class Ovni : Interactable
{
    [SerializeField] private Recipe[] recipes;

    private Dictionary<ItemType, GameObject> recipesDict;

    void Start()
    {
        recipesDict = new Dictionary<ItemType, GameObject>();

        foreach (var r in recipes)
        {
            recipesDict.Add(r.input, r.output);
        }
    }

    public override void Interact()
    {
        if (PlayerController.Instance.HasItem())
        {
            ItemType itemType = PlayerController.Instance.GetCurrentItem().GetItemType();

            if (recipesDict.ContainsKey(itemType))
            {
                PlayerController.Instance.ClearHand();

                Transform itemToHold = Instantiate(recipesDict[itemType]).transform;
                PlayerController.Instance.HoldItem(itemToHold);
            }
        }
    }
}
