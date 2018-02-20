using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaveRoomButton : Photon.PunBehaviour {

	public void LeftRoom()
    {
        NetWorkManager.networkManager.QuitRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
