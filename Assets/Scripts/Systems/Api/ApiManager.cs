using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;

public class apiManager : MonoBehaviour
{
    [SerializeField] private PlayerList _playerList;
    private const string LoginUrl = "https://ball-maze-backend-a84707e0b03a.herokuapp.com/login";
    private const string Username = "admin";
    private const string Password = "9S9aVoV4B8jZ";
    // Get Players
    private const string PlayersUrl = "https://ball-maze-backend-a84707e0b03a.herokuapp.com/api/players";

    // Create Player
    private const string CreatePlayerUrl = "https://ball-maze-backend-a84707e0b03a.herokuapp.com/api/players";
    // Update Player
    private const string UpdatePlayerUrl = "https://ball-maze-backend-a84707e0b03a.herokuapp.com/api/players";

    // Delete Player
    private const string DeletePlayerUrl = "https://ball-maze-backend-a84707e0b03a.herokuapp.com/api/players";
    private string bearerToken;

    private void Start()
    {

        StartCoroutine(Login());

    }
    [System.Serializable]
    public class LoginResponse
    {
        public string token;
    }
    private IEnumerator Login()
    {
        string credentials = $"{Username}:{Password}";
        string base64Credentials = System.Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(LoginUrl, ""))
        {
            request.SetRequestHeader("Authorization", "Basic " + base64Credentials);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Login response: " + request.downloadHandler.text);
                var response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
                bearerToken = response.token;
                if (IsFirstRun())
                {
                    Debug.Log("Это первый запуск игры.");
                    
                    StartCoroutine(CreatePlayer("username", 0, "base"));
                }
                else
                {
                    Debug.Log("Это не первый запуск игры.");

                }
                
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }  
    }
    public void TokenReceived(string token)
    {
        bearerToken = token;
        StartCoroutine(CreatePlayer("username", 0, "base"));
    }
    public async Task<PlayerList> GetPlayerList()
    {
        await GetPlayers();
        return _playerList;
    }

    public void ChangeName(string newName)
    {
        StartCoroutine(UpdatePlayer(SaveManager.LoadInt(SaveKeys.PlayerID), newName, SaveManager.LoadInt(SaveKeys.PlayerPoints), "base"));
    }

    public void ChangeScore(int newScore)
    {
        StartCoroutine(UpdatePlayer(SaveManager.LoadInt(SaveKeys.PlayerID), SaveManager.LoadString(SaveKeys.PlayerName), newScore, "base"));
    }

    private bool IsFirstRun()
    {
        if (!SaveManager.IsSaved(SaveKeys.FirstRunKey))
        {
            SaveManager.SaveInt(SaveKeys.FirstRunKey, 1);
            return true;
        }
        else
        {
            return false;
        }
    }

    private async Task GetPlayers()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(PlayersUrl))
        {
            request.SetRequestHeader("Authorization", "Bearer " + bearerToken);
            var operation = request.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Players response: " + request.downloadHandler.text);
                _playerList = JsonUtility.FromJson<PlayerList>("{\"players\":" + request.downloadHandler.text + "}");
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }

    private IEnumerator CreatePlayer(string name, int score, string imageURL)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);
        form.AddField("imageURL", imageURL);

        using (UnityWebRequest request = UnityWebRequest.Post(CreatePlayerUrl, form))
        {
            request.SetRequestHeader("Authorization", "Bearer " + bearerToken);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Create player response: " + request.downloadHandler.text);
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(request.downloadHandler.text);
                SaveManager.SaveInt(SaveKeys.PlayerID, playerData.id);
                SaveManager.SaveInt(SaveKeys.PlayerPoints, 0);
                Debug.Log("ID: " + playerData.id);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }

    private IEnumerator UpdatePlayer(int playerId, string name, int score, string imageURL)
    {
        string url = $"{UpdatePlayerUrl}/{playerId}";

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            request.SetRequestHeader("Authorization", "Bearer " + bearerToken);
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            // Создаем содержимое запроса
            WWWForm form = new WWWForm();
            form.AddField("name", name);
            form.AddField("score", score.ToString());
            form.AddField("imageURL", imageURL);

            // Устанавливаем содержимое запроса
            request.uploadHandler = new UploadHandlerRaw(form.data);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Update player response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }

    }

    private IEnumerator DeletePlayer(int playerId)
    {
        if (playerId <= 0)
        {
            Debug.LogError("Invalid playerId: " + playerId);
            yield break;
        }

        string url = $"{DeletePlayerUrl}/{playerId}";

        if (string.IsNullOrEmpty(bearerToken))
        {
            Debug.LogError("Bearer token is null or empty.");
            yield break;
        }

        using (UnityWebRequest request = UnityWebRequest.Delete(url))
        {
            request.SetRequestHeader("Authorization", "Bearer " + bearerToken);

            // Log the request URL for debugging purposes
            Debug.Log("Sending DELETE request to: " + url);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Delete player response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }
}
