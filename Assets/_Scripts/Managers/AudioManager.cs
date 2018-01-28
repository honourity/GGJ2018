using UnityEngine;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour
{

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

}
