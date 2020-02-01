using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSpawner : MonoBehaviour
{
    public GameObject[] planetsToSpawn;
    public float spawnerTimeMin;
    public float spawnerTimeMax;
    public float objVelocity;

    private void Start()
    {
        Invoke("Spawn", 0f);
    }

    private void Spawn()
    {
        int index = Random.Range(0, planetsToSpawn.Length);
        float randRotation = Random.Range(0.0f, 200.0f);
        float spawnT = Random.Range(spawnerTimeMin, spawnerTimeMax);

        GameObject go = Instantiate(planetsToSpawn[index], transform.position,Quaternion.Euler(new Vector3(randRotation,0,0)));
        go.GetComponent<MainMenuPlanetMovement>().velocity = objVelocity;
        go.transform.localScale = transform.localScale;
        go.transform.parent = transform;
        Invoke("Spawn", spawnT);
    }
}
