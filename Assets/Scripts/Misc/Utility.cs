using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static string AmountToString(float amount)
    {
        if (amount < 1000)
        {
            return amount.ToString();
        }else if (amount >= 1000 && amount <1000000)
        {
            return Math.Round(amount / 1000, 1).ToString() + "K";
        }else if (amount >= 1000000 && amount < 1000000000)
        {
            return Math.Round(amount / 1000000, 1).ToString() + "M";
        }else if (amount >= 1000000000 && amount < 1000000000000)
        {
            return Math.Round(amount / 1000000000, 1).ToString() + "B";
        }else if (amount >= 1000000000000 && amount < 1000000000000000)
        {
            return Math.Round(amount / 1000000000000, 1).ToString() + "T";
        }
        else
        {
            return "INF";
        }
    }

    public static DateTime GetTime()
    {
        return DateTime.Now;
    }
    
    
}
