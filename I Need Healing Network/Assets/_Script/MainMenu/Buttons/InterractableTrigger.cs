using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class InterractableTrigger {

    public static void ToggleInterractable<T>(Transform trans, string objName, bool b) where T : Button
    {
        for (int i = 0; i < trans.childCount; i++)
        {
            trans.GetChild(i).GetComponent<T>().interactable = b;
        }        
    }
}
