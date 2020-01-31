using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private Item[] initialItemsOnMap;
    private Dictionary<Vector2Int, Item> levelItems = new Dictionary<Vector2Int, Item>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            AddInitialItems();
        }
        else
            Destroy(this.gameObject);
    }

    void AddInitialItems()
    {
        foreach (Item item in initialItemsOnMap)
        {
            levelItems.Add(VectorOperations.Vector3ToVector2Int(item.transform.position), item);
        }
    }


    public bool HasItem(Vector2Int position) { return levelItems.ContainsKey(position); }

    public Item GetItem(Vector2Int position) { return levelItems[position]; }

    public void AddItem(Vector2Int position, Item item)
    {
        levelItems.Add(position, item);
    }

    public void RemoveItem(Vector2Int position)
    {
        if (levelItems.ContainsKey(position))
            levelItems.Remove(position);
    }
}
