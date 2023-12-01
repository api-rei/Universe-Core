using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    void Update()
    {
        this.gameObject.transform.position += (Vector3)velocity * Time.fixedDeltaTime;
        if (this.gameObject.transform.position.magnitude > 30)
            Destroy(this.gameObject);
    }
}
