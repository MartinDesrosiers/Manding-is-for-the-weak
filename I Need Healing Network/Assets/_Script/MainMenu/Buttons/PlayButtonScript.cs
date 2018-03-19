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
        GameObject menu = GameObject.Find("Canvas");
        _GameManager.Instance.StartGame();
        menu.SetActive(false);
    }
}
