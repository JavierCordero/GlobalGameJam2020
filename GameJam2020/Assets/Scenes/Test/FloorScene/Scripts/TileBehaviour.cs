using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
	public float maxSize = 1;
	public float increaseSize = 1;
	public float overFloorLevel = 0.1f;
	private GameObject parent;

	private void Start()
	{
		gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + overFloorLevel, transform.position.z);
	}

	IEnumerator startGrouwingSize()
	{
		while(gameObject.transform.localScale.x < maxSize)
		{
			gameObject.transform.localScale = new Vector3(transform.localScale.x + increaseSize / 100, transform.localScale.y, transform.localScale.z + increaseSize / 100);
			yield return null;
		}

		DestroyImmediate(parent);
		Destroy(this);
	}

	public void setParent(GameObject p)
	{
		parent = p;
	}

	public void startAnimation()
	{
		StartCoroutine(startGrouwingSize());
	}
}
