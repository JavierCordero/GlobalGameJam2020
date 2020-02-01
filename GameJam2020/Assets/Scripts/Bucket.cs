using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Item
{
    [SerializeField] private Mesh fullBucket;

    private Mesh emptyBucket;
    private bool bucketIsFull = false;
    private MeshFilter meshFilter;

    public bool IsBucketFull() { return bucketIsFull; }

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        emptyBucket = meshFilter.mesh;
    }

    public void TryFillBucket()
    {
        if (!bucketIsFull)
        {
            meshFilter.mesh = fullBucket;
            bucketIsFull = true;
        }
    }

    public void TryEmptyBucket()
    {
        if (bucketIsFull)
        {
            meshFilter.mesh = emptyBucket;
            bucketIsFull = false;
        }
    }
}
