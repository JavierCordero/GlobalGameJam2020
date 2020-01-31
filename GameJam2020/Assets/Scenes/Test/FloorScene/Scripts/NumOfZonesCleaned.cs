using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumOfZonesCleaned : MonoBehaviour
{
	private int numOfZonesCleaned = 0, totalNumOfZonesCleaned;

    // Start is called before the first frame update
    void Start()
    {
		totalNumOfZonesCleaned = FindObjectsOfType<ChangeMyZoneScript>().Length;
	}

	public void increaseNumberOfZonesCleaned(GameObject obj)
	{
		numOfZonesCleaned++;

		if (numOfZonesCleaned < totalNumOfZonesCleaned)
			obj.GetComponent<ChangeMyZoneScript>().despoblateZone();
	}

	public void decreaseNumberOfZonesCleaned()
	{
		numOfZonesCleaned--;
	}
}
