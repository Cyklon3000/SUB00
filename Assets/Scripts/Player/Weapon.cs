using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Dictionary<string, GameObject> weapons = new Dictionary<string, GameObject>();
    private Dictionary<string, WeaponStats> weaponSettings = new Dictionary<string, WeaponStats>();
    public GameObject currentWeapon;
    private WeaponStats currentWeaponSetting;
    
    private float lastTick = 0f;
    private float lastNapalmTick = 0f;

    private AudioManager audioManager;

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

        SwitchToWeapon("GasTorch"); //GasTorch
        //Invoke("SwitchToWeaponf", 6f);
        //Invoke("SwitchToWeaponf", 12f);
        //Invoke("SwitchToWeaponf", 18f);
        //Invoke("SwitchToN", 24f);

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
                currentWeaponSetting.ExecuteTick();
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
            currentWeaponSetting.ExecuteNapalmTick();
        }
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
