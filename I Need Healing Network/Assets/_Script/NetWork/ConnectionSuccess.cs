using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionSuccess : Photon.PunBehaviour
{
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnCreatedRoom()
    {
        if (PhotonNetwork.isMasterClient)
            PhotonNetwork.LoadLevel(1);
    }
    public override void OnConnectedToPhoton()
    {
        StartCoroutine(SafetyWait());
    }
    IEnumerator SafetyWait()
    {
        float timer = 0f;
        while(timer < 1f)
        {
            timer += timer + Time.fixedUnscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
        
        //NetWorkManager.Instance.mainMenu.TriggerMainMenuButtons(true);
    }
}

