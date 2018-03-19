using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayerInfo : Photon.PunBehaviour {
    int playerCount = 0;
    bool allNameAreSync;
    PhotonPlayer photonPlayer;
    PhotonView photonview;
    void Start ()
    {
        allNameAreSync = false;
        photonview = PhotonView.Get(this);
        photonview.viewID = PhotonNetwork.AllocateViewID();
        if (PhotonNetwork.player.IsLocal)
        {
            photonPlayer = new PhotonPlayer(true, PhotonNetwork.player.ID, "tapoui" + photonview.viewID);
            PhotonNetwork.player.NickName = "tapoui" + photonview.viewID;
        }
    }
    private void LateUpdate()
    {
        if(PhotonNetwork.playerList.Length != playerCount || !allNameAreSync)
        {
            if (PhotonNetwork.player.IsLocal)
            {
                playerCount = PhotonNetwork.playerList.Length;
                UpdateConnectedPlayer();
            }
        }
    }
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        UpdateConnectedPlayer();
    }
    void UpdateConnectedPlayer()
    {
        UpdateRoomInfo();
    }
    void UpdateRoomInfo()
    {
        allNameAreSync = true;
        for (int j = 0; j < transform.childCount; j++)
        {
            transform.GetChild(j).transform.GetComponent<Text>().text = "Writing player name";
        }
        List<PhotonPlayer> list = new List<PhotonPlayer>();
        SortPlayerList(ref list);
        for (int j = 0; j < list.Count; j++)
        {
            transform.GetChild(j).transform.GetComponent<Text>().text = list[j].NickName;
            if (list[j].NickName == "")
                allNameAreSync = false;
        }
    }
    public void QuitRoom()
    {
        NetWorkManager.networkManager.Disconnect();
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    void SortPlayerList(ref List<PhotonPlayer> list)
    {
        List<int> toSort = new List<int>();
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            toSort.Add(PhotonNetwork.playerList[i].ID);
        }
        toSort.Sort();
        int j = 0;
        do
        {
            if (toSort[0] == PhotonNetwork.playerList[j].ID)
            {
                list.Add(PhotonNetwork.playerList[j]);
                toSort.RemoveAt(0);
                j = 0;
            }
            else
            {
                j++;
            }
        } while (toSort.Count > 0);
    }
}
