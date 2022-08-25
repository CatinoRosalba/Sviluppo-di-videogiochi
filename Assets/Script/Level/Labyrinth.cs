using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth : MonoBehaviour
{
    private bool clear;
    public GameObject slime;

    private void Awake()
    {
        clear = false;
    }

    private void Update()
    {
        if(slime == null)
        {
            clear = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && clear == false)
        {
            ActivateSpawners();
        }
    }

    private void ActivateSpawners()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.CompareTag("Spawner"))
            {
                child.GetComponent<SpawnerScript>().SpawnEnemy();
            }
        }
    }
}
