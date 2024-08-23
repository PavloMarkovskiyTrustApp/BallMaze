using Assets.Scripts.Systems.Events;
using Assets.Scripts.UI.Screens.MainMenu.Leaderboard;
using Assets.Scripts.UI.Screens.Variables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : BaseScreen
{
    [SerializeField] private AvatarManager avatarManager;
    [Header("Player")]
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerRank;
    [SerializeField] private TMP_Text _playerScore;
    [Header("AnotherPlayers")]
    [SerializeField] private List<LeaderBoardPosition> _leaders;
    [Header("PlayersInDB")]
    [SerializeField] private PlayerList _playerList;

    [SerializeField] private apiManager _apiManager;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _playerPosPref;

    private int playerPos;

    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
    }
    public override void Show()
    {
        base.Show();
        SetLeaderBoard();
    }
    public override void Hide()
    {
        base.Hide();
        RessetLeaderBoard();
    }


    public void RessetLeaderBoard()
    {
        base.Hide();
        for (int i = 0; i < _leaders.Count; i++)
        {
            GameObject obj = _leaders[i].gameObject;
            Destroy(obj);
        }
        _leaders.Clear();
    }
    private async void SetLeaderBoard()
    {
        _playerList = await _apiManager.GetPlayerList();
        SortPlayersByScore(_playerList);
        SetPlayer();
        SetOtherPlayers();
    }

    private void SortPlayersByScore(PlayerList playerList)
    {
        if (playerList != null && playerList.players != null)
        {
            playerList.players.Sort((x, y) => y.score.CompareTo(x.score));
        }
    }

    private void SetPlayer()
    {
        for (int i = 0; i < _playerList.players.Count; i++)
        {
            if (_playerList.players[i].id == SaveManager.LoadInt(SaveKeys.PlayerID))
            {
                int pos = i;
                pos += 1;
                playerPos = pos;
                _playerRank.text = pos.ToString();
                _playerName.text = SaveManager.LoadString(SaveKeys.PlayerName);
                _playerScore.text = SaveManager.LoadInt(SaveKeys.TotalCoins).ToString();
                avatarManager.SetPlayerAvatarLeaders();
            }
        }

    }
    private void SetOtherPlayers()
    {
        for (int i = 0; i < _playerList.players.Count; i++)
        {
            LeaderBoardPosition player;
            int score = _playerList.players[i].score;
            int pos = i + 1;
            string name = _playerList.players[i].name;
            GameObject obj = Instantiate(_playerPosPref, _content);
            player = obj.GetComponent<LeaderBoardPosition>();
            _leaders.Add(player);
            player.Init(score, name, pos, playerPos);
        }
    }
}
