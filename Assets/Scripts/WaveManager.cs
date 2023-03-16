using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] GameObject playerEnemy;
    [SerializeField] GameObject npcEnemy;
    int waveNumber = 0;
    IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startWave()
    {
        waveNumber += 1;
        coroutine = spawnWave(waveNumber);
        StartCoroutine(coroutine);
    }

    private IEnumerator spawnWave(int number)
    {
        int enemies = number * 2 + 7;
        Vector3 playerEnemyOffset = Vector3.zero;
        Vector3 npcEnemyOffset = Vector3.zero;

        for (int i = 0; i < enemies; i++)
        {
            playerEnemyOffset = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(Random.Range(10f, 20f), Random.Range(10f, 20f), 0);
            npcEnemyOffset = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(Random.Range(10f, 20f), Random.Range(10f, 20f), 0);
            Instantiate(playerEnemy, player.position + playerEnemyOffset, Quaternion.Euler(0, 0, 0));
            Instantiate(npcEnemy, player.position + npcEnemyOffset, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(25f/enemies);
        }

        yield return null;
    }
}
