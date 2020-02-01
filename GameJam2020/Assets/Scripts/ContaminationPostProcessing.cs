using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ContaminationPostProcessing : MonoBehaviour
{
    [SerializeField] private float maxOffset;
    [SerializeField] [Range(0f,1f)] float fogLevel;
    [SerializeField] private PostProcessProfile profile;

    private ColorGrading colorGrading;

    void Awake()
    {
        colorGrading = profile.GetSetting<ColorGrading>();
    }

    void Update()
    {
        colorGrading.contrast.value = (-maxOffset * fogLevel);
        colorGrading.saturation.value = (-maxOffset * fogLevel);
    }
}
