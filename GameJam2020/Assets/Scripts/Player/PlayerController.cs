﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;

    [SerializeField] private float aimSensitivity = 10f;
    [Range(0f, 1f)] [SerializeField] private float buildUpRot;
    [SerializeField] private float minInput;
    [SerializeField] private Vector3 pickUpZoneSize = new Vector3(3,1,3);

    // Aim
    private Vector3 forward;
    private Vector3 right;

    private PlayerInputHandler playerInput;

    // Items
    private Transform playerHand;
    private Item currentItem;

    #region Gets
    public float GetMinInput() { return minInput; }
    public float GetBuildUpRot() { return buildUpRot; }

    public float GetItemWeight()
    {
        if (currentItem == null) return 0;
        else return currentItem.GetWeight();
    }

    public Item GetCurrentItem() { return currentItem; }
    public bool HasItem() { return currentItem != null; }

    #endregion

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        playerInput = GetComponent<PlayerInputHandler>();
        playerHand = transform.Find("Hand");
    }

    void OnDrawGizmos()
    {
        if (EditorApplication.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3Int.RoundToInt(transform.position + transform.forward), pickUpZoneSize);
        }
    }

    Vector2Int GetCenterPos2D()
    {
        return VectorOperations.Vector3ToVector2Int(transform.position + transform.forward * 0.4f);
    }

    Vector2Int GetAimPos2D()
    {
        return VectorOperations.Vector3ToVector2Int(transform.position + transform.forward);
    }

    private void PickUpItem()
    {
        if (currentItem == null)
        {
            Interact();        
        }
    }

    private void Interact()
    {
        GameObject interactable = GetInteractable();

        if (interactable != null)
        {
            interactable.GetComponent<Interactable>().Interact();
            
        }
    }

    GameObject GetInteractable()
    {
        GameObject interactable = null;

        Collider[] objects = Physics.OverlapBox(transform.position + transform.forward, pickUpZoneSize);

        Vector3 aimTarget = transform.position + transform.forward;

        float minDistance = float.MaxValue;
        foreach (Collider o in objects)
        {
            if (o.CompareTag("Interactable"))
            {
                float objDistance = Vector3.Distance(aimTarget, o.transform.position);
                if (objDistance < minDistance)
                {
                    minDistance = objDistance;
                    interactable = o.gameObject;
                }
            }
        }

        return interactable;
    }

    bool CanReleaseItem()
    {
        Vector2 pos = GetCenterPos2D();

        Collider[] objects = Physics.OverlapBox(new Vector3(pos.x, 1, pos.y), new Vector3(0.9f, 0.9f, 0.9f));

        foreach (Collider o in objects)
        {
            if (o.CompareTag("Interactable"))
                return false;
        }

        return true;
    }

    private void ReleaseItem()
    {
        if (currentItem != null)
        {
            if (CanReleaseItem())
            {
                Vector2 pos = GetCenterPos2D();

                currentItem.transform.position = new Vector3(pos.x, 0.5f, pos.y);
                currentItem.transform.parent = null;
                currentItem.transform.localRotation = Quaternion.identity;
                currentItem.GetComponent<BoxCollider>().enabled = true;

                if (currentItem != null)
                {
                    SoundManager.Instance.PlaySound(GetComponent<FMODUnity.StudioEventEmitter>(), "event:/Drop");
                }

                currentItem = null;

               
            }
        }
    }

    public void PickUpRelease()
    {
        if(currentItem == null)
            PickUpItem();
        else
        {
            if(TryInteract())
                return;

            ReleaseItem();
        }
    }

    // Returns true if the player has interacted
    private bool TryInteract()
    {
        if (!CanReleaseItem())
        {
            Interact();
        }

        return false;
    }

    // Set item in player hand
    public void HoldItem(Transform item)
    {
        currentItem = item.GetComponent<Item>();
        item.GetComponent<BoxCollider>().enabled = false;
        item.parent = playerHand;
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.identity;

        if(SoundManager.Instance != null)
            SoundManager.Instance.PlaySound(GetComponent<FMODUnity.StudioEventEmitter>(), "event:/Pick");
    }

    public void ClearHand()
    {
        Destroy(currentItem.gameObject);
        currentItem = null;
    }
}
