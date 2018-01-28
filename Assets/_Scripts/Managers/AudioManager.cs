using UnityEngine;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour
{
   [SerializeField] AudioClip _musicTrack;
   [SerializeField] AudioClip _deathMusicTrack;

   public static AudioManager Instance
   {
      get { return _instance = _instance ?? FindObjectOfType<AudioManager>() ?? new AudioManager { }; }
   }
   private static AudioManager _instance;

   public AudioSource AudioSource { get { return _audioSource; } }

   private AudioSource _audioSource;

   private void Awake()
   {
      _audioSource = GetComponent<AudioSource>();
   }

   private void OnEnable()
   {
      EventManager.AddListener("DAED", OnDAED);
   }

   private void OnDisable()
   {
      EventManager.RemoveListener("DAED", OnDAED);
   }

   private void Start()
   {
      _audioSource.clip = _musicTrack;
      _audioSource.Play();
   }

   private void OnDAED()
   {
      _audioSource.Stop();
   }

}
