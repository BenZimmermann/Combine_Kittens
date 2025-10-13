using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    [Header("Panels & UI")]
    [SerializeField] private GameObject _watchAdPanel;      // UI Panel mit Video
    [SerializeField] private GameObject _gameOverPanel;     // Game Over Panel
    [SerializeField] private Button _closeAdButton;         // Schließen-Button nach Ad-Ende

    [Header("Video Settings")]
    [SerializeField] private VideoPlayer _videoPlayer;      // Dein Video Player im UI
    [SerializeField] private VideoClip[] _adClips;          // Liste zufälliger Videos

    [Header("References")]
    [SerializeField] private PlayerController _playerController; // Spielersteuerung

    private bool _hasWatchedAd = false;
    private bool _isPlayingAd = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        _closeAdButton.gameObject.SetActive(false);
        _watchAdPanel.SetActive(false);
        _videoPlayer.Stop();
    }

    /// <summary>
    /// Startet den Ad-Vorgang
    /// </summary>
    public void WatchAD()
    {
        if (_hasWatchedAd || _isPlayingAd )
            return; // Werbung darf nur einmal angesehen werden

        _isPlayingAd = true;

        // Panel aktivieren
        _watchAdPanel.SetActive(true);
        _gameOverPanel.SetActive(false);
        _closeAdButton.gameObject.SetActive(false);

        // Spieler deaktivieren statt Time.timeScale
        if (_playerController != null)
            _playerController.enabled = false;

        // Zufälliges Video wählen
        if (_adClips != null && _adClips.Length > 0)
        {
            int randomIndex = Random.Range(0, _adClips.Length);
            _videoPlayer.clip = _adClips[randomIndex];
        }

        // Video abspielen
        _videoPlayer.loopPointReached += OnVideoFinished;

        // Video starten
        _videoPlayer.Play();
    }

    /// <summary>
    /// Wird aufgerufen, sobald das Video fertig ist
    /// </summary>
    private void OnVideoFinished(VideoPlayer vp)
    {
        _closeAdButton.gameObject.SetActive(true);
        _videoPlayer.loopPointReached -= OnVideoFinished; // Event abmelden
    }

    /// <summary>
    /// Wird aufgerufen, wenn der Spieler die Werbung schließt
    /// </summary>
    public void CloseAD()
    {
        
            _watchAdPanel.SetActive(false);
            _gameOverPanel.SetActive(false);
            _isPlayingAd = false;
            _hasWatchedAd = true;

            // Spielersteuerung wieder aktivieren
            if (_playerController != null)
                _playerController.enabled = true;

            // Video zurücksetzen
            _videoPlayer.Stop();

            // Spiel fortsetzen
       // GameManager.instance._hasWatchedAd = true;
        GameManager.instance.RestartGame();
        
    }
}
