using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace TimeForChange.Cinematic
{
    public class CinematicBehaviour : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClipToChange;
        [SerializeField] private GameObject _mainVirtualCamera;
        [SerializeField] private GameObject _text;
    
        [SerializeField] private int _sceneIndex;
        [SerializeField] private bool _startInAwake;
    
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            if (_startInAwake)
            {
                StartCoroutine(StartInAwake());
            }
        }

        public void PlayButton(VideoPlayer videoPlayer) => StartCoroutine(PlayButtonIEnumerator(videoPlayer));

        private IEnumerator StartInAwake()
        {
            yield return new WaitWhile(() => _audioSource.isPlaying);
            _mainVirtualCamera.SetActive(false);
            _audioSource.clip = _audioClipToChange;
        
            _audioSource.loop = true;
            _audioSource.Play();
            _text.SetActive(true);
        }

        public void ExitEndButton() => SceneManager.LoadScene(_sceneIndex);

        private IEnumerator PlayButtonIEnumerator(VideoPlayer videoPlayer)
        {
            videoPlayer.Play();
            _audioSource.clip = _audioClipToChange;
            _audioSource.loop = false;
        
            _audioSource.Play();
            _mainVirtualCamera.SetActive(false);
            _text.SetActive(false);
        
            yield return new WaitWhile(() => _audioSource.isPlaying);
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}
        
