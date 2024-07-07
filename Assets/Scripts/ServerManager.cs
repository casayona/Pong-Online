using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPunCallbacks
{
    PhotonView pw;
    void Start()
    {
        pw = GetComponent<PhotonView>();
        PhotonNetwork.ConnectUsingSettings(); // Photon aða baðlan
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye giriþildi");
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 2,
            IsOpen = true,
            IsVisible = true
        };
        PhotonNetwork.JoinOrCreateRoom("TEST", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join the room. Creating a new room.");
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 2,
            IsOpen = true,
            IsVisible = true
        };
        PhotonNetwork.CreateRoom("NewRoom_" + Random.Range(0, 10000), roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        GameObject yeni_oyuncu = PhotonNetwork.Instantiate("Tahta", Vector3.zero, Quaternion.identity, 0);
        yeni_oyuncu.GetComponent<PhotonView>().Owner.NickName = Random.Range(1, 100) + ("Misafir");
        MenuManager.instance.OpenMenu("title");
        Debug.Log("Joined a room!");
    }
}
