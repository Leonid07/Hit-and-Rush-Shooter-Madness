using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public bool Landscape = false;

    public void Start()
    {
        if (Landscape == true)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        DataManger.InstanceData.LoadCoin();
        DataManger.InstanceData.ApplyCoin();
        DataManger.InstanceData.LoadCountLevel();
    }
}
