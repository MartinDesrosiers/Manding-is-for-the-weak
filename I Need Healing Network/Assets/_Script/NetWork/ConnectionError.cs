using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionError : Photon.PunBehaviour
{
    MainMenu mainMenu;
    private void Start()
    {
        mainMenu = GameObject.Find("MainMenu").GetComponent<MainMenu>();
    }
    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        mainMenu.UnableToConnectWindow(true);
    }
    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        foreach (object o in codeAndMsg)
            Debug.Log(o.ToString());
        StartCoroutine(mainMenu.FailedToConnect("Can't Create room"));
    }
    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        StartCoroutine(mainMenu.FailedToConnect("Can't Join room"));
    }
    public override void OnDisconnectedFromPhoton()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        StartCoroutine(mainMenu.FailedToConnect("You have been Disconnected"));
        OnLeftRoom();
    }
}