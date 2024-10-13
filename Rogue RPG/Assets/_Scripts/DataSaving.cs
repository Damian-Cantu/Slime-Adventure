using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSaving : MonoBehaviour
{
    public void SaveData(float health, float potions, float coins, float score, float time)
    {
        string hlth = health.ToString();
        string ptns = potions.ToString();
        string cns = coins.ToString();
        string scr = score.ToString();
        string tm = time.ToString();

        string file = @"Assets\Characters\Player\Player_Data.txt";
        if (!File.Exists(file))
        {
            File.WriteAllText(@"Assets\Characters\Player\Player_Data.txt", "");
        }
        string[] textLines1 = {hlth, ptns, cns, scr, tm};
        File.WriteAllLines(file, textLines1);
    }
}
