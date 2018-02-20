using UnityEngine;
using UnityEngine.UI;

public class JoinGameButton : MonoBehaviour {
    
    public void JoinRoom()
    {
        NetWorkManager.Instance.ConnectToNetwork(transform.GetChild(0).GetComponent<Text>().text);
    }
}
