using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayerInfo : Photon.PunBehaviour {
    int playerCount = 0;
    bool allNameAreSync;
    // Use this for initialization
    void Start ()
    {
        allNameAreSync = false;
    }
    private void LateUpdate()
    {
        if (PhotonNetwork.playerList.Length != playerCount && allNameAreSync)
            allNameAreSync = false;
        if (!allNameAreSync)
        {
            playerCount = PhotonNetwork.playerList.Length;
            UpdateConnectedPlayer();
        }
    }
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        UpdateConnectedPlayer();
    }
    public override void OnLeftRoom()
    {
        UpdateConnectedPlayer();
    }
    void UpdateConnectedPlayer()
    {
        UpdateRoomInfo();
    }
    void UpdateRoomInfo()
    {
        for (int j = 0; j < transform.childCount; j++)
            transform.GetChild(j).transform.GetComponent<Text>().text = "Writing player name";
        PhotonPlayer[] pp = ToolsScript.SortArray(PhotonNetwork.playerList);
        for(int k = 0; k < pp.Length; k++)
            transform.GetChild(k).transform.GetComponent<Text>().text = (string)pp[k].CustomProperties["Name"];
        allNameAreSync = true;
    }

    public void QuitRoom()
    {
        UpdateRoomInfo();
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
