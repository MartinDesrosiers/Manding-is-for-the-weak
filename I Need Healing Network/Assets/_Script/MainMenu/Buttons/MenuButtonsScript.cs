using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsScript : Buttons {
    protected override void Start()
    {
        base.Start();
    }
    public void CreateAndConnectToARoom()
    {
        string roomNameEntered;
        //Set interactable to false to avoid player to click on the button again while connecting;
        if (!PhotonNetwork.insideLobby)
        {
            Debug.Log("not inside lobby");
            return;
        }
        //verifie if the name is already taken. if yes, choose another.
        //Can't create without a name
        roomNameEntered = mainMenu.GetInputEntered();
        if (roomNameEntered != "")
        {
            TriggerMainMenuButtons(false);
            if (!LookIfNameIsAlreadyTaken(roomNameEntered))
                NetWorkManager.Instance.ConnectToNetwork(roomNameEntered);
            else
            {
                //If name already taken, show message for a certain time;
                StartCoroutine(mainMenu.NameTakenTimer());
                TriggerMainMenuButtons(true);
            }
        }
    }
    bool LookIfNameIsAlreadyTaken(string name)
    {
        //Get every name of the rooms available and compare it;
        foreach (RoomInfo info in PhotonNetwork.GetRoomList())
        {
            if (name == info.Name)
                return true;
        }
        return false;
    }

    public void TryJoinRandomRoom()
    {
        if (!PhotonNetwork.insideLobby)
        {
            Debug.Log("not connected");
            return;
        }
        PhotonNetwork.JoinRandomRoom();
        StartCoroutine(NetWorkManager.Instance.WaitUntilJoinedRoom());
    }

    public void JoinRoom()
    {
        mainMenu.GetRoomInfo.gameObject.SetActive(true);
        mainMenu.GetMenu.gameObject.SetActive(false);
        mainMenu.GetRoomInfo.GetComponent<ListOfRoomsScript>().ShowListOfRoom();
    }
}
