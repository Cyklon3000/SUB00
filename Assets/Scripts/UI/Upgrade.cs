using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Upgrade : MonoBehaviour
{
    private int selectedUpgrade = -1;
    private int selectedCost = 0;
    private GameObject selection;
    private Inventory inventory;
    private GameObject currentUpgrades;
    private GameObject upgradesContainer;
    private GameObject upgradesLevel1;
    private GameObject upgradesLevel0;

    // Start is called before the first frame update
    void Start()
    {
        upgradesContainer = GameObject.Find("Upgrades");
        upgradesLevel1 = GameObject.Find("UpgradesLevel1");
        upgradesLevel0 = GameObject.Find("UpgradesLevel0");
        HideUpgradeUI();
        selection = GameObject.Find("Selection");
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        // Quit menu if moved, Esc or E
        if (!(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).Equals(Vector2.zero))
            || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.E))
            HideUpgradeUI();
    }

    public void ShowUpgradeUI()
    {
        upgradesContainer.SetActive(true);
        int currentLevel = GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
        Debug.Log($"Current Level: {currentLevel}");
        selectedUpgrade = -1;
        if (currentLevel == 2)
            currentUpgrades = upgradesLevel1;
        else
            currentUpgrades = upgradesLevel0;
        currentUpgrades.SetActive(true);
        GameObject.Find("Selection").GetComponent<RectTransform>().localPosition = 150 * Vector3.right * selectedUpgrade;
        SelectUpgrade(-1);
    }

    public void SelectUpgrade(int upgrade)
    {
        int cost = int.Parse(currentUpgrades.transform.GetChild(upgrade + 1).Find("Pricing").GetComponent<TextMeshProUGUI>().text);
        if (inventory.HasItems("Currency", cost))
        {
            selectedUpgrade = upgrade;
            GameObject.Find("Selection").GetComponent<RectTransform>().localPosition = 150 * Vector3.right * selectedUpgrade;
        }
    }

    public void HideUpgradeUI()
    {
        upgradesLevel1.SetActive(false);
        upgradesLevel0.SetActive(false);
        upgradesContainer.SetActive(false);
    }

    public void ConfirmSelection()
    {
        selectedCost = int.Parse(currentUpgrades.transform.GetChild(selectedUpgrade + 1).Find("Pricing").GetComponent<TextMeshProUGUI>().text);
        if (!inventory.PayItems("Currency", selectedCost)) return;
        int currentLevel = GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
        string newWeapon = GameObject.Find("Weapon").GetComponent<Weapon>().currentWeapon.name;
        if (currentLevel == 2)
        {
            switch (selectedUpgrade)
            {
                case -1:
                    newWeapon = "Flamethrower1";
                    break;
                case 0:
                    newWeapon = "Flamethrower2";
                    break;
                case 1:
                    newWeapon = "Flamethrower3";
                    break;
            }
        }
        else
        {
            switch (selectedUpgrade)
            {
                case -1:
                    break; // Stays same
                case 0:
                    newWeapon = "FlamethrowerBurner";
                    break;
                case 1:
                    newWeapon = "FlamethrowerNapalm";
                    break;
            }
        }
        GameObject.Find("Weapon").GetComponent<Weapon>().SwitchToWeapon(newWeapon);
        HideUpgradeUI();
        // Progress to next Level
        GameObject.Find("GameManager").GetComponent<GameManager>().LoadNextLevel();
    }
}
