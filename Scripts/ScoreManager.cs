using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public Text player1Label;
    public Text player2Label;
    public Text player3Label;
    public Text player4Label;
    public Button ExitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        if (playerCount >= 1) { this.player1Label.text = players[0].NickName + " : " + (int)players[0].CustomProperties["score"]; this.player1Label.color = LobbyManager.getColor((string)players[0].CustomProperties["playerColor"]); }
        if (playerCount >= 2) { this.player2Label.text = players[1].NickName + " : " + (int)players[1].CustomProperties["score"]; this.player2Label.color = LobbyManager.getColor((string)players[1].CustomProperties["playerColor"]); }
        if (playerCount >= 3) { this.player3Label.text = players[2].NickName + " : " + (int)players[2].CustomProperties["score"]; this.player3Label.color = LobbyManager.getColor((string)players[2].CustomProperties["playerColor"]); }
        if (playerCount >= 4) { this.player4Label.text = players[3].NickName + " : " + (int)players[3].CustomProperties["score"]; this.player4Label.color = LobbyManager.getColor((string)players[3].CustomProperties["playerColor"]); }
    }

    public void onButtonExit()
    {
        Debug.Log("Button clicked !");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LocalPlayer.CustomProperties = new ExitGames.Client.Photon.Hashtable();
        Debug.Log("Room left!");
        SceneManager.LoadScene("Lobby");

    }
}
