using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TimeForChange.Player;
using UnityEngine;

namespace TimeForChange.Cinematic
{
    public class Meteor : MonoBehaviour
    {
        [Header("Meteor settings ")] 
        [SerializeField] private Transform _pointToGo;

        [Header("Disable Components")]
        [SerializeField] private GameObject _disableMainVirtualCam;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private GameObject _canvas;

        [Header("Cut Scene Settings: ")] 
        [SerializeField] private Ease _animation;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioClip _destructionClip;
        [SerializeField] private ParticleSystem _fire, _explosion;
        [SerializeField] private GameObject _lava;
    
        [Header("Destruction Objects: ")] 
        [SerializeField] private List<Destruction.Destruction> _destructions;
        [SerializeField] private GameObject _prefabObject;
        [SerializeField] private AudioSource _musicSource;
    
        [Header("UI")]
        [SerializeField] private GameObject _exitButton;
        
        private MeshRenderer _meshRenderer;
        private AudioSource _audioSource;
        private float _duration;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _duration = _audioClip.length;
        }

        private void OnEnable()
        {
            DestroyElements();
            StartCoroutine(MeteorStart());
        }

        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _audioSource.Play();
        }

        private IEnumerator MeteorStart()
        {
            _musicSource.volume = 0f;
            _disableMainVirtualCam.gameObject.SetActive(true);
            
            _audioSource.PlayOneShot(_audioClip);
            _meshRenderer.enabled = true;
            _fire.Play();
        
            foreach (var des in _destructions)
            {
                des.PrefabObject = _prefabObject;
                des.Force = new Vector3(4.59f, 2.34f, 0.52f);
            }
            
            transform.Rotate(new Vector3(0,10,2));
        
            DOTween.To(() => transform.position, x => transform.position = x, _pointToGo.position, _duration)
                .SetEase(_animation)
                .OnComplete(() => StartCoroutine(GameOver()));

            yield return null;
        }

        private void DestroyElements()
        {
            Destroy(_playerMovement);
            for (int i = 0; i < _canvas.transform.childCount-1; i++) 
                Destroy(_canvas.transform.GetChild(i).gameObject);
        }
    
        private IEnumerator GameOver()
        {
            _audioSource.PlayOneShot(_destructionClip);
            _meshRenderer.enabled = false;
            _fire.Stop();
            _explosion.Play();

            _lava.gameObject.SetActive(true);
            foreach (var destruction in _destructions) 
                destruction.DestructionObject();

            _exitButton.SetActive(true);
            yield return null;
        }
    }
}
