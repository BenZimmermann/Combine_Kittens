using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int CurrentScore { get; set; }

    [SerializeField] private TextMeshProUGUI[] _scoreText;
    [SerializeField] private Image _gameOverPanel;

    [SerializeField] private PlayerController _playerController; // Spielersteuerung
    [SerializeField] private float _fadeTime = 2f;

    public float TimeTillGameOver = 1f;

    //public bool _hasWatchedAd = false; // Flag für Werbung
    private void OnEnable()
    {
        //SceneManager.sceneLoaded += FadeGameIn;
    }
    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= FadeGameIn;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UpdateScoreText();
    }

    public void IncreaseScore(int amount)
    {
        CurrentScore += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        foreach (var text in _scoreText)
        {
            if (text != null)
                text.text = CurrentScore.ToString("0");
        }
    }
    public void GameOver()
    {
        if (_playerController != null)
            _playerController.enabled = false;
        StartCoroutine(FadeGameOut());

        //RestartGame();
        // StartCoroutine(RestartGame());
    }
   
    public /*IEnumerator*/ void  RestartGame()
    {
        AudioManager.instance.PlaySFX("click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //playerControlls Disablen anstatt Time.timeScale
        if (_playerController != null)
            _playerController.enabled = true;
        UpdateScoreText();
    }
    private void FadeGameOut(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeGameOut());
    }

    private IEnumerator FadeGameOut()
    {
        _gameOverPanel.gameObject.SetActive(true);
        Color startColor = _gameOverPanel.color;
        startColor.a = 1f;
        _gameOverPanel.color = startColor;

        float elapsedTime = 0f;
        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(0f, 1f, (elapsedTime / _fadeTime));
            startColor.a = newAlpha;
            _gameOverPanel.color = startColor;
            yield return null;
        }
        //playerControlls Disablen anstatt Time.timeScale
        if (_playerController != null)
            _playerController.enabled = false;
    }
    //public void WatchAD()
    //{
    //    _watchAdPanel.gameObject.SetActive(true);
    //    //playerControlls Disablen anstatt Time.timeScale
    //    Time.timeScale = 0f;
    //    //wenn webung geschaut wurde, close ad anzeigen
    //    _watchAdButton.interactable = true;
    //}

    //public void CloseAD()
    //{
    //    _watchAdPanel.gameObject.SetActive(false);
    //    _gameOverPanel.gameObject.SetActive(false);

    //    //wenn webung angeschaut wurde, game fortsetzen
    //    //obere reihe löschen damit spiel weitergehen kann
    //    //wenn werbung einmal geschaut wurde, dann nicht nochmal anbieten
    //    _hasWatchedAd = true;
    //    _destroyModeActive = true;
    //    Time.timeScale = 1f;
    //    RestartGame();
    //}

    //liste aus Media
    //wenn watchAD gedrückt wird
    //zufälliges wird ausgwählt
    //fängt an zu spielen
    //wenn werbung zuende ist, close ad button aktivieren
    public void QuitGame()
    {
        AudioManager.instance.PlaySFX("click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }
}
