using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Dictionary<string, GameObject> weapons = new Dictionary<string, GameObject>();
    private Dictionary<string, WeaponStats> weaponSettings = new Dictionary<string, WeaponStats>();
    private GameObject currentWeapon;
    private WeaponStats currentWeaponSetting;

    // Weapon modifiers
    public float rangeMultiplier = 1f;
    public float widthMultiplier = 1f;
    public float damageMultiplier = 1f;
    public float speedMultiplier = 1f;

    
    private float lastTick = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            weapons[child.name] = child.gameObject;
            weaponSettings[child.name] = child.GetComponent<WeaponStats>();
        }

        currentWeapon = weapons["GasTorch"];
        currentWeaponSetting = weaponSettings[currentWeapon.name];
        currentWeaponSetting.Enable();
        UpdateWeaponSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            currentWeaponSetting.Activate();
            if (Time.time - lastTick > weaponSettings[currentWeapon.name].tickDuration)
            {
                lastTick = Time.time;
                currentWeaponSetting.executeTick();
            }
        }
        else
        {
            currentWeaponSetting.Deactivate();
        }
    }

    public void UpdateWeaponSettings()
    {
        Vector2[] points = currentWeapon.GetComponent<PolygonCollider2D>().points;
        // Update range of flame
        points[1] = points[0] + rangeMultiplier * (points[1] - points[0]);
        points[2] = points[0] + rangeMultiplier * (points[2] - points[0]);
        // Update width of flame
        Vector2 middlePoint = 0.5f * (points[1] + points[2]);
        points[1] = middlePoint + widthMultiplier * (points[1] - middlePoint);
        points[2] = middlePoint + widthMultiplier * (points[2] - middlePoint);

        currentWeaponSetting.tickDamage = currentWeaponSetting.baseTickDamage * damageMultiplier;
        currentWeaponSetting.tickDuration = currentWeaponSetting.baseTickDuration /= speedMultiplier;
        Debug.Log(currentWeaponSetting.tickDuration);
    }
}
