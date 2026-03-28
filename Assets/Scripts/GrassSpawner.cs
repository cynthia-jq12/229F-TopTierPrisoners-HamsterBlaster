using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public GameObject grassPrefab;
    public int grassCount = 100;
    public Vector3 areaSize = new Vector3(50, 0, 50);

    void Start()
    {
        SpawnGrass();
    }

    void SpawnGrass()
    {
        for (int i = 0; i < grassCount; i++)
        {
            float randomX = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
            float randomZ = Random.Range(-areaSize.z / 2f, areaSize.z / 2f);

            Vector3 spawnPos = new Vector3(randomX, 0, randomZ) + transform.position;

            GameObject grass = Instantiate(grassPrefab, spawnPos, Quaternion.Euler(0, Random.Range(0, 360), 0));

            grass.transform.parent = this.transform;
        }
    }
}