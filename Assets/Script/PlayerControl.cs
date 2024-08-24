using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class PlayerControl : UnityEngine.MonoBehaviour
{
    public float speed = 10f;
    PhotonView  photonview;
    SpriteRenderer render;
    public AgoraChat chat;
    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        render = GetComponent<SpriteRenderer>();
         if(photonview.isMine==false){
            render.enabled = false;
           }
        chat = GameObject.Find("AgoraControl").GetComponent<AgoraChat>();  

    }

    // Update is called once per frame
    void Update()
    {
     if(photonview.isMine){  
    transform.position += new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0)*Time.deltaTime*speed;
     }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            PhotonView otherPhotonView = other.GetComponent<PhotonView>();
            if (otherPhotonView != null && otherPhotonView.isMine)
            {
                // Show this player for the other player
                photonview.RPC("ShowPlayer", PhotonTargets.AllBuffered, photonview.viewID);
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {   
        if (other.CompareTag("Player"))
        {
            PhotonView otherPhotonView = other.GetComponent<PhotonView>();
            if (otherPhotonView != null && otherPhotonView.isMine)
            {
                // Show this player for the other player
                photonview.RPC("HidePlayer", PhotonTargets.AllBuffered, photonview.viewID);
            }
        }  
    }


    [PunRPC]
    void HidePlayer(int otherPlayerViewID)
    {
        Debug.Log(otherPlayerViewID+ " hide"+photonview.viewID);
        if (photonview.viewID == otherPlayerViewID)
        {     
            chat.Leave();   
          render.enabled = false;   
        }
    }

    [PunRPC]
    void ShowPlayer(int otherPlayerViewID)
    {
        Debug.Log((photonview.viewID == otherPlayerViewID) +" show");
        if (photonview.viewID == otherPlayerViewID)
        {
            chat.Join();
             render.enabled = true;
            
        }
    }
}
