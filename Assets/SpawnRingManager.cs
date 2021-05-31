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
        RenderSettings.ambientLight = Color.black;
        foreach (Light target in _lights) target.enabled = false;
        int player = Random.Range(0, _spawns.Length);
        Instantiate(shrink, _spawns[player].transform);
    }
}
