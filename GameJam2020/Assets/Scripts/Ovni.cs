using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Recipe
{
    public ItemType itemType;
    public GameObject outputPrefab;
    public Sprite hintPNG;
}

public class Ovni : Interactable
{
    [SerializeField] private ItemType initialHint;
    [SerializeField] private Recipe[] recipes;

    private Dictionary<ItemType, Recipe> recipesDict;
    private GameObject hint;
    private SpriteRenderer hintRenderer;


    void Start()
    {
        recipesDict = new Dictionary<ItemType, Recipe>();

        foreach (var r in recipes)
        {
            recipesDict.Add(r.itemType, r);
        }

        hint = transform.GetChild(0).gameObject;
        hintRenderer = hint.transform.GetChild(0).GetComponent<SpriteRenderer>();
        HideHint();

        if(initialHint != ItemType.None)
            ShowHint(initialHint);
    }

    public override void Interact()
    {
        if (PlayerController.Instance.HasItem())
        {
            ItemType itemType = PlayerController.Instance.GetCurrentItem().GetItemType();

            if (recipesDict.ContainsKey(itemType))
            {
                PlayerController.Instance.ClearHand();

                Transform itemToHold = Instantiate(recipesDict[itemType].outputPrefab).transform;
                PlayerController.Instance.HoldItem(itemToHold);

                HideHint();
            }
        }
    }

    public void ShowHint(ItemType itemType)
    {
        if (recipesDict.ContainsKey(itemType))
        {
            hint.SetActive(true);
            hintRenderer.sprite = recipesDict[itemType].hintPNG;
        }
    }

    public void HideHint()
    {
        hint.SetActive(false);
    }
}
