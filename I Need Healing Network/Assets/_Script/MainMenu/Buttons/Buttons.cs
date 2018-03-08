using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : Photon.PunBehaviour {
    protected MainMenu mainMenu;
    // Use this for initialization
    protected virtual void Start () {
        mainMenu = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        Debug.Log(mainMenu);
        if (PhotonNetwork.connected)
            TriggerMainMenuButtons(true);
    }
    public override void OnConnectedToPhoton()
    {
        TriggerMainMenuButtons(true);
        Debug.Log("lghaskfjgh");
    }
    public void TriggerMainMenuButtons(bool active)
    {
        InterractableTrigger.ToggleInterractable<Button>(transform, "Buttons", active);
    }
}
