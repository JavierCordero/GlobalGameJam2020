using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum levelState { blocked, available, done }

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private int levelIndex;

    public Mesh grey;
    public Mesh red;
    public Mesh green;

    private MeshFilter meshFilter;

    private bool locked = false;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    public string getLevel() {
        if(!locked)
            return "Level"+levelIndex.ToString();
        else
            return null;
    }

    public void setState(levelState l)
    {
        switch (l)
        {
            case levelState.available:
                meshFilter.mesh = red;
                break;
            case levelState.done:
                meshFilter.mesh = green;
                break;
            case levelState.blocked:
                meshFilter.mesh = grey;
                locked = true;
                break;
        }
    }
}
