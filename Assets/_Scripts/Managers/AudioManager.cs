using UnityEngine;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour
{
   private static AudioManager _instance;
   public static AudioManager Instance { get { return _instance = _instance ?? FindObjectOfType<AudioManager>(); } }
}
