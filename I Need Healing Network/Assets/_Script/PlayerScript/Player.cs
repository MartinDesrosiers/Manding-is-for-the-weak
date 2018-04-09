using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterMain
{
    protected Rigidbody rg;
    protected PhotonView pv;
    protected BoxCollider swordCollider;
    GameObject sword;
    int setchild = 0;
    Vector3 playerNetworkVel;
    
    float lastUpdateTime;
    float r;
    float g;
    float b;
    CharacterMain characterMain;
    public PhotonView GetPV
    {
        get { return pv; }
    }
    public ExitGames.Client.Photon.Hashtable GetHash
    {
        get { return NetWorkManager.Instance.PlayerProperty; }
    }
    public int GetId { get { return playerID; } }
protected virtual void Start()
    {
        pv = GetComponent<PhotonView>();
        if (pv.isMine)
        {
            PhotonNetwork.AllocateViewID();
            object[] ohn = photonView.instantiationData;
            string i = ohn[0].ToString();
            Init(Color.red, 100, pv.viewID, i);
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color((float)PhotonNetwork.player.CustomProperties["r"],
                                                                                         (float)PhotonNetwork.player.CustomProperties["g"],
                                                                                         (float)PhotonNetwork.player.CustomProperties["b"]);

        }
        else
        {
            for (int i = 0; i < PhotonNetwork.otherPlayers.Length; i++)
            {
                InitTwo(Color.blue, 100);
                transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }
        sword = transform.GetChild(1).gameObject;
        swordCollider = sword.transform.GetChild(0).GetComponent<BoxCollider>();
        swordCollider.enabled = false;
        rg = transform.GetComponent<Rigidbody>();
        playerNetworkVel = new Vector3();
    }
    // Update is called once per frame
    void Update()
    {
        /*if (!photonView.isMine)
        {
            pv.RPC("UpdateNetworkPlayerPos", PhotonTargets.All, rg, playerNetworkVel);
        }*/
    }
    private void FixedUpdate()
    {
        if (isAttacking)
            BasicAttack(sword);
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
    [PunRPC]
    public virtual void GetHurt()
    {
        Debug.Log(pv.isMine + " : pv.isMine");
        Debug.Log((int)PhotonNetwork.player.CustomProperties["ID"]);
        life -= 10;
        if (life < 1)
            Death();
        lifeText.transform.GetComponent<PhotonView>().RPC("UpdateLifeText", PhotonTargets.All, life);
    }
    void Death()
    {
        gameObject.SetActive(false);
    }
}