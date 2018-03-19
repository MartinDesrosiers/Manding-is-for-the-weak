using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitRoomButton : MonoBehaviour {

	public void QuitRoom()
    {
        NetWorkManager.Instance.Disconnect();
    }
}
