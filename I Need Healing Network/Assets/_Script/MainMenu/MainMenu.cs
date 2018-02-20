using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Photon.PunBehaviour {
    public Button roomInfoButton;
    GameObject menu;
    GameObject onlineMenu;
    GameObject connectingScreen;
    GameObject unableToConnect;
    GameObject roomInfoScreen;
    RoomInfo[] roomInfo;
    Button[] button;
    Button joinGameButton;
    Button joinButton;
    Button createButton;
    InputField inputField;
    InputField inputName;
    Text takenText;
    int page;
    int limitPerPage;
    int nbrOfRooms;
	// Use this for initialization
	void Awake () {
        nbrOfRooms = 0;
        page = 1;
        limitPerPage = 4;
        button = new Button[4];
        menu = transform.GetChild(0).GetChild(0).gameObject;
        connectingScreen = transform.GetChild(0).GetChild(1).gameObject;
        unableToConnect = transform.GetChild(0).GetChild(2).gameObject;
        roomInfoScreen = transform.GetChild(0).GetChild(3).gameObject;
        joinGameButton = ObjectFinders.FindAnyChild<Button>(transform, "RandomGameButton");
        joinButton = ObjectFinders.FindAnyChild<Button>(transform, "JoinButton");
        createButton = ObjectFinders.FindAnyChild<Button>(transform, "CreateButton");
        inputField = ObjectFinders.FindAnyChild<InputField>(transform, "RoomNameField");
        takenText = inputField.transform.GetChild(1).GetComponent<Text>();
    }
    private void Start()
    {
        StartCoroutine(WaitingToConnect());
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
    public void TriggerMainMenuButtons(bool active)
    {
        joinGameButton.interactable = active;
        joinButton.interactable = active;
        createButton.interactable = active;
    }
    public string GetInputEntered()
    {
        return inputField.text.ToString();
    }
    public void QuitListOfRoom()
    {
        Debug.Log(button.Length);
        for(int i = 0; i < button.Length; i++)
        {
            Destroy(button[i].gameObject);
        }
        roomInfoScreen.SetActive(false);
        menu.gameObject.SetActive(true);
        TriggerMainMenuButtons(true);
    }
    public void ChangePage(int nextOrPrevious)
    {
        if (page + nextOrPrevious == 0 || page * limitPerPage >= roomInfo.Length)
            return;
            page += nextOrPrevious;
    }
    public void ShowListOfRoom()
    {
        menu.gameObject.SetActive(false);
        roomInfoScreen.gameObject.SetActive(true);
        roomInfo = PhotonNetwork.GetRoomList();
        int limit = page * limitPerPage;
        for (int i = limit - limitPerPage; i < limit; i++)
        {
            if (i >= roomInfo.Length)
            {
                Debug.Log(roomInfo.Length);
                return;
            }
            button[i] = Instantiate(roomInfoButton, 
                                 roomInfoScreen.transform.GetChild(1).GetChild(i).position,
                                 Quaternion.identity, 
                                 transform) ;
            button[i].transform.GetChild(0).GetComponent<Text>().text = roomInfo[i].Name;
            button[i].transform.GetChild(1).GetComponent<Text>().text = roomInfo[i].PlayerCount.ToString();
        }
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
