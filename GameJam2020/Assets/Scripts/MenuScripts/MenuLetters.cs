using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLetters : MonoBehaviour
{
    public GameObject[] _re;
    public GameObject[] _pair;
    public ParticleSystem[] _particleSystems;
    private int _pairIndex;
    public GameObject[] _planet;
    private int _planetIndex;

    public float _timeBtwLetterDestroy;
    public float _timeBtwLetterCreate;

    private bool _destroying;
    private bool _creating;

    private bool _animationInProcess;
    private float _actualTime;

    // Start is called before the first frame update
    void Start()
    {
        _pairIndex = _pair.Length;
        _planetIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartAnimation(true);
        }
        if (_animationInProcess)
        {
            _actualTime += Time.deltaTime;
            if (_destroying)
            {
                if(_actualTime >= _timeBtwLetterDestroy)
                {
                    _particleSystems[_pairIndex - 1].Play();
                    Destroy(_pair[_pairIndex - 1].gameObject);
                    _pairIndex--;
                    _actualTime = 0;
                }
            }
            else if (_creating)
            {
                if(_actualTime >= _timeBtwLetterCreate)
                {
                    _planet[_planetIndex].SetActive(true);
                    _planetIndex++;
                    _actualTime = 0;
                }
            }

            if (_pairIndex == 0)
            {
                _destroying = false;
                _creating = true;
            }
            else if (_planetIndex == _planet.Length)
            {
                _creating = false;
            }
        }
    }

    public void StartAnimation(bool b)
    {
        _animationInProcess = b;
        _destroying = true;
    }
}
