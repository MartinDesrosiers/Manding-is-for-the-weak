using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _GameManager : Photon.PunBehaviour {
    public static _GameManager m_instance;
    public Canvas ping;
    public GameObject spawnMage;
    GameObject player;
    public List<GameObject> players;
    WaitForSeconds wait;
    // Use this for initialization
    public _GameManager() { }

    public static _GameManager Instance
    {
        get
        {
            return m_instance;
        }
    }
    void Awake ()
    {
        GetComponent<PhotonView>().viewID = 21;
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("yo");
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        GetComponent<PhotonView>().viewID = 20;
        DontDestroyOnLoad(gameObject);
        ping = Instantiate(ping, gameObject.transform);
        players = new List<GameObject>();
        wait = new WaitForSeconds(1.0f);
    }
    private void Start()
    {
        StartCoroutine(UpdatePing());
    }
    public void AddPlayer(Vector3 pos)
    {
        Vector3 position = pos;
        player = PhotonNetwork.Instantiate("PlayerMain", position, Quaternion.identity, 0);
    }
    public void StartGame()
    {
        //player.transform.GetComponent<Control>().enabled = true;
    }
    public void QuitGame()
    {
        players.Clear();
        GetComponent<PhotonView>().viewID = 21;
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
