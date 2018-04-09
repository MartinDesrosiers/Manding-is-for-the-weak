using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsScript : MonoBehaviour
{
    bool hasBeenTouched;
    private void Start()
    {
        hasBeenTouched = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player.photonView.isMine && player.GetId != transform.parent.GetComponentInParent<Player>().GetId)
            {
                player.GetHurt();
                hasBeenTouched = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (hasBeenTouched)
                hasBeenTouched = false;
        }
    }
}
