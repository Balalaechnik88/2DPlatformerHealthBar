using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _authorsPanel;

    [Header("Scene")]
    [SerializeField] private string _gameSceneName = "Game";

    private void Awake()
    {
        if (_authorsPanel != null)
            _authorsPanel.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void OpenAuthors()
    {
        if (_authorsPanel != null)
            _authorsPanel.SetActive(true);
    }

    public void CloseAuthors()
    {
        if (_authorsPanel != null)
            _authorsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
