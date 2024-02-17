using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> itemAmountDict = new Dictionary<string, int>();
    public Dictionary<string, TextMeshProUGUI> itemCounterDict = new Dictionary<string, TextMeshProUGUI>();

    // Start is called before the first frame update
    void Start()
    {
        itemAmountDict["Currency"] = 0;
        itemCounterDict["Currency"] = GameObject.Find("CurrencyCounter").GetComponent<TextMeshProUGUI>();
        itemAmountDict["BronzeKey"] = 1;
        itemCounterDict["BronzeKey"] = GameObject.Find("BronzeKeyCounter").GetComponent<TextMeshProUGUI>();
        itemAmountDict["SilverKey"] = 0;
        itemCounterDict["SilverKey"] = GameObject.Find("SilverKeyCounter").GetComponent<TextMeshProUGUI>();
        itemAmountDict["GoldKey"] = 0;
        itemCounterDict["GoldKey"] = GameObject.Find("GoldKeyCounter").GetComponent<TextMeshProUGUI>();
        UpdateCounters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetItems(string itemType)
    {
        return itemAmountDict[itemType];
    }

    public void AddItems(string itemType, int amount)
    {
        itemAmountDict[itemType] += amount;
        UpdateCounters();
    }

    public void SetItems(string itemType, int amount)
    {
        itemAmountDict[itemType] = amount;
        UpdateCounters();
    }

    public bool PayItems(string itemType, int amount)
    {
        // Returns weather the player has enough of the given item and subtracts if so
        if (itemAmountDict[itemType] >= amount)
        {
            SubItems(itemType, amount);
            return true;
        }
        return false;
    }

    public bool HasItems(string itemType, int amount)
    {
        // Returns weather the player has enough of the given item
        if (itemAmountDict[itemType] >= amount)
        {
            return true;
        }
        return false;
    }

    public void SubItems(string itemType, int amount)
    {
        itemAmountDict[itemType] -= amount;
        UpdateCounters();
    }

    public void RemoveItem(string itemType)
    {
        itemAmountDict[itemType] = 0;
        UpdateCounters();
    }

    public void UpdateCounters()
    {
        foreach (string itemType in itemAmountDict.Keys)
        {
            itemCounterDict[itemType].text = itemAmountDict[itemType].ToString();
        }
    }

    public void ClearKeys()
    {
        RemoveItem("BronzeKey");
        RemoveItem("SilverKey");
        RemoveItem("GoldKey");
    }

    public static string IDtoItemName(int id)
    {
        switch (id)
        {
            case 0:
                return "BronzeKey";
            case 1:
                return "SilverKey";
            case 2:
                return "GoldKey";
            case 3:
                return "Currency";
        }
        return null;
    }
}
