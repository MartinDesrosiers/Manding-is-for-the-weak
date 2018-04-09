using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMain : Photon.PunBehaviour {

    public bool isAttacking;
    public bool slash;
    public int life;
    public int playerID;
    protected LifeScript lifeText;

    protected void Init(Color color, int lifes, int ID, string name)
    {
        playerID = ID;
        life = lifes;
        ExitGames.Client.Photon.Hashtable PlayerProperty = new ExitGames.Client.Photon.Hashtable();
        PlayerProperty.Add("r", color.r);
        PlayerProperty.Add("g", color.g);
        PlayerProperty.Add("b", color.b);
        PlayerProperty.Add("life", lifes);
        PlayerProperty.Add("ID", ID);
        PlayerProperty.Add("Name", name);
        PhotonNetwork.player.SetCustomProperties(PlayerProperty);
        SetLifeText();
    }
    //Put the player list in the same order for every player. Their name and life are at the same spot in every screen
    protected void SetLifeText()
    {
        PhotonPlayer[] phoPlay = ToolsScript.SortArray(PhotonNetwork.playerList);
        for (int i = 0; i < phoPlay.Length; i++)
        {
            if (phoPlay[i].IsLocal)
            {
                lifeText = GameObject.Find("PlayersLife").transform.GetChild(i).GetComponent<LifeScript>();
                return;
            }
        }
    }
    protected void InitTwo(Color color, int lifes)
    {
        life = lifes;
    }
    private void Start()
    {
        isAttacking = false;
        slash = false;
    }
    protected void BasicAttack(GameObject weapon)
    {
        if (weapon.transform.localEulerAngles.y < 90 && slash)
        {
            float yRot = 0;
            yRot += 90 * 3 * Time.deltaTime;
            weapon.transform.Rotate(new Vector3(0f, yRot, 0f));
            return;
        }
        else if (weapon.transform.localEulerAngles.y < 345)
        {
            float yRot = transform.localEulerAngles.y;
            yRot -= 90 * 3 * Time.deltaTime;
            weapon.transform.Rotate(new Vector3(0f, yRot, 0f));
            if (slash)
                slash = false;
            return;
        }
        weapon.transform.localEulerAngles = Vector3.zero;
        weapon.GetComponentInChildren<BoxCollider>().enabled = false;
        isAttacking = false;
    }
}
