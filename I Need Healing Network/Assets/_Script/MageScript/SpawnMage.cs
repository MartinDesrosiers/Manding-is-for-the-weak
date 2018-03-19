using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMage : Photon.PunBehaviour {
    [SerializeField]
    GameObject magePrefab;
    GameObject mage;
    GameObject player;
    GameObject spawningPoint;
    Vector3 position;
    // Use this for initialization
    void Start()
    {
        position = new Vector3();
        mage = Instantiate(magePrefab, transform.position,Quaternion.identity);
        spawningPoint = mage.transform.GetChild(1).gameObject;
        position = SpawnPlace();
        PhotonNetwork.Instantiate("PlayerMain", position, Quaternion.identity, 0);
    }

    Vector3 SpawnPlace()
    {
        return spawningPoint.transform.GetChild(PhotonNetwork.playerList.Length - 1).transform.position;
    }
}
