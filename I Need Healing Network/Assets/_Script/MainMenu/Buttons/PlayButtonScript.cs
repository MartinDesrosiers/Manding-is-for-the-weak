using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonScript : Photon.PunBehaviour {

    public void StartGame()
    {
        photonView.RPC("CallStartGame", PhotonTargets.All, null);
    }
    [PunRPC]
    void CallStartGame()
    {
        GameObject menu = GameObject.Find("OnlineMenu");
        _GameManager.Instance.StartGame(true);
        menu.SetActive(false);
    }
}
