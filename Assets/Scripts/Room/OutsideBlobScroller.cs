using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideBlobScroller : MonoBehaviour
{
    private SpriteRenderer blobs1;
    private SpriteRenderer blobs2;

    private Vector2 blobs1Direction;
    private Vector2 blobs2Direction;

    private float directionChangeStrength = 2f;
    private float speed = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        blobs1 = transform.GetChild(0).GetComponent<SpriteRenderer>();
        blobs2 = transform.GetChild(1).GetComponent<SpriteRenderer>();

        blobs1Direction = GetRandomUnitVector2D();
        blobs2Direction = GetRandomUnitVector2D();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 randomDirectionChange = GetRandomUnitVector2D() * directionChangeStrength * Time.deltaTime;
        blobs1Direction = (blobs1Direction + randomDirectionChange).normalized;
        // Offset by blobs1Direction * Time.deltaTime

        randomDirectionChange = GetRandomUnitVector2D() * directionChangeStrength * Time.deltaTime;
        blobs2Direction = (blobs2Direction + randomDirectionChange).normalized;
        // Offset by blobs2Direction * Time.deltaTime
    }

    private static Vector2 GetRandomUnitVector2D()
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float x = Mathf.Cos(randomAngle);
        float y = Mathf.Sin(randomAngle);
        return new Vector2(x, y);
    }
}
