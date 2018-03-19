using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMage : Photon.PunBehaviour {
    [SerializeField]
    GameObject magePrefab;
    GameObject mage;
    GameObject spawningPoint;
    int i = 0;
    // Use this for initialization
    void Start()
    {
        mage = Instantiate(magePrefab, transform.position, Quaternion.identity);
        spawningPoint = mage.transform.GetChild(1).gameObject;
        _GameManager.Instance.AddPlayer(SpawnPlace());
    }

    Vector3 SpawnPlace()
    {
        return spawningPoint.transform.GetChild(PhotonNetwork.playerList.Length - 1).transform.position;
    }
}
