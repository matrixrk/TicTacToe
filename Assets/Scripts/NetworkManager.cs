using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using WebSocketSharp;
using System.Linq;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject StartButton;
    public static NetworkManager Instance;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text roomNameText;
    public static NetworkManager instance;
    public Text txtstatus = null;
    public GameObject btnstart = null;
    [SerializeField] TMP_Text errorText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform PlayerListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject PlayerListItemPrefab;

    public object MemoryGameClient { get; private set; }

    void Awake(){
       // StartButton.SetActive(false);
        Instance = this;
    }
   
    void Start() {
        PhotonNetwork.ConnectUsingSettings();
        btnstart.SetActive(false);
        Status("connecting to server");
    }

    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();
        PhotonNetwork.AutomaticallySyncScene = true;
        Status("connected to server:" + PhotonNetwork.ServerAddress);
        btnstart.SetActive(true);
        PhotonNetwork.JoinLobby();


    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("Title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000") +System.Environment.NewLine; ;
        
    }
    
    private void Status(string msg) {
        Debug.Log(msg);
        txtstatus.text = msg;
    }
    public void btnstart_click() {
        string roomName = "test_room";
        Photon.Realtime.RoomOptions opts = new Photon.Realtime.RoomOptions();
        opts.IsOpen = true;
        opts.IsVisible = true;
        opts.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(roomName, opts, Photon.Realtime.TypedLobby.Default);
        btnstart.SetActive(false);
        Status("joining " + roomName);
    }
    public void startGame()
    {
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        foreach (Photon.Realtime.Player player1 in players)
        {
            if (player1.IsLocal & player1.IsMasterClient)
            {
                SceneManager.LoadScene("Game1");
            }


        }
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        Photon.Realtime.RoomOptions opts = new Photon.Realtime.RoomOptions();
        opts.IsOpen = true;
        opts.IsVisible = true;
        opts.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(roomNameInputField.text, opts, Photon.Realtime.TypedLobby.Default);
        MenuManager.Instance.OpenMenu("Loading");
    }
    
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
       
    }
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        foreach(Transform child in PlayerListContent)
        {
            Destroy(child.gameObject); 
        }
        for (int i = 0; i < players.Count(); i++)
        {   
            Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        if (players.Count() == 2)
        {
           // StartButton.SetActive(true);
        }

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        Debug.LogError("Room Creation Failed: " + message);
        MenuManager.Instance.OpenMenu("error");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }
    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
