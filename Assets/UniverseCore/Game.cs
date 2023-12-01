using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public static Game Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void AddBullet(Vector2 position, Vector2 velocity)
    {
        GameObject bulletInstance = Instantiate(bullet);
        bulletInstance.transform.position = position;
        bulletInstance.GetComponent<Bullet>().velocity = velocity;
    }
}
