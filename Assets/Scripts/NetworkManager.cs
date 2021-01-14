using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    public Text txtstatus= null;
    public GameObject btnstart = null;

    /*void Awake(){

        if(instance != null && instance != this)
            gameObject.SetActive(false);
        else{
        instance=this;
        DontDestroyOnLoad(gameObject);
        }
    }*/
    void Start(){
        PhotonNetwork.ConnectUsingSettings();
        btnstart.SetActive(false);
        Status("connecting to server");
    }

    public override void OnConnectedToMaster(){
        base.OnConnectedToMaster();
        PhotonNetwork.AutomaticallySyncScene=true;
        Status("connected to server:"+ PhotonNetwork.ServerAddress);
        btnstart.SetActive(true);
        //CreateRoom("testroom");
    }

    /*public override void OnCreatedRoom(){
        Debug.Log("connected to room:"+ PhotonNetwork.CurrentRoom.Name);
    }*/
    /*public override void OnPlayerEnteredRoom(Player newPlayer){
        Debug.Log("current players:" +PhotonNetwork.CurrentRoom.PlayerCount);
        if(PhotonNetwork.CurrentRoom.PlayerCount==2){
            
            PhotonNetwork.CurrentRoom.IsOpen=false;
            PhotonNetwork.LoadLevel("Game");
        }
    }*/

   /* public void CreateRoom(string roomName){
        PhotonNetwork.CreateRoom(roomName);

    }*/
    /*public void JoinRoom(string roomName){
        PhotonNetwork.JoinRoom(roomName);
    }*/
    public override void OnJoinedRoom(){
        base. OnJoinedRoom();
        SceneManager.LoadScene("Game1");
    }
    private void Status(string msg){
        Debug.Log(msg);
        txtstatus.text=msg;
    }
    public void btnstart_click(){
        string roomName="test_room";
        Photon.Realtime.RoomOptions opts = new Photon.Realtime.RoomOptions();
        opts.IsOpen=true;
        opts.IsVisible=true;
        opts.MaxPlayers=2;
        PhotonNetwork.JoinOrCreateRoom(roomName,opts,Photon.Realtime.TypedLobby.Default);
        btnstart.SetActive(false);
        Status("joining "+ roomName);
    }
}
