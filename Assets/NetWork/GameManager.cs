using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    public Transform[] spawnPoints; //player swwan pos
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;
        PhotonNetwork.AutomaticallySyncScene = true;
        StartCoroutine(this.Spwan());
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnDisconnected(DisconnectCause cause)
    {

    }
    IEnumerator Spwan()
    {
        int index = Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate("Player", spawnPoints[index].transform.position, spawnPoints[index].transform.rotation);

        yield return null;
    }
}
