using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Dictionary<string, GameObject> weapons = new Dictionary<string, GameObject>();
    private Dictionary<string, WeaponStats> weaponSettings = new Dictionary<string, WeaponStats>();
    private GameObject currentWeapon;
    private WeaponStats currentWeaponSetting;
    
    private float lastTick = 0f;
    private float lastNapalmTick = 0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            weapons[child.name] = child.gameObject;
            weaponSettings[child.name] = child.GetComponent<WeaponStats>();
            weaponSettings[child.name].Disable();
        }

        SwitchToWeapon("FlamethrowerBurner");
        //Invoke("SwitchToWeaponf", 6f);
        //Invoke("SwitchToWeaponf", 12f);
        //Invoke("SwitchToWeaponf", 18f);
        //Invoke("SwitchToN", 24f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            currentWeaponSetting.Activate();
            if (Time.time - lastTick > currentWeaponSetting.tickDuration)
            {
                lastTick += currentWeaponSetting.tickDuration;
                currentWeaponSetting.executeTick();
            }
        }
        else
        {
            currentWeaponSetting.Deactivate();
        }

        if (Time.time - lastNapalmTick > currentWeaponSetting.tickDuration)
        {
            lastNapalmTick += currentWeaponSetting.tickDuration;
            if (!currentWeaponSetting.isNapalm) return;
            if (Random.Range(0f, 1f) > 0.33f) return;
            currentWeaponSetting.executeNapalmTick();
        }
    }

    private int i = 1;

    public void SwitchToWeaponf()
    {
        SwitchToWeapon($"Flamethrower{i}");
        i++;
    }

    public void SwitchToN()
    {
        SwitchToWeapon("FlamethrowerNapalm");
    }

    public void SwitchToWeapon(string weaponName)
    {
        if (currentWeaponSetting != null)
            currentWeaponSetting.Disable();
        currentWeapon = weapons[weaponName];
        currentWeaponSetting = weaponSettings[weaponName];
        currentWeaponSetting.Enable();
        lastNapalmTick = currentWeaponSetting.tickDuration / 2;
    }
}
