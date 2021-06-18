using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRingManager : MonoBehaviour
{
    [SerializeField] private float timeForShrink = 5f;
    [SerializeField] private GameObject[] _spawns;
    [SerializeField] GameObject shrink;
    [SerializeField] Light[] _lights;
    void Start()
    {
        StartCoroutine(spawnShrinker());
    }
    private IEnumerator spawnShrinker()
    {
        yield return new WaitForSeconds(timeForShrink);
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        StartCoroutine(changeColor());
        foreach (Light target in _lights) target.enabled = false;
        int player = Random.Range(0, _spawns.Length);
        Instantiate(shrink, _spawns[player].transform);
    }

    private IEnumerator changeColor()
    {
        float t = 0;
        Color startColor = new Color(0.654902f, 0.7370836f, 0.852f);
        Color targetColor = new Color(0.4627451f, 0.3372549f, 0.3411765f);
        for (; ; )
        {
            RenderSettings.ambientLight = Color.Lerp(startColor, targetColor, t);
            yield return new WaitForFixedUpdate();
            if (t == 1) break;
            else t += 0.005f;
        }

    }
}
