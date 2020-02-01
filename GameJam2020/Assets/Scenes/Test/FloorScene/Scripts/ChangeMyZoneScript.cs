﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMyZoneScript : MonoBehaviour
{
	public GameObject _myZone;
	public float _timeBetweenPoblateAnimation = 0.5f, _timeBetweenDespoblateAnimation = 1f;
	private List<GameObject> _tiles, _auxTiles;
	private bool expandingZone = false;
	public GameObject _brokenTree, _treePlaceholder;
	public bool _tutorialLevel;

	public void Awake()
	{
		_tiles = new List<GameObject>();
		_auxTiles = new List<GameObject>();
		FillTiles();
	}

	private void Start()
	{
		if (_tutorialLevel)
		{
			poblateZone();
		}
	}

	public void poblateZone()
	{
		if (!expandingZone)
		{
			StopAllCoroutines();
			expandingZone = true;

			foreach (GameObject g in _auxTiles)
				_tiles.Add(g);
			StartCoroutine(PoblateZone());
		}
	}

	public void despoblateZone()
	{
		if (!expandingZone)
		{
			StopAllCoroutines();
			expandingZone = false;

			foreach (GameObject g in _auxTiles)
				_tiles.Add(g);

			StartCoroutine(DespoblateZone());
		}
	}

	private void FillTiles()
	{
		foreach (Transform t in _myZone.transform.GetComponentsInChildren<Transform>())
		{
			if (t != _myZone.transform && t.GetComponent<TileBehaviour>())
			{
				_tiles.Add(t.gameObject);
				_auxTiles.Add(t.gameObject);
			}
		}
	}

	IEnumerator PoblateZone()
	{
		while(_tiles.Count > 0)
		{
			int rnd = Random.Range(0, _tiles.Count);

			GameObject g = _tiles[rnd].gameObject;

			TileBehaviour tb = g.GetComponent<TileBehaviour>();

			tb.startGrowAnimation();

			_tiles.RemoveAt(rnd);

			yield return new WaitForSeconds(_timeBetweenPoblateAnimation * Time.deltaTime);
		}

		expandingZone = false;
		FindObjectOfType<NumOfZonesCleaned>().increaseNumberOfZonesCleaned(gameObject);
	}

	IEnumerator DespoblateZone()
	{
		while (_tiles.Count > 0)
		{
			int rnd = Random.Range(0, _tiles.Count);

			GameObject g = _tiles[rnd].gameObject;

			TileBehaviour tb = g.GetComponent<TileBehaviour>();

			tb.startDecreaseAnimation();

			_tiles.RemoveAt(rnd);

			yield return new WaitForSeconds(_timeBetweenDespoblateAnimation * Time.deltaTime);
		}

		FindObjectOfType<NumOfZonesCleaned>().decreaseNumberOfZonesCleaned();
		Instantiate(_brokenTree, transform.position, Quaternion.identity);
		GameObject ga = Instantiate(_treePlaceholder, transform.position, Quaternion.identity);
		ga.GetComponent<Constructable>().objectPrefab = this.gameObject;
		ga.SetActive(false);
		gameObject.SetActive(false);
	}
}
