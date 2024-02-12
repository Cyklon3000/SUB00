using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTable
{
    private int monster0Amount { get; set; }
    private int randomRange0 { get; set; }
    private int monster1Amount { get; set; }
    private int randomRange1 { get; set; }
    private int monster2Amount { get; set; }
    private int randomRange2 { get; set; }

    public MonsterTable(int monster0Amount, int randomRange0, int monster1Amount, int randomRange1, int monster2Amount, int randomRange2)
    {
        this.monster0Amount = monster0Amount;
        this.randomRange0 = randomRange0;
        this.monster1Amount = monster1Amount;
        this.randomRange1 = randomRange1;
        this.monster2Amount = monster2Amount;
        this.randomRange2 = randomRange2;
    }

    public int[] getRandomAmounts()
    {
        int[] amounts = new int[3];
        amounts[0] = this.monster0Amount + (int) Random.Range(this.monster0Amount - this.randomRange0, 
                                                              this.monster0Amount + this.randomRange0 + 1.0f);
        amounts[1] = this.monster1Amount + (int) Random.Range(this.monster1Amount - this.randomRange1, 
                                                              this.monster1Amount + this.randomRange1 + 1.0f);
        amounts[2] = this.monster2Amount + (int) Random.Range(this.monster2Amount - this.randomRange2, 
                                                              this.monster2Amount + this.randomRange2 + 1.0f);
        return amounts;
    }
}
