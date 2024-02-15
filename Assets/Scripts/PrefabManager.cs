using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PrefabManager : MonoBehaviour
{
    public GameObject napalmParticles;
    

    public static PrefabManager GetPrefabs()
    {
        return GameObject.Find("PrefabCollector").GetComponent<PrefabManager>();
    }
}
