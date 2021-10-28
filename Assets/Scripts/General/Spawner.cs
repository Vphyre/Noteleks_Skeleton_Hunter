using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int enemyWaveAmount;
    [SerializeField] private float timeToRespawn;
    [SerializeField] protected Transform [] rangedPlaces;
    [SerializeField] protected Transform [] meleePlaces;
    [SerializeField] private Pooling rangedPool;
    [SerializeField] private Pooling meleePool;
    private List<GameObject> enemies = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        // Min enemy amount
        if(enemyWaveAmount<=1)
        {
            enemyWaveAmount = 2;
        }
        // Max enemy amount
        else if(enemyWaveAmount>30)
        {
            enemyWaveAmount = 30;
        }
        InvokeRepeating("Respawn", 3.1f, timeToRespawn);
    }

    private int GetQtdEnemiesDead()
    {
        List<GameObject> deadGO = new List<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            if(!enemy.activeInHierarchy)
            {
                deadGO.Add(enemy);
            }
        }
        foreach (GameObject dead in deadGO)
        {
            enemies.Remove(dead);
        }
        return enemies.Count;
    }

    private void Respawn()
    {
        int qtdEnemiesDead = GetQtdEnemiesDead();
        for (int i = 0; i < enemyWaveAmount-qtdEnemiesDead; i++)
        {
            int sort = Random.Range(0, 2);

            if (sort == 0)
            {
                GameObject enemy = meleePool.GetPooledObject();
                int sortPos = Random.Range(0, meleePlaces.Length);
                enemy.GetComponent<Enemy>().Spawn(meleePlaces[sortPos].position);
                enemies.Add(enemy);
            }
            if (sort == 1)
            {
                GameObject enemy = rangedPool.GetPooledObject();
                int sortPos = Random.Range(0, rangedPlaces.Length);
                enemy.GetComponent<Enemy>().Spawn(rangedPlaces[sortPos].position);
                enemies.Add(enemy);
            }
            Debug.Log(enemies.Count);
        }
    }
}
