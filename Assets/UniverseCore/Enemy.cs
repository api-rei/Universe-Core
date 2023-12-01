using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ScriptableObject
{
    public GameObject prefab;
    public float maxChance;
    public float multi;
    public int phase;
}
