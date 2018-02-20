using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkManager : Photon.PunBehaviour {
    public static NetWorkManager networkManager;
    protected MainMenu mainMenu;
    TypedLobby typeLobby;
    LobbyType lobbyType;
    string masterServerAdd;
    string appID;
    // Use this for initialization
    public NetWorkManager() { }

    public static NetWorkManager Instance
    {
        get
        {
            if (networkManager == null)
            {
                networkManager = new NetWorkManager();
            }
            return networkManager;
        }
    }
    void Awake () {
        PhotonNetwork.lobby.Name = "General Lobby";
        masterServerAdd = "Cae";
        appID = "a50f0a2c-bfdd-454a-83c1-0c5ed1fa7630";
        ExitGames.Client.Photon.Hashtable PlayerProperty = new ExitGames.Client.Photon.Hashtable();
        PlayerProperty["Ping"] = PhotonNetwork.GetPing();
        PhotonNetwork.player.SetCustomProperties(PlayerProperty);
        DontDestroyOnLoad(gameObject);
	}
    public void Start()
    {
        Debug.Log("NetWorkStart");
        mainMenu = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        ConnectToPhotonServer();
    }
    void ConnectToPhotonServer()
    {
        PhotonNetwork.ConnectUsingSettings("0.1v");
        PhotonNetwork.automaticallySyncScene = true;
    }
    public void ConnectToARoom()
    {
        string roomNameEntered;
        mainMenu.TriggerMainMenuButtons(false);
        if (!PhotonNetwork.insideLobby)
        {
            Debug.Log("not inside lobby");
            return;
        }
        roomNameEntered = mainMenu.GetInputEntered();
        if (roomNameEntered != "")
        {
            if (!LookIfNameIsAlreadyTaken(roomNameEntered))
                ConnectToNetwork(roomNameEntered);
            else
            {
                StartCoroutine(mainMenu.NameTakenTimer());
                mainMenu.TriggerMainMenuButtons(true);
            }
        }
    }
    bool LookIfNameIsAlreadyTaken(string name)
    {
        foreach(RoomInfo info in PhotonNetwork.GetRoomList())
        {
            if (name == info.Name)
                return true;
        }
        return false;
    }
    public void ConnectToNetwork(string roomName = "Room 1")
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(roomName, ro, typeLobby);
        StartCoroutine(WaitUntilJoinedRoom());
    }
    public void JoinRandomRoom()
    {
        if (!PhotonNetwork.insideLobby)
        {
            Debug.Log("not connected");
            return;
        }
        PhotonNetwork.JoinRandomRoom();
        StartCoroutine(WaitUntilJoinedRoom());
    }
    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.room.Name != null)
            Debug.Log(PhotonNetwork.room.Name + " : PhotonNetwork.room.Name");
    }
    public override void OnLeftRoom()
    {
        mainMenu.TriggerMainMenuButtons(true);
        mainMenu.ShowListOfRoom();
    }
    public void QuitRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            foreach (PhotonPlayer ply in PhotonNetwork.playerList)
            {
                Debug.Log(ply.CustomProperties["Ping"].ToString());
            }
        }
        PhotonNetwork.LeaveRoom();
    }
    public void CancelButton()
    {
        mainMenu.UnableToConnectWindow(false);
    }
    public void RetryButton()
    {
        mainMenu.UnableToConnectWindow(false);
        ConnectToPhotonServer();
    }
    IEnumerator WaitUntilJoinedRoom()
    {
        while (!PhotonNetwork.inRoom)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            Debug.Log(PhotonNetwork.room.Name);
        }
    }
}
