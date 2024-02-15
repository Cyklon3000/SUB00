using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabberBehaviour : MonoBehaviour
{
    public float damage;
    private float stabDuration = 0.5f;
    private float stabIntensity = 2.5f;
    private float rotationSpeed = 0.05f;

    private float currentStabProgress = 1f;

    float tempMaxSize = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentStabProgress += Time.deltaTime / stabDuration;

        transform.localScale = Vector3.one + Vector3.one * stabIntensity * sizeCurve(currentStabProgress) * tempMaxSize;
        transform.localEulerAngles += new Vector3(0f, 0f, Time.deltaTime * rotationSpeed * 360f);
    }

    private float sizeCurve(float t)
    {
        return (t < 1f) ? -Mathf.Pow(2f*t - 1f, 2) + 1 : 0;
    }

    public void Stab()
    {
        currentStabProgress = 0f;
        tempMaxSize = 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Equals("Player")) return;
            collision.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(damage);
        tempMaxSize = sizeCurve(currentStabProgress);
        currentStabProgress = 0.5f;


    }
}
