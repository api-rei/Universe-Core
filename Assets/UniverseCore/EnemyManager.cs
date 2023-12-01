using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies;
    List<float> allWeights = new List<float>();
    private List<GameObject> spawningEnemies = new List<GameObject>();
    float sum = 0f;
    public void StartPhase(int phase)
    {
        spawningEnemies = new List<GameObject>();
        allWeights = new List<float>();
        sum = 0;
        foreach (Enemy enemy in enemies)
        {
            float weight = Mathf.Clamp((phase - enemy.phase) * enemy.multi, 0, enemy.maxChance);
            allWeights.Add(weight);
            sum += weight;
        }
    }
    public void SpawnEnemy()
    {
        float randomValue = -1f;
        while (randomValue > 0f && randomValue < 1f)
        {
            randomValue = Random.value;
        }
        float value = sum * randomValue;
        int index = 0;
        foreach (var weight in allWeights)
        {
            value -= weight;
            if (value < 0)
            {
                SpawnEnemy(enemies[index]);
            }
            index++;
        }
    }
    private void SpawnEnemy(Enemy enemy)
    {
        GameObject gameObject = Instantiate(enemy.prefab);
        float angle = Random.value * Mathf.PI * 2;
        gameObject.transform.position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 30f;
        spawningEnemies.Add(gameObject);
    }
    private void ResetEnemies()
    {
        foreach (var enemy in spawningEnemies)
        {
            Destroy(enemy);
        }
        spawningEnemies.Clear();
    }
}
