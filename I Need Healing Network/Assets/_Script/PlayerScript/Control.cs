using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : Player
{
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        if (!pv.isMine)
            return;
        rg.velocity = Vector3.zero;
        if (Input.GetKey("a") && !Input.GetKey("d")) { 
            rg.velocity = Vector3.left * 5;
            transform.localScale = new Vector3(-1, 1, -1);
        }
        if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            rg.velocity = Vector3.right * 5;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKey("w") && !Input.GetKey("s"))
            rg.velocity = Vector3.forward * 5;
        if (Input.GetKey("s") && !Input.GetKey("w"))
            rg.velocity = Vector3.back * 5;
        if (Input.GetKeyDown("space") && !isAttacking)
        {
            SetIsAttacking(true);
            pv.RPC("SetIsAttacking", PhotonTargets.Others, true);
        }
    }
    [PunRPC]
    protected void SetIsAttacking(bool b)
    {
        isAttacking = b;
        slash = b;
        swordCollider.enabled = true;
    }
    public override void GetHurt()
    {
        base.GetHurt();
    }
}
