using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class SpawnPlayer : UnityEngine.MonoBehaviour
{
    public Vector2 minDis;
    public Vector2 maxDis;

     void Start()
    {
        Vector2 pos = new Vector2(Random.Range(minDis.x,maxDis.x),Random.Range(minDis.y,maxDis.y));
        PhotonNetwork.Instantiate("Player",pos,Quaternion.identity,0,null);
    }
}
