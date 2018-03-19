using UnityEngine;
using UnityEngine.UI;

public class ListOfRoomsScript : Buttons {

    const string ROOMINFOBUTTON = "_Prefabs/RoomInfoField";
    RoomInfo[] roomInfo;
    Button[] button;
    int page;
    int limitPerPage;
    protected override void Start()
    {
        page = 1;
        limitPerPage = 4;
        button = new Button[4];
        base.Start();
    }
    private void OnEnable()
    {
        ShowListOfRoom();
    }
    //Destroy buttons when not needed
    public void QuitListOfRoom()
    {
        DestroyButtonsList();
        mainMenu.GetRoomInfo.SetActive(false);
        mainMenu.GetMenu.gameObject.SetActive(true);
        TriggerMainMenuButtons(true);
    }
    void DestroyButtonsList()
    {
        if (button.Length < 1)
            return;
        for (int i = 0; i < button.Length; i++)
        {
            if (button[i] != null)
                Destroy(button[i].gameObject);
        }
    }
    //Get a list of room, max 4, and show them.
    public void ShowListOfRoom()
    {
        roomInfo = PhotonNetwork.GetRoomList();
        int limit = page * limitPerPage;
        DestroyButtonsList();
        for (int i = limit - limitPerPage; i < limit; i++)
        {
            if (i >= roomInfo.Length)
            {
                Debug.Log(roomInfo.Length);
                return;
            }
            //Create and set info on each button
            button[i] = Instantiate(Resources.Load(ROOMINFOBUTTON, typeof(Button)) as Button,
                                 mainMenu.GetRoomInfo.transform.GetChild(1).GetChild(i).position,
                                 Quaternion.identity,
                                 transform);
            button[i].transform.GetChild(0).GetComponent<Text>().text = roomInfo[i].Name;
            button[i].transform.GetChild(1).GetComponent<Text>().text = roomInfo[i].PlayerCount.ToString();
        }
    }
    public void ChangePage(int nextOrPrevious)
    {
        if (page + nextOrPrevious == 0 || page * limitPerPage >= roomInfo.Length)
            return;
        page += nextOrPrevious;
        ShowListOfRoom();
    }
}
