using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
   private static ScoreManager _instance;
   public static ScoreManager Instance { get { return _instance = _instance ?? FindObjectOfType<ScoreManager>(); } }

   private Text _scoreText;
   public int Score;

   private void Awake()
   {
      _scoreText = GetComponent<Text>();
      _scoreText.text = "0";
   }

   private void Start()
   {
      StartCoroutine(CountScore());
   }

   private IEnumerator CountScore()
   {
      while (!GameManager.Instance.Player.Dead)
      {
         yield return new WaitForSeconds(1f);
         Score++;
         _scoreText.text = Score.ToString();
      }

   }

}
