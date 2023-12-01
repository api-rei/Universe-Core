using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetPlacer : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] List<Planet> planetScriptableObjects = new List<Planet>();
    [SerializeField] Camera mainCamera;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] List<GameObject> holograms = new List<GameObject>();
    [SerializeField] GameObject border;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreText2;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject result;
    [SerializeField] bool isTitle = false;
    [Header("Colors")]
    [SerializeField] Color SpeedyRed;
    private Dictionary<string, Planet> planetAssets = new Dictionary<string, Planet>();
    private List<string> planetKeys = new List<string>();
    private List<PlanetData> planets = new List<PlanetData>();
    class PlanetData
    {
        public PlanetData(PlanetC Item1, Func<Vector2, float, Vector2> Item2, int Item3, float Item4, float Item5, GameObject Item6)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
            this.Item3 = Item3;
            this.Item4 = Item4;
            this.Item5 = Item5;
            this.Item6 = Item6;
        }
        public PlanetC Item1;
        public Func<Vector2, float, Vector2> Item2;
        public int Item3;
        public float Item4;
        public float Item5;
        public GameObject Item6;
    }
    string selecting = "";
    int speed = 1;
    private float score = 0f;
    private float time = 0f;
    private float lastPlacedTime = 0f;
    private bool dead = false;
    void Start()
    {
        time = 0f; lastPlacedTime = 0f;
        planetAssets = new Dictionary<string, Planet>();
        planets.Clear();
        foreach (var planetObject in planetScriptableObjects)
        {
            planetAssets.Add(planetObject.id, planetObject);
        }
        planetKeys = new List<string>(planetAssets.Keys);
        selecting = (planetKeys[UnityEngine.Random.Range(0, planetAssets.Count)]);
        if (isTitle)
        {
            Place("Earth", new Vector2(25, 0));
            Place("Comet", new Vector2(0, 30));
        }
    }
    void Trail()
    {
        float T = planetAssets[selecting].T;
        int n = 250;
        Vector3[] positions = new Vector3[n + 1];
        for (int i = 0; i < n + 1; i++)
        {
            positions[i] = planetAssets[selecting].Orbit(mainCamera.ScreenToWorldPoint(Input.mousePosition), (T / n) * i);
        }
        lineRenderer.positionCount = n + 1;
        lineRenderer.SetPositions(positions);
    }
    void CheckDistances()
    {
        List<PlanetData> destroiablePlanets = new List<PlanetData>();
        for (int i = 0; i < planets.Count; i++)
        {
            var planet = planets[i];
            float minDistance = Mathf.Infinity;
            foreach (var planet2 in planets)
            {
                float sqrDis = (planet2.Item1.transform.position - planet.Item1.transform.position).magnitude - planet2.Item5;
                if (sqrDis < minDistance && planet2 != planet)
                    minDistance = sqrDis;
            }
            if (minDistance < planet.Item5)
            {
                destroiablePlanets.Add(planet);
            }
            else if (minDistance - planet.Item5 < 5 && !(planet.Item4 - planet.Item5 < 5))
            {
                score += 5;
            }
            planets[i].Item4 = minDistance;
        }
        foreach (var planet in destroiablePlanets)
        {
            Destroy(planet.Item1.gameObject);
            Destroy(planet.Item6);
            planets.Remove(planet);
            Death();
        }
    }
    void Update()
    {
        if (dead)
            return;
        time += Time.deltaTime;
        foreach (var planet in planets)
        {
            planet.Item1.time += Time.deltaTime;
            planet.Item1.gameObject.transform.position = planet.Item2(planet.Item1.p1, planet.Item1.time * planet.Item3 * 0.55f);
        }
        if (isTitle)
            return;
        for (int i = 0; i < holograms.Count; i++)
        {
            float value = (Time.time - i * 0.5f + 1.5f) % (holograms.Count * 0.5f);
            float lateValue = (Time.time - Time.deltaTime - i * 0.5f + 1.5f) % (holograms.Count * 0.5f);
            float iso = 1.25f;
            if (lateValue < iso && value >= iso)
                holograms[i].transform.DOScale(0f, 0.25f).SetEase(Ease.OutSine);
            if (lateValue > value)
                holograms[i].transform.DOScale(planetAssets[selecting].Scale * 0.5f, 0.25f).SetEase(Ease.OutSine);
            holograms[i].transform.position = planetAssets[selecting].Orbit(mainCamera.ScreenToWorldPoint(Input.mousePosition), value * speed * 0.55f);
            //holograms[i].transform.localScale = planetAssets[selecting].Scale * Vector2.one;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Place(selecting, mainCamera.ScreenToWorldPoint(Input.mousePosition));
        }
        Trail();
        CheckDistances();
        lastPlacedTime += Time.deltaTime;
        float limit = (1 / Mathf.Pow(time + 60, 1.25f)) * 5000;
        if (lastPlacedTime > limit)
            Death();
        border.transform.position = Vector3.up * ((lastPlacedTime / limit) * 100 - 50);
        scoreText.text = $"score:{score}";
    }
    private void Death()
    {
        dead = true;
        scoreText2.text = $"score:{score}";
        timeText.text = $"time:{(int)time}";
        result.transform.DOScale(1f, 0.25f).SetEase(Ease.OutSine);
    }
    void Place(string id, Vector2 position)
    {
        GameObject gameObject = Instantiate(planetAssets[id].prefab);
        PlanetC planetC = gameObject.AddComponent<PlanetC>();
        planetC.p1 = position;
        gameObject.transform.position = position;
        GameObject clonedTrail = Instantiate(lineRenderer.gameObject);
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);
        LineRenderer clonedRenederer = clonedTrail.GetComponent<LineRenderer>();
        clonedRenederer.positionCount = lineRenderer.positionCount;
        clonedRenederer.startColor = new Color((clonedRenederer.startColor).r, (clonedRenederer.startColor).g, (clonedRenederer.startColor).b, 0.25f);
        clonedRenederer.endColor = new Color((clonedRenederer.endColor).r, (clonedRenederer.endColor).g, (clonedRenederer.endColor).b, 0.25f);
        clonedRenederer.SetPositions(points);
        planets.Add(new PlanetData(planetC, (p1, t) => planetAssets[id].Orbit(p1, t), speed, Mathf.Infinity, planetAssets[id].Scale, clonedRenederer.gameObject));
        selecting = (planetKeys[UnityEngine.Random.Range(0, planetAssets.Count)]);
        speed = (UnityEngine.Random.value < 0.5 ? 1 : -1) * UnityEngine.Random.Range(1, 2);
        switch (Mathf.Abs(speed))
        {
            case 1:
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = Color.white;
                break;
            case 2:
                lineRenderer.startColor = SpeedyRed;
                lineRenderer.endColor = SpeedyRed;
                break;
        }
        lastPlacedTime = 0f;
    }
    [SerializeField] string SceneName;
    public void Retry()
    {
        SceneManager.LoadScene(SceneName);
    }
}
