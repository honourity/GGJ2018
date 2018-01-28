using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   private Animator _animator;

   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   public void PlayButton()
   {
      Debug.Log("Start Game!");
      _animator.Play("StartGameFade");
   }

   public void StartGame()
   {
      SceneManager.LoadScene("Game");
   }
}
