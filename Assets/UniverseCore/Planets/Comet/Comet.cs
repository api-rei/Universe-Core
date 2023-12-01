using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Comet : Planet
{
    private const float rX = 1;
    private const float rY = 1.5f;
    public override Vector2 Orbit(Vector2 p1, float time)
    {
        float angle = time;
        Vector2 p = (p1 * rX * Mathf.Cos(angle)) + ((Vector2)(Quaternion.Euler(0, 0, -90) * p1) * rY * Mathf.Sin(angle));
        float distance = p1.magnitude;
        return p;
    }
    public override float T => Mathf.PI * 2;
    public override float Scale => 0.75f;
}
