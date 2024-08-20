
using Assets.Scripts.Game.Achievements;
using Assets.Scripts.Systems.Events;
using Assets.Scripts.UI.Screens.Variables;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : BaseScreen
{
    [SerializeField] private GameObject _editPopup;
    //[SerializeField] private ApiManager _apiManager;
    [SerializeField] private TMP_InputField _nameTextInputField;
    [SerializeField] private Button _photoButton;
    [SerializeField] private AvatarManager _avatarManager;

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _closeEditorButton;

    [Header("Achievements")]
    [SerializeField] private AchievementTypes[] _achievementTypes;
    [SerializeField] private Image[] _achievements;
    [SerializeField] private Sprite[] _receivedAchievements;

    private int maxSize = 1000;

    void OnDestroy()
    {
        Unsubscribe();
    }
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        if (!SaveManager.IsSaved(SaveKeys.PlayerName))
        {
            SaveManager.SaveString(SaveKeys.PlayerName, "UserName1");
        }
        Subscribe();

    }
    public override void Show()
    {
        base.Show();
        SetAchievements();
        SetName();
        SetAvatarIcon();
    }
    public override void Hide()
    {
        SaveNewName();
        base.Hide();

    }
    private void Subscribe()
    {
        AchievementEvents.OnNewAchieve += SetAchievements;
        _photoButton.onClick.AddListener(_avatarManager.TakePicture);
        _editButton.onClick.AddListener(EditProfile);
        _closeEditorButton.onClick.AddListener(EditProfile);
    }
    private void Unsubscribe()
    {
        AchievementEvents.OnNewAchieve -= SetAchievements;
        _photoButton.onClick.RemoveListener(_avatarManager.TakePicture);
        _editButton.onClick.RemoveListener(EditProfile);
        _closeEditorButton.onClick.RemoveListener(EditProfile);
    }
    private void EditProfile()
    {
        if (_editPopup.activeInHierarchy)
        {
            _editPopup.SetActive(false);
        }
        else
        {
            _editPopup.SetActive(true);
        }
    }
    private void SetAchievements()
    {
        if (SaveManager.IsSaved(SaveKeys.Achievements))
        {
            List<AchievementTypes> recewedAchievements = SaveManager.LoadAchievementList(SaveKeys.Achievements);
            for (int i = 0; i < recewedAchievements.Count; i++)
            {
                for (int j = 0; j < _achievementTypes.Length; j++)
                {
                    if (recewedAchievements[i] == _achievementTypes[j])
                    {
                        _achievements[j].sprite = _receivedAchievements[j];
                    }
                }
            }
        }
    }
    private void SetAvatarIcon()
    {
        _avatarManager.SetSavedPicture();
    }
    private void SaveNewName()
    {
        SaveManager.SaveString(SaveKeys.PlayerName, _nameTextInputField.text);
      //  _apiManager.ChangeName(SaveManager.LoadString(SaveKeys.PlayerName));
    }

    private void SetName()
    {
        if (SaveManager.HasKey(SaveKeys.PlayerName))
        {
            _nameTextInputField.text = SaveManager.LoadString(SaveKeys.PlayerName);
            _nameText.text = SaveManager.LoadString(SaveKeys.PlayerName);
        }
    }


}
