using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleParticles : MonoBehaviour
{
    private Loader loader;
    private int roomID = -1;

    // Start is called before the first frame update
    void Awake()
    {
        loader = GameObject.Find("StageManager").GetComponent<Loader>();
        Invoke("SetRoomID", 1f);
    }

    private void SetRoomID()
    {
        roomID = loader.GetRoomIDClosestToPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (loader.currentRoomID != roomID)
        {
            GetComponent<ParticleSystem>().Stop();
        }
        else if (!GetComponent<ParticleSystem>().isPlaying)
        {
            GetComponent<ParticleSystem>().Play();
        }

    }
}
