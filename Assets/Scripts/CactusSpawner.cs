using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] cactuses = null;

    private GlobalManager globalManager;

    public void StartSpawn()
    {
        StartCoroutine(spawnCactus());
    }

    private void Start()
    {
        globalManager = FindObjectOfType<GlobalManager>();
    }

    public void PuregCactuses()
    {
        Cactus[] spawnedCactuses = FindObjectsOfType<Cactus>();
        foreach (Cactus cactus in spawnedCactuses)
        {
            Destroy(cactus.gameObject);
        }
    }

    private IEnumerator spawnCactus()
    {
        while (true)
        {
            float minRange = 2 - globalManager.worldSpeed*0.8f + globalManager.initialWorldSpeed;
            float maxRange = 5 - globalManager.worldSpeed*0.8f + globalManager.initialWorldSpeed;
            if (minRange < 0.8f) minRange = 0.8f;
            if (maxRange < 1.2f) maxRange =1.2f;
            float randomTime = Random.Range(minRange,maxRange);
            yield return new WaitForSeconds(randomTime);
            int index = Random.Range(0, cactuses.Length);
            Instantiate(cactuses[index], new Vector3(11, 1f, 0), Quaternion.identity);
        }
        
        
    }
}
