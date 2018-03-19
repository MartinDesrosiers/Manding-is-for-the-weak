using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {
    protected Rigidbody rg;
    protected PhotonView pv;
    GameObject sword;
    Vector3 playerNetworkVel;
    BoxCollider boxCol;
    float lastUpdateTime;
    protected bool isAttacking;
    protected bool slash;
    private void Start()
    {
        sword = transform.GetChild(1).gameObject;
        boxCol = sword.transform.GetChild(0).GetComponent<BoxCollider>();
        pv = GetComponent<PhotonView>();
        rg = transform.GetComponent<Rigidbody>();
        playerNetworkVel = new Vector3();
        isAttacking = false;
        slash = false;
    }
    // Update is called once per frame
    void Update () {
        if (!photonView.isMine)
        {
            pv.RPC("UpdateNetworkPlayerPos", PhotonTargets.All, null);
        }
	}
    private void FixedUpdate()
    {
        if (isAttacking)
            BasicAttack();
    }
    [PunRPC]
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
    protected void BasicAttack()
    {
        if (transform.GetChild(1).localEulerAngles.y < 90 && slash)
        {
            float yRot = 0;
            yRot += 90 * 3 * Time.deltaTime;
            transform.GetChild(1).Rotate(new Vector3(0f, yRot, 0f));
            return;
        }
        else if (transform.GetChild(1).localEulerAngles.y < 345)
        {
            float yRot = transform.localEulerAngles.y;
            yRot -= 90 * 3 * Time.deltaTime;
            transform.GetChild(1).Rotate(new Vector3(0f, yRot, 0f));
            if (slash)
                slash = false;
            return;
        }
        transform.GetChild(1).localEulerAngles = Vector3.zero;
        boxCol.enabled = false;
        isAttacking = false;
    }
}
