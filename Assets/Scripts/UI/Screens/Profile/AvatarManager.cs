using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Screens.Variables
{
    [Serializable]
    public class AvatarManager
    {
        [SerializeField] private RawImage _playerAvatar;
        [SerializeField] private RawImage _playerAvatarMain;

        private int maxSize = 1000;

        public void SetSavedPicture()
        {
            if (SaveManager.IsSaved(SaveKeys.PlayerAvatarPath))
            {
                string path = SaveManager.LoadString(SaveKeys.PlayerAvatarPath);
                _playerAvatar.texture = NativeCamera.LoadImageAtPath(path, maxSize);
                _playerAvatarMain.texture = NativeCamera.LoadImageAtPath(path, maxSize);
            }
        }
        public void SetPlayerAvatarLeaders()
        {
            if (SaveManager.IsSaved(SaveKeys.PlayerAvatarPath))
            {
                string path = SaveManager.LoadString(SaveKeys.PlayerAvatarPath);
                _playerAvatar.texture = NativeCamera.LoadImageAtPath(path, maxSize);
            }
        }
        public void TakePicture()
        {
            if (NativeCamera.IsCameraBusy())
                return;

            NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
            {
                Debug.Log("Image path: " + path);
                if (path != null)
                {
                    // Сохраняем путь к выбранному изображению в PlayerPrefs
                    SaveManager.SaveString(SaveKeys.PlayerAvatarPath, path);

                    // Create a Texture2D from the captured image
                    Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize);
                    if (texture == null)
                    {
                        Debug.Log("Couldn't load texture from " + path);
                        return;
                    }
                    _playerAvatar.texture = texture;
                    _playerAvatarMain.texture = texture;
                }
            }, maxSize);

            Debug.Log("Permission result: " + permission);
        }
    }

}