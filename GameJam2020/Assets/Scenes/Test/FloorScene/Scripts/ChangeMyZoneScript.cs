using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMyZoneScript : MonoBehaviour
{
	private enum Action { Poblate, Despoblate }

	public GameObject _myZone;
	public float _timeBetweenPoblateAnimation = 0.5f, _timeBetweenDespoblateAnimation = 1f;
	public GameObject _newFloor;
	private List<GameObject> _tiles;
	private bool expandingZone = false;

	public void poblateZone()
	{
		StopAllCoroutines();
		expandingZone = true;
		StartCoroutine(FillTiles(Action.Poblate));
	}

	public void despoblateZone()
	{
		if (!expandingZone)
		{
			StopAllCoroutines();
			expandingZone = false;
			FindObjectOfType<NumOfZonesCleaned>().decreaseNumberOfZonesCleaned();
			StartCoroutine(FillTiles(Action.Despoblate));
		}
	}

	IEnumerator FillTiles(Action a)
	{
		_tiles = new List<GameObject>();

		foreach (Transform t in _myZone.transform.GetComponentsInChildren<Transform>())
		{
			if (t != _myZone.transform && t.GetComponent<TileBehaviour>())
				_tiles.Add(t.gameObject);

			yield return null;
		}

		if(a == Action.Poblate)
			StartCoroutine(PoblateZone());
		else if(a == Action.Despoblate)
			StartCoroutine(DespoblateZone());
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

			GameObject g = _tiles[rnd].gameObject; //Instantiate(_newFloor, _tiles[rnd].transform.position, Quaternion.identity);

			TileBehaviour tb = g.GetComponent<TileBehaviour>();

			tb.startDecreaseAnimation();

			_tiles.RemoveAt(rnd);

			yield return new WaitForSeconds(_timeBetweenDespoblateAnimation);
		}
	}
}
