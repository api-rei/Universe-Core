using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Line : Planet
{
    public override Vector2 Orbit(Vector2 p1, float time)
    {
        float angle = time;
        float distance = p1.magnitude;
        return (Vector2)(Mathf.Sin(angle) * (Quaternion.Euler(0, 0, 90) * p1.normalized) * 15f) + p1;
    }
    public override float T => Mathf.PI * 2;
    public override float Scale => 1.5f;
}
