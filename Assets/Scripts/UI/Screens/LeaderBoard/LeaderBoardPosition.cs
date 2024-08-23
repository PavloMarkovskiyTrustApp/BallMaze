
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
        [SerializeField] private GameObject _crownImageGold;
        [SerializeField] private GameObject _crownImageSilver;
        [SerializeField] private GameObject _crownImageBronze;
        [SerializeField] private GameObject _background;
        [SerializeField] private GameObject _botoomLine;

        public void Init(int Score, string name, int posInList,int playerPos)
        {
            _scoreText.text = Score.ToString();
            _nameText.text = name;
            _posInListText.text = posInList.ToString();
            switch(posInList)
            {
                case 1:
                    _crownImageGold.SetActive(true);
                    break;
                case 2:
                    _crownImageSilver.SetActive(true);
                    break;
                case 3:
                    _crownImageBronze.SetActive(true);
                    break;
            }
            if(posInList == (playerPos - 1))
            {
                _botoomLine.SetActive(false);
            }
            if (posInList == playerPos)
            {
                _botoomLine.SetActive(false);
                _background.SetActive(true);
            }
        }
    }
}