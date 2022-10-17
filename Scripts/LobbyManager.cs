using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Lobbies objects")]
    public GameObject lobby;
    public GameObject lobby2;
    public GameObject lobby3;

    private GameObject currentLobby;

    public Text debugLabel;

    [Header("Lobby 1 objects")]
    public InputField createRoomInput;
    public InputField joinRoomInput;

    [Header("Lobby 2 objects")]
    public InputField usernameInput;
    public SpriteRenderer userColorLobby2;
    public Dropdown levelInput;
    public GameObject gameLevelRow;

    [Header("Lobby 3 objects")]
    public Text roomNumberLabel;
    public Text levelLabel;
    public Text player1Label;
    public Text player2Label;
    public Text player3Label;
    public Text player4Label;
    public Button startGameButton;

    private System.Tuple<string, Color> localPlayerColor;

    // Start is called before the first frame update
    void Start()
    {
        currentLobby = lobby;
        lobby.SetActive(true);
        lobby2.SetActive(false);
        lobby3.SetActive(false);
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(createRoomInput.text, roomOptions);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created!");
        ChangeLobby(lobby2);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(this.joinRoomInput.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined!");
        List<string> allowedColors = new List<string> { "red", "green", "blue", "yellow", "cyan" };

        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            string playerColor = (string)player.CustomProperties["playerColor"];
            if (allowedColors.Contains(playerColor))
            {
                allowedColors.Remove(playerColor);
            }
        }

        int randomColor = Random.Range(0, allowedColors.Count);
        string randomColorString = allowedColors[randomColor];
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "playerColor", randomColorString } });

        if (randomColorString.Equals("red")) {
            this.localPlayerColor = new System.Tuple<string, Color>("red", Color.red);
            userColorLobby2.color = Color.red;
        }
        else if (randomColorString.Equals("green"))
        {
            this.localPlayerColor = new System.Tuple<string, Color>("green", Color.green);
            userColorLobby2.color = Color.green;
        }
        else if (randomColorString.Equals("blue"))
        {
            this.localPlayerColor = new System.Tuple<string, Color>("blue", Color.blue);
            userColorLobby2.color = Color.blue;
        }
        else if (randomColorString.Equals("cyan"))
        {
            this.localPlayerColor = new System.Tuple<string, Color>("cyan", Color.cyan);
            userColorLobby2.color = Color.cyan;
        }
        else
        {
            this.localPlayerColor = new System.Tuple<string, Color>("yellow", Color.yellow);
            userColorLobby2.color = Color.yellow;
        }

        //line = "";
        //foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        //{
        //    line += " | " + player;
        //}
        //line += " | Nb of players = "+ PhotonNetwork.CurrentRoom.PlayerCount;
        //this.debugLabel.text = line;

        ChangeLobby(lobby2);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Join room failed: " + message); // Do something here
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player other)
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        this.player1Label.text = "";
        this.player2Label.text = "";
        this.player3Label.text = "";
        this.player4Label.text = "";
        if (playerCount >= 1) { this.player1Label.text = players[0].NickName; this.player1Label.color = getColor((string)players[0].CustomProperties["playerColor"]); }
        if (playerCount >= 2) { this.player2Label.text = players[1].NickName; this.player2Label.color = getColor((string)players[1].CustomProperties["playerColor"]); }
        if (playerCount >= 3) { this.player3Label.text = players[2].NickName; this.player3Label.color = getColor((string)players[2].CustomProperties["playerColor"]); }
        if (playerCount >= 4) { this.player4Label.text = players[3].NickName; this.player4Label.color = getColor((string)players[3].CustomProperties["playerColor"]); }

        // For ownerShip
        if (PhotonNetwork.CurrentRoom.MasterClientId == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            if (currentLobby == lobby2)
            {
                this.gameLevelRow.SetActive(true);
            }
            this.startGameButton.gameObject.SetActive(true);
        }
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player player, Hashtable changedProps)
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        if (playerCount >= 1) { this.player1Label.text = players[0].NickName; this.player1Label.color = getColor((string)players[0].CustomProperties["playerColor"]); }
        if (playerCount >= 2) { this.player2Label.text = players[1].NickName; this.player2Label.color = getColor((string)players[1].CustomProperties["playerColor"]); }
        if (playerCount >= 3) { this.player3Label.text = players[2].NickName; this.player3Label.color = getColor((string)players[2].CustomProperties["playerColor"]); }
        if (playerCount >= 4) { this.player4Label.text = players[3].NickName; this.player4Label.color = getColor((string)players[3].CustomProperties["playerColor"]); }
    }

    public override void OnLeftRoom()
    {
        ChangeLobby(lobby);
    }

    public static Color getColor(string colorSrting)
    {
        if (colorSrting.Equals("red"))
        {
            return Color.red;
        }
        else if (colorSrting.Equals("green"))
        {
            return Color.green;
        }
        else if (colorSrting.Equals("blue"))
        {
            return Color.blue;
        }
        else if (colorSrting.Equals("cyan"))
        {
            return Color.cyan;
        }
        else
        {
            return Color.yellow;
        }
    }

    public void ChangeLobby(GameObject newLobby)
        {
            currentLobby.SetActive(false);
            currentLobby = newLobby;
            currentLobby.SetActive(true);
        }

    public void Lobby1CreateRoomButtonClicked()
    {
        // Take the room name
        if (createRoomInput.text != "") //TODO: AND GAME ROOM DONT EXISTS ALREADY
        {
            CreateRoom();

            this.gameLevelRow.gameObject.SetActive(true);
            return;
        }

        //TODO: COULD GIVE A RANDOM NAME or display a message to say its wrong
        return;
    }

    public void Lobby1JoinRoomButtonClicked()
    {
        // Take the room name
        if (joinRoomInput.text != "") //TODO: AND GAME ROOM EXISTS
        {
            // Try to connect to room
            JoinRoom();

            this.gameLevelRow.gameObject.SetActive(false);
        return;
        }

        //TODO: display a message to say its wrong
        return;
    }

    public void Lobby2ReadyButtonClicked()
    {
        if (usernameInput.text != "") // And username is not already taken
        {
            PhotonNetwork.LocalPlayer.NickName = usernameInput.text;
            Hashtable hash = new Hashtable();
            hash.Add("playerName", usernameInput.text);
            hash.Add("playerColor", this.localPlayerColor.Item1);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
            this.player1Label.text = "";
            this.player2Label.text = "";
            this.player3Label.text = "";
            this.player4Label.text = "";
            if (playerCount >= 1) { this.player1Label.text = players[0].NickName; this.player1Label.color = getColor((string)players[0].CustomProperties["playerColor"]); }
            if (playerCount >= 2) { this.player2Label.text = players[1].NickName; this.player2Label.color = getColor((string)players[1].CustomProperties["playerColor"]); }
            if (playerCount >= 3) { this.player3Label.text = players[2].NickName; this.player3Label.color = getColor((string)players[2].CustomProperties["playerColor"]); }
            if (playerCount >= 4) { this.player4Label.text = players[3].NickName; this.player4Label.color = getColor((string)players[3].CustomProperties["playerColor"]); }

            this.roomNumberLabel.text = "GAME ROOM: " + PhotonNetwork.CurrentRoom.Name;

            if (PhotonNetwork.CurrentRoom.MasterClientId == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Hashtable roomProperties = new Hashtable();
                roomProperties.Add("Level", this.levelInput.value + 1);
                PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
                this.startGameButton.gameObject.SetActive(true);
            }
            else
            {
                this.startGameButton.gameObject.SetActive(false);
            }

            this.levelLabel.text = "Level: " + PhotonNetwork.CurrentRoom.CustomProperties["Level"];

            ChangeLobby(lobby3);
        }
    }

    public void Lobby2CancelButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void Lobby3StartGameButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void Lobby3ExitButtonClicked()
    {
        // Do stuff to remove the player from the online game room and put back his color in it

        PhotonNetwork.LeaveRoom();
    }
}
