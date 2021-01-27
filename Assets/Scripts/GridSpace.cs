using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;

public class GridSpace : MonoBehaviourPun
{
    public Button button;
    public Text buttonText;
    
    public string playerRole=null;
    private GameController1 gameController;
    public void Awake(){
       /* int i = PhotonNetwork.CurrentRoom.PlayerCount-1;
        if(i==0){
            playerRole="X";
        }else{
           playerRole="O" ;
        }*/
        int i=0;
         Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        foreach (Photon.Realtime.Player player1 in players){
            if (player1.IsLocal & i==0)
            {
               playerRole="X";
            }
            if (player1.IsLocal & i==1){
               playerRole="O" ;
                  }
            i++;
        }

    }
    public void SetSpace()
    {
        //gameController.EndTurn();
       if(gameController.GetPlayerSide()==playerRole){
           photonView.RPC("update", RpcTarget.All);
       }
       
        
        
    }
    public void SetGameControllerReference(GameController1 controller)
    {
        gameController = controller;
    }
    [PunRPC]
    void update(){
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }

}
