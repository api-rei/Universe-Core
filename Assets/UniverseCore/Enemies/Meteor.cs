using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    float velocity = 0f;
    void Start()
    {
        velocity = (Random.value + 0.5f) / 1.5f * 5f;
    }
    void Update()
    {
        this.gameObject.transform.position += -this.gameObject.transform.position.normalized * velocity * Time.deltaTime;

    }
}
