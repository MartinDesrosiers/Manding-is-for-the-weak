using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : Photon.PunBehaviour {
    protected MainMenu mainMenu;
    // Use this for initialization
    protected void Init () {
        mainMenu = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        if (PhotonNetwork.connected)
            TriggerMainMenuButtons(true);
    }
    public override void OnConnectedToPhoton()
    {
        TriggerMainMenuButtons(true);
    }
    public void TriggerMainMenuButtons(bool active)
    {
        InterractableTrigger.ToggleInterractable<Button>(transform, "Buttons", active);
    }
}
