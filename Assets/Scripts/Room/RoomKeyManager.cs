using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomKeyManager : MonoBehaviour
{
    public int[] keys = new int[3];

    public int[] GetKeysToDrop()
    {
        // TO BE CHANGED! NO MORE KEYS THAN DOORS TO OPEN!
        return keys;
    }
}
