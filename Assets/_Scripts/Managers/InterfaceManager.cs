using UnityEngine;

[DisallowMultipleComponent]
public class InterfaceManager : MonoBehaviour
{
   private static InterfaceManager _instance;
   public static InterfaceManager Instance { get { return _instance = _instance ?? FindObjectOfType<InterfaceManager>(); } }
}
