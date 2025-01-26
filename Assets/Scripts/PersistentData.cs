using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData
{
    private static PersistentData instance = new PersistentData();
    public static PersistentData Instance
    {
        get
        {
            return instance;
        }
    }

    public bool postSunRoof = false;
    public string prevScene;
    public int prevBubbles = 2;
}
