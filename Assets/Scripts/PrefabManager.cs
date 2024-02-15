using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PrefabManager : MonoBehaviour
{
    public GameObject napalmParticles;
    public GameObject bubblePrefab;
    public GameObject tentaclePrefab;
    public GameObject tinyTentaclePrefab;
    public GameObject harpunePrefab;
    public GameObject gooPrefab;
    public GameObject roomPrefab;
    public GameObject monsterBase;
    public GameObject itemBase;
    public GameObject exitHatch;
    
    public static PrefabManager GetPrefabs()
    {
        return GameObject.Find("PrefabCollector").GetComponent<PrefabManager>();
    }
}
