using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkManager : Photon.PunBehaviour {
    public static NetWorkManager networkManager;
    TypedLobby typeLobby;
    LobbyType lobbyType;
    string masterServerAdd;
    string appID;
    // Use this for initialization
    public NetWorkManager(){}
    public static NetWorkManager Instance
    {
        get
        {
            return networkManager;
        }
    }
    void Awake ()
    {
        if (networkManager != null && networkManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            networkManager = this;
            DontDestroyOnLoad(gameObject);
        }
        PhotonNetwork.lobby.Name = "General Lobby";
        masterServerAdd = "Cae";
        appID = "a50f0a2c-bfdd-454a-83c1-0c5ed1fa7630";//appId. Can't play online without that
        PhotonNetwork.PhotonServerSettings.AppID = appID;
        PhotonNetwork.ConnectToRegion(CloudRegionCode.cae, "1.0");
        ExitGames.Client.Photon.Hashtable PlayerProperty = new ExitGames.Client.Photon.Hashtable();
        PlayerProperty["Ping"] = PhotonNetwork.GetPing();
        PhotonNetwork.player.SetCustomProperties(PlayerProperty);
    }
    public void Start()
    {
        Debug.Log("NetWorkStart");
        ConnectToPhotonServer();
    }
    public void ConnectToPhotonServer()
    {
        PhotonNetwork.ConnectUsingSettings("0.1v");
        PhotonNetwork.automaticallySyncScene = true;
    }
    public void ConnectToNetwork(string roomName = "Room 1")
    {
        //Set the room options
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(roomName, ro, typeLobby);
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
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu")
            PhotonNetwork.LoadLevel("MainMenu");
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
    public IEnumerator WaitUntilJoinedRoom()
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
