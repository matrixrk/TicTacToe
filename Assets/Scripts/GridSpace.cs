using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;

public class GridSpace : MonoBehaviourPun
{
    public Button button;
    public Text buttonText;

    private GameController1 gameController;
    
    public void SetSpace()
    {
       
        //gameController.EndTurn();
        photonView.RPC("update", RpcTarget.All);
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
