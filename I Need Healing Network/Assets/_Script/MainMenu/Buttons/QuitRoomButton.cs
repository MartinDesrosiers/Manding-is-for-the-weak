using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitRoomButton : MonoBehaviour {

	public void QuitRoom()
    {
        _GameManager.Instance.QuitGame();
        NetWorkManager.Instance.Disconnect();
    }
}
