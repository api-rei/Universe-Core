using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Unstable : Planet
{
    public override Vector2 Orbit(Vector2 p1, float time)
    {
        float angle = Mathf.Atan2(p1.y, p1.x) + time;
        float distance = p1.magnitude;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * (distance + (Mathf.Cos(time * 5) - 1) * distance * 0.25f);
    }
    public override float T => Mathf.PI * 2;
    public override float Scale => 0.75f;
}
