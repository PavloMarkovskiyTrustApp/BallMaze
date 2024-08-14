
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Screens.MainMenu.Leaderboard
{
    public class LeaderBoardPosition : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _posInListText;

        public void Init(int Score, string name, int posInList)
        {
            _scoreText.text = Score.ToString();
            _nameText.text = name;
            _posInListText.text = posInList.ToString();
        }
    }
}