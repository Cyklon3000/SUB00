using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    [SerializeField]
    private Sprite[] keySprites = new Sprite[3];
    [SerializeField]
    private Sprite currencySprite;

    [HideInInspector]
    public string itemType;
    [HideInInspector]
    public int itemAmount;

    private float spawnProgress = 0f;
    private float spawnTime = 0.3f;

    private Vector3 pickUpStart;
    private float pickUpProgress = -1f;
    private float pickUpTime = 1.25f;

    private GameObject player;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public void Setup()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        switch (itemType)
        {
            case "Currency":
                spriteRenderer.sprite = currencySprite;
                break;
            case "BronzeKey":
                spriteRenderer.sprite = keySprites[0];
                break;
            case "SilverKey":
                spriteRenderer.sprite = keySprites[1];
                break;
            case "GoldKey":
                spriteRenderer.sprite = keySprites[2];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnProgress > 1f) goto SpawnProgressOver;
        spawnProgress += Time.deltaTime / spawnTime;
        transform.localScale = Vector3.one * Mathf.Clamp01(spawnProgress);
        return;

        SpawnProgressOver:
        if (pickUpProgress < 0f) return;

        pickUpProgress += Time.deltaTime / pickUpTime;
        transform.position = new Vector3(
            Mathf.Lerp(pickUpStart.x, player.transform.position.x, pickUpProgress),
            Mathf.Lerp(pickUpStart.y, player.transform.position.y, pickUpProgress),
            0f);
        transform.localScale = Vector3.one * Mathf.Clamp01(1f - pickUpProgress);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f - pickUpProgress);

        if (pickUpProgress < 1f) return;

        player.GetComponent<Inventory>().addItems(itemType, itemAmount);
        Destroy(gameObject);
    }

    public static void SpawnItems(string type, int amount, Vector3 pos)
    {
        int stackLimit = 9;

        while (amount >= stackLimit)
        {
            SpawnItem(type, 9, pos);
            amount -= stackLimit;
        }
        SpawnItem(type, amount, pos);
    }

    private static void SpawnItem(string type, int amount, Vector3 pos)
    {
        if (amount == 0) return;
        float randomifyStrength = 0.5f;
        Vector3 randomifyedPos = pos + (Vector3)GetRandomUnitVector2D().normalized * randomifyStrength;
        Item newItem = Instantiate(PrefabManager.GetPrefabs().itemBase, randomifyedPos, Quaternion.Euler(Vector3.zero)).GetComponent<Item>();
        newItem.itemType = type;
        newItem.itemAmount = amount;
        newItem.Setup();
    }

    public void PickItemUp()
    {
        if (spawnProgress <= 1f) return;
        if (pickUpProgress >= 0f) return;
        pickUpProgress = 0f;
        pickUpStart = transform.position;
    }

    private static Vector2 GetRandomUnitVector2D()
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float x = Mathf.Cos(randomAngle);
        float y = Mathf.Sin(randomAngle);
        return new Vector2(x, y);
    }
}
