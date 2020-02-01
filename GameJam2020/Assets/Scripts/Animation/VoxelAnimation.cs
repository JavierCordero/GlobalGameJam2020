using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAnim", menuName = "Animation/VoxelAnimation", order = 1)]
public class VoxelAnimation : ScriptableObject
{
    [SerializeField] public string animationName;
    [SerializeField] public Mesh[] frames;
    [SerializeField] public float framesPerSecond = 5;
}
