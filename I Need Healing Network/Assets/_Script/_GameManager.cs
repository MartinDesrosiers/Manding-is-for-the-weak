using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _GameManager : Photon.PunBehaviour {
    public static _GameManager m_instance;
    public Canvas ping;
    public GameObject spawnMage;
    WaitForSeconds wait;
    // Use this for initialization
    public _GameManager() { }

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
    void Awake () {
        DontDestroyOnLoad(gameObject);
        ping = Instantiate(ping, gameObject.transform);
        wait = new WaitForSeconds(1.0f);
    }
    private void Start()
    {
        StartCoroutine(UpdatePing());
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
