
using Assets.Scripts.Game.Achievements;
using Assets.Scripts.Systems.Events;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : BaseScreen
{
    [SerializeField] private RotateWheel _rotateWheel;
    [SerializeField] private int[] _bonuses;

    [Header("Buttons")]
    [SerializeField] private Button _getBonus;
    [SerializeField] private Button _thankButton;
    [Header("Panels")]
    [SerializeField] private GameObject _wheelPopup;
    [SerializeField] private GameObject _timePopup;
    [SerializeField] private GameObject _winPopup;
    [Header("Text")]
    [SerializeField] private TMP_Text _bonusTimerText;
    [SerializeField] private TMP_Text _winText;

    [SerializeField] private bool _devMode;


    private const string LastSpinKey = "LastSpinTime";
    private const int HoursToWait = 24;

    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        if (_devMode)
        {
            ResetSpinTime();
        }
        if (CanGet())
        {
            // Разрешить игроку крутить колесо фортуны
            Debug.Log("You can spin the wheel!");
            _getBonus.gameObject.SetActive(true);
            _getBonus.interactable = true;
        }
        Subscribe();
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }
    public override void Show()
    {
        base.Show();
        if (CanGet())
        {
            _wheelPopup.SetActive(true);
            _timePopup.SetActive(false);
            StopCoroutine(UpdateTimer());
        }
        else
        {
            _wheelPopup.SetActive(false);
            _timePopup.SetActive(true);
            TimeSpan timeLeft = GetTimeUntilNextSpin();
            _bonusTimerText.text = $"{timeLeft.Hours}:{timeLeft.Minutes}:{timeLeft.Seconds}";
            StartCoroutine(UpdateTimer());
        }
    }
    private void Subscribe()
    {
        _rotateWheel.OnBonusReceived += BonusReceived;

        _getBonus.onClick.AddListener(GetBonus);
        _thankButton.onClick.AddListener(ThanksPressed);
    }
    private void Unsubscribe()
    {
        _rotateWheel.OnBonusReceived -= BonusReceived;

        _getBonus.onClick.RemoveListener(GetBonus);
        _thankButton.onClick.RemoveListener(ThanksPressed);
    }

    private void ThanksPressed()
    {
        _winPopup.SetActive(false);
        _timePopup.SetActive(true);
        _wheelPopup.SetActive(false);
    }
    private void GetBonus()
    {
        if (CanGet())
        {
            int index = UnityEngine.Random.Range(0, _bonuses.Length);
            int bonus = _bonuses[index];
            _rotateWheel.Rotate(bonus);
            CheckAchievements(bonus);

        }
        else
        {
            Debug.Log("You cannot spin the wheel yet!");
        }
    }

    private void BonusReceived(int bonus)
    {
        _winPopup.SetActive(true);
        _winText.text = $"+{bonus}";

        PlayerPrefs.SetString(LastSpinKey, DateTime.Now.ToString());
        PlayerPrefs.Save();

        StartCoroutine(UpdateTimer());
    }
    private bool CanGet()
    {
        if (PlayerPrefs.HasKey(LastSpinKey))
        {
            DateTime lastSpinTime = DateTime.Parse(PlayerPrefs.GetString(LastSpinKey));
            return DateTime.Now >= lastSpinTime.AddHours(HoursToWait);
        }
        else
        {
            return true; // Если времени последнего вращения нет, значит игрок еще не крутил колесо
        }
    }

    private TimeSpan GetTimeUntilNextSpin()
    {
        if (PlayerPrefs.HasKey(LastSpinKey))
        {
            DateTime lastSpinTime = DateTime.Parse(PlayerPrefs.GetString(LastSpinKey));
            DateTime nextSpinTime = lastSpinTime.AddHours(HoursToWait);
            return nextSpinTime - DateTime.Now;
        }
        else
        {
            return TimeSpan.Zero;
        }
    }

    public void ResetSpinTime()
    {
        PlayerPrefs.DeleteKey(LastSpinKey);
        PlayerPrefs.Save();
    }

    IEnumerator UpdateTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            TimeSpan timeLeft = GetTimeUntilNextSpin();
            _bonusTimerText.text = $"{timeLeft.Hours}:{timeLeft.Minutes}:{timeLeft.Seconds}";
        }
    }

    private void CheckAchievements(int receivedBonus)
    {
        if(receivedBonus == _bonuses[_bonuses.Length-1])
        {
            AchievementEvents.Achieve(AchievementTypes.JackpotWinner);
        }
        if(SaveManager.IsSaved(SaveKeys.TotalBonuses))
        {
            if(SaveManager.LoadInt(SaveKeys.TotalBonuses) == 5)
            {
                AchievementEvents.Achieve(AchievementTypes.BonusSeeker);
            }
            int bonuses = SaveManager.LoadInt(SaveKeys.TotalBonuses);
            bonuses++;
            SaveManager.SaveInt(SaveKeys.TotalBonuses, bonuses);
        }
        else
        {
            SaveManager.SaveInt(SaveKeys.TotalBonuses, 1);
        }
    }
}
