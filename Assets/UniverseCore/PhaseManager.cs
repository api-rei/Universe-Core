using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    private int phase = 1;
    float time = 0f;
    bool isProcessing = false;
    float enemyWeight = 0f;
    float spawnTick = 0f;
    private void Start()
    {
        phase = 1;
        time = 0f;
        isProcessing = true;
        enemyManager.StartPhase(1);
    }
    private void Update()
    {
        if (isProcessing)
        {
            enemyWeight += Time.deltaTime * 0.1f * phase;
            spawnTick += Time.deltaTime;
            if (spawnTick > 0.1f)
            {
                print(isProcessing + ":" + enemyWeight);
                if (Random.value < enemyWeight)
                {
                    enemyManager.SpawnEnemy();
                    enemyWeight = 0f;
                }
                spawnTick = 0f;
            }
            time += Time.deltaTime;
            if (Time.deltaTime > 15f + phase)
            {
                phase += 1;
                time = 0f;
                isProcessing = false;
                DoNextPhase();
                enemyManager.StartPhase(phase);
            }
        }
    }
    void DoNextPhase()
    {

    }
}
