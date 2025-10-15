using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    [SerializeField] private GameObject _watchAdPanel;      
    [SerializeField] private GameObject _gameOverPanel;     
    [SerializeField] private Button _closeAdButton;         

    [SerializeField] private VideoPlayer _videoPlayer;      
    [SerializeField] private VideoClip[] _adClips;          

    [SerializeField] private PlayerController _playerController; 
    [SerializeField] private string _catLayerName = "Cat";

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

    public void WatchAD()
    {
        if (_hasWatchedAd || _isPlayingAd )
            return; 

        _isPlayingAd = true;

        _watchAdPanel.SetActive(true);
        _gameOverPanel.SetActive(false);
        _closeAdButton.gameObject.SetActive(false);

        AudioManager.instance.PlaySFX("click");

        if (_playerController != null)
            _playerController.enabled = false;

        if (_adClips != null && _adClips.Length > 0)
        {
            int randomIndex = Random.Range(0, _adClips.Length);
            _videoPlayer.clip = _adClips[randomIndex];
        }

        _videoPlayer.loopPointReached += OnVideoFinished;

        _videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        _closeAdButton.gameObject.SetActive(true);
        _videoPlayer.loopPointReached -= OnVideoFinished;
    }

    public void CloseAD()
    {
        
            _watchAdPanel.SetActive(false);
            _gameOverPanel.SetActive(false);
            _isPlayingAd = false;
            _hasWatchedAd = true;

            if (_playerController != null)
                _playerController.enabled = true;

            _videoPlayer.Stop();
        AudioManager.instance.PlaySFX("click");
        //Cat layer as int
        int catLayer = LayerMask.NameToLayer(_catLayerName);
        //Find All Objects in Cat layer
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        //foreach object in cat layer, destroy it
        foreach (GameObject obj in allObjects)
        {
            //if object is in cat layer
            if (obj.layer == catLayer)
            {
                //destroy cat :(
                Destroy(obj);
            }
        }
        //GameManager.instance.RestartGame();
        
    }
}
