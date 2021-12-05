using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloud;

    private GlobalManager globalManager;
    // Start is called before the first frame update
    void Start()
    {
        globalManager = FindObjectOfType<GlobalManager>();
        StartCoroutine(spawnCloud());
    }


    private IEnumerator spawnCloud()
    {
        while (true)
        {
            float minRange = 2 - globalManager.worldSpeed * 0.8f + globalManager.initialWorldSpeed;
            float maxRange = 4 - globalManager.worldSpeed * 0.8f + globalManager.initialWorldSpeed;
            if (minRange < 1) minRange = 1;
            if (maxRange < 2f) maxRange = 2f;
            float randomTime = Random.Range(minRange, maxRange);
            yield return new WaitForSeconds(randomTime);
            int randomY = Random.Range(12, 18);
            int randomZ = Random.Range(60, 120);
            Instantiate(cloud, new Vector3(80, randomY, randomZ), Quaternion.identity);
        }


    }
}
