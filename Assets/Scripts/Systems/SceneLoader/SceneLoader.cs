using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] List<Image> _loadCelles;
    [SerializeField] private int sceneIndexToLoad;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync(sceneIndexToLoad));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // Забезпечити, що сцена завантажується в фоновому режимі
        operation.allowSceneActivation = false;

        int totalCells = _loadCelles.Count;

        while (!operation.isDone)
        {
            if (_loadCelles != null)
            {
                // Отримати поточний прогрес у відсотках
                float progress = Mathf.Clamp01(operation.progress / 0.9f);

                // Обчислити кількість клітинок, які потрібно активувати
                int cellsToActivate = Mathf.CeilToInt(progress * totalCells);

                for (int i = 0; i < totalCells; i++)
                {
                    _loadCelles[i].gameObject.SetActive(i < cellsToActivate);
                }
            }

            // Перевіряємо, чи завантаження завершено
            if (operation.progress >= 0.9f)
            {
                // Після завершення завантаження активуємо сцену
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
