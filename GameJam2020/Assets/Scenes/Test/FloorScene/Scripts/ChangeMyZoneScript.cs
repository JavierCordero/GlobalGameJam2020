using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMyZoneScript : MonoBehaviour
{

	public GameObject _myZone;
	public float _timeBetweenAnimation = 0.5f;
	public GameObject _newFloor;
	private List<GameObject> _tiles;

	private void Awake()
	{
		_tiles = new List<GameObject>();

		foreach (Transform t in _myZone.transform.GetComponentsInChildren<Transform>())
		{
			if(t != _myZone.transform)
				_tiles.Add(t.gameObject);
		}
	}

	public void ChangeMyZone()
	{
		StartCoroutine(PoblateZone());
	}

	IEnumerator PoblateZone()
	{
		while(_tiles.Count > 0)
		{
			int rnd = Random.Range(0, _tiles.Count);

			GameObject g = Instantiate(_newFloor, _tiles[rnd].transform.position, Quaternion.identity);

			TileBehaviour tb = g.GetComponent<TileBehaviour>();

			tb.setParent(_tiles[rnd]);
			tb.startAnimation();

			_tiles.RemoveAt(rnd);

			yield return new WaitForSeconds(_timeBetweenAnimation);
		}

		DestroyImmediate(this);
	}
}
