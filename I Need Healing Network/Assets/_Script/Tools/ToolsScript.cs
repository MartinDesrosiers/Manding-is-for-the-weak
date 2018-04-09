using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ToolsScript {

    public static PhotonPlayer[] SortArray(PhotonPlayer[] phoPlay)
    {
        int[] playerArray = new int[phoPlay.Length];
        PhotonPlayer[] sortedArray = new PhotonPlayer[phoPlay.Length];
        for (int i = 0; i < phoPlay.Length; i++)
        {
            playerArray[i] = (int)phoPlay[i].CustomProperties["ID"];
        }
        System.Array.Sort(playerArray);
        for (int k = 0; k < phoPlay.Length; k++)
            for (int l = 0; l < phoPlay.Length; l++)
                if (playerArray[k] == (int)phoPlay[l].CustomProperties["ID"])
                    sortedArray[k] = phoPlay[l];
        return sortedArray;
    }
}
