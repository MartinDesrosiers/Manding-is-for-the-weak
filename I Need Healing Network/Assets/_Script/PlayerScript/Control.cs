using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : Photon.PunBehaviour {
    Rigidbody rg;
	// Use this for initialization
	void Start () {
        if (!photonView.isMine)
            enabled = false;
        rg = transform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rg.velocity = Vector3.zero;
		if(Input.GetKey("a") && !Input.GetKey("d"))
            rg.velocity = Vector3.left * 5;
        if (Input.GetKey("d") && !Input.GetKey("a"))
            rg.velocity = Vector3.right * 5;
        if (Input.GetKey("w") && !Input.GetKey("s"))
            rg.velocity = Vector3.forward * 5;
        if (Input.GetKey("s") && !Input.GetKey("w"))
            rg.velocity = Vector3.back * 5;
    }
}
