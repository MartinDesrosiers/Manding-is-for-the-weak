using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayerInfo : Photon.PunBehaviour {
    int i = 0;
	// Use this for initialization
	void Start ()
    {
        UpdateConnectedPlayer();
    }
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("OnPhotonPlayerConnected() " + newPlayer.NickName); // not seen if you're the player connecting
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
        {
            transform.GetChild(j).transform.GetComponent<Text>().text = "Writing player name";
        }
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                if (PhotonNetwork.playerList[i].ID - 1 == j)
                    transform.GetChild(j).transform.GetComponent<Text>().text = PhotonNetwork.playerList[i].NickName;
            }
        }
    }

    public void QuitRoom()
    {
        NetWorkManager.networkManager.Disconnect();
        UpdateRoomInfo();
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
