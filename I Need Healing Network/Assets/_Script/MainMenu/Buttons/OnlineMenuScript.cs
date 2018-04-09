using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineMenuScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Button playButton = ObjectFinders.FindAnyChild<Button>(transform, "PlayButton");
        if (!PhotonNetwork.player.IsMasterClient)
           playButton.interactable = false;
    }
}
