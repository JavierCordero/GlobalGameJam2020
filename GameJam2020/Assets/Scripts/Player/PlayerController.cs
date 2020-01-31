using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float aimSensitivity = 10f;
    [Range(0f, 1f)] [SerializeField] private float buildUpRot;
    [SerializeField] private float minInput;

    // Aim
    private Vector3 aimTarget;
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

    #endregion

    void Awake()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        playerInput = GetComponent<PlayerInputHandler>();
        playerHand = transform.Find("Hand");
    }

    void Update()
    {
        //UpdateAimTarget();
        //UpdatePlayerRotation();
    }

    void OnDrawGizmos()
    {
        if (EditorApplication.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3Int.RoundToInt(transform.position + transform.forward), Vector3.one);
        }
    }

    Vector2Int GetAimPos2D()
    {
        return VectorOperations.Vector3ToVector2Int(transform.position + transform.forward);
    }

    private void PickUpItem()
    {
        if (currentItem == null)
        {
            Vector2Int pos = GetAimPos2D();
            if (LevelManager.Instance.HasItem(pos))
            {
                currentItem = LevelManager.Instance.GetItem(pos);
                LevelManager.Instance.RemoveItem(pos);

                // Set item in player hand
                currentItem.transform.parent = playerHand;
                currentItem.transform.localPosition = Vector3.zero;
                currentItem.transform.localRotation = Quaternion.identity;
            }
        }
    }

    private void ReleaseItem()
    {
        if (currentItem != null)
        {
            Vector2Int pos = GetAimPos2D();
            if (!LevelManager.Instance.HasItem(pos))
            {
                LevelManager.Instance.AddItem(pos, currentItem);

                currentItem.transform.position = new Vector3(pos.x, 1, pos.y);
                currentItem.transform.parent = null;
                currentItem.transform.localRotation = Quaternion.identity;

                currentItem = null;
            }
        }
    }

    public void PickUpRelease()
    {
        if(currentItem == null)
            PickUpItem();
        else
            ReleaseItem();
    }
}
