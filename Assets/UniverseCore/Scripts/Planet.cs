using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Planet : ScriptableObject
{
    public string id;
    public GameObject prefab;
    public abstract Vector2 Orbit(Vector2 p1, float time);
    public abstract float T { get; }
    public abstract float Scale { get; }
}
