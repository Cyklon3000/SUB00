using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    private void Awake()
    {
        Transform eHint = transform.Find("EHint");
        eHint.localScale = 0.5f * new Vector3(eHint.localScale.x / eHint.lossyScale.x, eHint.localScale.y / eHint.lossyScale.y, eHint.localScale.z / eHint.lossyScale.z);
    }

    public void ShowEHint()
    {
        transform.Find("EHint").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideEHint()
    {
        transform.Find("EHint").GetComponent<SpriteRenderer>().enabled = false;
    }
}
