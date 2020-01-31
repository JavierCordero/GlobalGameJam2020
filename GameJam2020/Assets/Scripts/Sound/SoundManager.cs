using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class soundParameters
{
    public FMODUnity.StudioEventEmitter emitter;
    public float delay;
}

public class SoundManager : MonoBehaviour
{
    List<soundParameters> sounds = new List<soundParameters>();

    #region instance
    private static SoundManager instance = null;

    // Game Instance Singleton
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion


    private void Update()
    {
        for(int i = 0; i < sounds.Count; i++)
        {
            sounds[i].delay -= Time.deltaTime;
            if(sounds[i].delay <= 0)
            {
                sounds[i].emitter.Play();
                sounds.RemoveAt(i);
            }
        }
    }

    public void PlaySound(FMODUnity.StudioEventEmitter emitter)
    {
        emitter.Play();
    }

    public void PlaySoundWithDelay(FMODUnity.StudioEventEmitter emitter, float delay)
    {
        soundParameters aux_ = new soundParameters();
        aux_.emitter = emitter;
        aux_.delay = delay;
        sounds.Add(aux_);
    }
}
