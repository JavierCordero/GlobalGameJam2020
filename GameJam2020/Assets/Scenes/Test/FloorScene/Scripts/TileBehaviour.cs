using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
	public float maxSize = 1;
	public float increaseSize = 1;
	public float decreaseSize = 0.1f;
	public float overFloorLevel = 0.1f;

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
	}

	IEnumerator startDecreasingSize()
	{
		while (gameObject.transform.localScale.x > 0)
		{
			gameObject.transform.localScale = new Vector3(transform.localScale.x - decreaseSize / 100, transform.localScale.y, transform.localScale.z - decreaseSize / 100);
			yield return null;
		}
	}

	public void startGrowAnimation()
	{
		StopAllCoroutines();
		StartCoroutine(startGrouwingSize());
	}

	public void startDecreaseAnimation()
	{
		StopAllCoroutines();
		StartCoroutine(startDecreasingSize());
	}
}
