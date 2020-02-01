using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMyZoneScript : MonoBehaviour
{
	public GameObject _myZone;
	public float _timeBetweenPoblateAnimation = 0.5f, _timeBetweenDespoblateAnimation = 1f;
	private List<GameObject> _tiles, _auxTiles;
	private bool expandingZone = false;

	public void Awake()
	{
		_tiles = new List<GameObject>();
		_auxTiles = new List<GameObject>();
		FillTiles();
	}

	public void poblateZone()
	{
		if (!expandingZone)
		{
			StopAllCoroutines();
			expandingZone = true;

			//Debug.Log(_auxTiles);

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
			FindObjectOfType<NumOfZonesCleaned>().decreaseNumberOfZonesCleaned();

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

			yield return new WaitForSeconds(_timeBetweenPoblateAnimation);
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

			yield return new WaitForSeconds(_timeBetweenDespoblateAnimation);
		}
	}
}
