using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoom : Photon.PunBehaviour {

    string name;
    int player;

    public void Init(string str, int ply)
    {
        name = str;
        player = ply;
    }
    public void JoinTheRoom()
    {
        _GameManager.Instance.SetInstantiationObject();
        NetWorkManager.Instance.ConnectToNetwork(name);
    }
}
