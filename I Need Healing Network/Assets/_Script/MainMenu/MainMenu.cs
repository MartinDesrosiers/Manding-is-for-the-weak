using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Photon.PunBehaviour {
    GameObject menu;
    GameObject onlineMenu;
    GameObject connectingScreen;
    GameObject unableToConnect;
    GameObject roomInfoScreen;
    InputField inputField;
    Text takenText;
    int nbrOfRooms;
    #region GetSet
    public GameObject GetRoomInfo { get { return roomInfoScreen; } }
    public GameObject GetMenu { get { return menu; } }
#endregion
    // Use this for initialization
    public void Awake () {
        nbrOfRooms = 0;
        menu = transform.GetChild(0).GetChild(0).gameObject;
        connectingScreen = transform.GetChild(0).GetChild(1).gameObject;
        unableToConnect = transform.GetChild(0).GetChild(2).gameObject;
        roomInfoScreen = transform.GetChild(0).GetChild(3).gameObject;
        inputField = ObjectFinders.FindAnyChild<InputField>(transform, "RoomNameField");
        takenText = inputField.transform.GetChild(1).GetComponent<Text>();
    }
    private void Start()
    {
        //Wait to do anything until connected to Photon
        if (!PhotonNetwork.connected)
        {
            StartCoroutine(WaitingToConnect());
        }
    }
    private void LateUpdate()
    {
        if(nbrOfRooms != PhotonNetwork.countOfRooms)
        {
            Debug.Log("nbrOfRoom = " + nbrOfRooms + "   =>    PhotonNetwork.countOfRooms = " + PhotonNetwork.countOfRooms);
        }
        if (PhotonNetwork.inRoom)
        {
            Debug.Log("inRoom");
        }
        else
        {
            Debug.Log("not In Room");
        }
    }
    public string GetInputEntered()
    {
        return inputField.text.ToString();
    }
    public void CancelButton()
    {
        UnableToConnectWindow(false);
    }
    public void RetryButton()
    {
        UnableToConnectWindow(false);
        NetWorkManager.Instance.ConnectToPhotonServer();
    }
    public void UnableToConnectWindow(bool b)
    {
        unableToConnect.SetActive(b);
    }
    #region Coroutine
    public IEnumerator WaitingToConnect()
    {
        Debug.Log("wait to connect");
        connectingScreen.SetActive(true);
        while (PhotonNetwork.connecting)
        {
            yield return new WaitForSeconds(0.1f);
        }
        connectingScreen.SetActive(false);
    }
    public IEnumerator NameTakenTimer()
    {
        float timeLimit = 2f;
        float time = 0f;
        takenText.enabled = true;
        while (time < timeLimit)
        {
            time += time + Time.fixedUnscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
        takenText.enabled = false;
    }
    public IEnumerator FailedToConnect(string str)
    {
        float timeLimit = 2f;
        float time = 0f;
        connectingScreen.SetActive(true);
        connectingScreen.transform.GetChild(1).GetComponent<Text>().text = str;
        while (time < timeLimit)
        {
            time += time + Time.fixedUnscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
        connectingScreen.SetActive(false);
    }
    #endregion
}
