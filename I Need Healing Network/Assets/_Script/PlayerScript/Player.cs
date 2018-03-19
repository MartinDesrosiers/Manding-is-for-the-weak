using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {
    Rigidbody rg;
    Vector3 playerNetworkVel;
    float lastUpdateTime;
    private void Start()
    {
        rg = transform.GetComponent<Rigidbody>();
        playerNetworkVel = new Vector3();
    }
    // Update is called once per frame
    void Update () {
        if (!photonView.isMine)
        {
            UpdateNetworkPlayerPos();
        }
	}
    void UpdateNetworkPlayerPos()
    {
        rg.velocity = playerNetworkVel;
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(rg.velocity);
        }
        else
        {
            playerNetworkVel = (Vector3)stream.ReceiveNext();
            lastUpdateTime = (float)PhotonNetwork.time;
        }

    }
}
