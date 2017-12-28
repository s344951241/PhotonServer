using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhotonEngine : MonoBehaviour,IPhotonPeerListener {

    private PhotonEngine _instance;

    private static PhotonPeer _peer;

    public static PhotonPeer Peer { get { return _peer; } }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (_instance != this)
            {
                GameObject.Destroy(this);
                return;
            }
        }

    }
    // Use this for initialization
    void Start () {
        _peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        _peer.Connect("127.0.0.1:5055", "SLGame");

    }
	
	// Update is called once per frame
	void Update () {
        if (_peer != null)
        {
            _peer.Service();
        }  
	}
    private void OnDestroy()
    {
        if (_peer != null && _peer.PeerState == PeerStateValue.Connected)
        {
            _peer.Disconnect();
        }
    }
    public void DebugReturn(DebugLevel level, string message)
    {
       // throw new NotImplementedException();
    }

    public void OnEvent(EventData eventData)
    {
        switch (eventData.Code)
        {
            case 1:
                Debug.Log(eventData.Parameters[1]);
                break;
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        switch (operationResponse.OperationCode)
        {
            case 1:
                Debug.Log("收到服务器端返回1" + operationResponse.Parameters[1]+operationResponse.Parameters[2]);
                break;
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log(statusCode);
    }

}
