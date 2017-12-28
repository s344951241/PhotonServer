using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Dictionary<byte, object> dic = new Dictionary<byte, object>();

            dic.Add(1, 100);
            dic.Add(2, "客户端");

            PhotonEngine.Peer.OpCustom(1, dic, true);
        }
	}
}
