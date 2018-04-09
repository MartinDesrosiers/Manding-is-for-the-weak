using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    [PunRPC]
    public void UpdateLifeText(int life)
    {
        transform.GetComponent<UnityEngine.UI.Text>().text = life.ToString();
    }
}
