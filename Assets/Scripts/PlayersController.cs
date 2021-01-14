
using UnityEngine;
using Photon.Pun;

public class GameContoller : MonoBehaviourPun
{
    //public Transform[] SpawnPoint=null;
    private void Awake(){
        int i = PhotonNetwork.CurrentRoom.PlayerCount-1;

       // PhotonNetwork.Instantiate("Cube",SpawnPoint[i].position,SpawnPoint[i].rotation);
       if(i==0){
       // PhotonNetwork.Instantiate("Canvas",new Vector3(485, 122, 0), Quaternion.identity);
        }
    
    }
  
}
