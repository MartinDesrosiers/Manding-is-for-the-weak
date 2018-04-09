using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _GameManager : Photon.PunBehaviour {
    private static _GameManager m_instance = null;
    public GameObject mainMenu;
    public Canvas ping;
    public GameObject spawnMage;
    MainMenu mainMenuScript;
    object[] objectData;
    GameObject player;
    WaitForSeconds wait;
    // Use this for initialization
    private _GameManager() { }

    public static _GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new _GameManager();
            }
            return m_instance;
        }
    }
    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
        GetComponent<PhotonView>().viewID = 20;
        mainMenuScript = mainMenu.GetComponent<MainMenu>();
        ping = Instantiate(ping, gameObject.transform);
        wait = new WaitForSeconds(1.0f);
        objectData = new object[2];
        Debug.Log(objectData.Length + " : objectData.Length");
    }
    private void Start()
    {
        StartCoroutine(UpdatePing());
    }
    public void SetInstantiationObject()
    {
        objectData[0] = mainMenuScript.GetPlayerNameEntered();
    }
    [PunRPC]
    public void AddPlayer(Vector3 pos)
    {
        player = PhotonNetwork.Instantiate("PlayerMain", pos, Quaternion.identity, 0, objectData);
    }
    public void StartGame(bool b)
    {
        GameObject[] gameobj = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        gameobj = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject obj in gameobj)
        {
            if(obj != null)
                obj.transform.GetComponent<Control>().enabled = b;
        }
    }
    public void QuitGame()
    {
        NetWorkManager.Instance.Disconnect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        GetComponent<PhotonView>().viewID = 25;
    }
    IEnumerator UpdatePing()
    {
        while (true)
        {
            ping.GetComponentInChildren<Text>().text = "Ping : " + PhotonNetwork.GetPing().ToString();
            yield return wait;
        }
    }
}
