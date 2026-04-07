using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _authorsPanel;

    [Header("Scene")]
    [SerializeField] private string _gameSceneName = "Game";

    private void Awake()
    {
        if (_authorsPanel == null)
        {
            Debug.LogError($"[{nameof(MenuUI)}] Не назначен AuthorsPanel. Скрипт отключён.", this);
            enabled = false;
            return;
        }

        _authorsPanel.gameObject.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void OpenAuthors()
    {
        _authorsPanel.gameObject.SetActive(true);
    }

    public void CloseAuthors()
    {
        _authorsPanel.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}