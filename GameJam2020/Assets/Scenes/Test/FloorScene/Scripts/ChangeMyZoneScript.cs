using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMyZoneScript : MonoBehaviour
{
	public GameObject _myZone;
	public float _timeBetweenPoblateAnimation = 0.5f, _timeBetweenDespoblateAnimation = 1f;
	private List<GameObject> _tiles, _auxTiles;
	int[] selectedFlowerInTile;
	private bool expandingZone = false;
	public GameObject _brokenTree, _treePlaceholder;
	public bool _tutorialLevel;
	public GameObject [] _flowers;


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

		selectedFlowerInTile = new int[_auxTiles.Count];

		int random = _flowers.Length + 20;

		for(int i = 0; i < selectedFlowerInTile.Length; i++)
		{
			int rnd = Random.Range(0, random);

			selectedFlowerInTile[i] = rnd;
		}

	}

	IEnumerator PoblateZone()
	{
		GameObject last = null;

		while(_tiles.Count > 0)
		{
			int rnd = Random.Range(0, _tiles.Count);

			GameObject g = _tiles[rnd].gameObject;

			TileBehaviour tb = g.GetComponent<TileBehaviour>();

			int index = _auxTiles.FindIndex(x => x == g);

			if (selectedFlowerInTile[index] < _flowers.Length)
			{
				GameObject f = Instantiate(_flowers[selectedFlowerInTile[index]], transform.position, Quaternion.identity);

				f.transform.parent = g.transform;
				f.transform.localPosition = new Vector3(0, 0.5f, 0);
				f.transform.localScale = new Vector3(1, 25, 1);
				//g.transform.GetChild(selectedFlowerInTile[index]).gameObject.SetActive(true);
			}
			tb.startGrowAnimation();

			_tiles.RemoveAt(rnd);

			yield return new WaitForSeconds(_timeBetweenPoblateAnimation * Time.deltaTime);

			last = g;
		}

		expandingZone = false;

		last.AddComponent<lastFlowerBehaviour>();
		last.GetComponent<lastFlowerBehaviour>().myFather = gameObject;

	}

	IEnumerator DespoblateZone()
	{
		GameObject last = null;

		while (_tiles.Count > 0)
		{
			int rnd = Random.Range(0, _tiles.Count);

			GameObject g = _tiles[rnd].gameObject;

			TileBehaviour tb = g.GetComponent<TileBehaviour>();

			tb.startDecreaseAnimation();

			_tiles.RemoveAt(rnd);

			yield return new WaitForSeconds(_timeBetweenDespoblateAnimation * Time.deltaTime);

			last = g;
		}

		last.AddComponent<lastFlowerBehaviour>();
		last.GetComponent<lastFlowerBehaviour>().myFather = gameObject;

		Instantiate(_brokenTree, transform.position, Quaternion.identity);
		GameObject ga = Instantiate(_treePlaceholder, transform.position, Quaternion.identity);
		ga.GetComponent<Constructable>().objectPrefab = this.gameObject;
		ga.SetActive(false);
		gameObject.SetActive(false);
	}
}
