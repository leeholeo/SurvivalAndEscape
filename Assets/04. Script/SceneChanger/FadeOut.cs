// // 캐릭터 사망 시 화면 fadeout, UI canvas에 부착, DeadPanel 연결

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class FadeOut : MonoBehaviour
// {
//     // 수동으로 연결
//     [Header("Manual Link")]
//     public GameObject deadPanel;
//     // public void Fade()
//     // {
//     //     Debug.Log("coroutine started");
//     //     StartCoroutine(DoFade());
//     // }
//     //public Camera PlayerCamera;

//     public void PlayerDead()
//     {
//         //Camera.main.cullingMask = 1 << 5;
//         //PlayerCamera.cullingMask = 1 << 5;
//         //this.transform.LookAt(PlayerCamera.transform.parent);
//         //print("부모:" + PlayerCamera.transform.parent.name);
//         StartCoroutine(PlayerDeadCoroutine());
//     }

//     public IEnumerator PlayerDeadCoroutine()
//     {
//         yield return StartCoroutine(DoFade());
//         yield return StartCoroutine(ToTestScene());
//     }

//     public IEnumerator DoFade()
//     {
//         // Panel on
//         // Debug.Log("FadeOut start");
//         deadPanel.SetActive(true);
//         yield return null;
//         // Fading
//         CanvasGroup canvasGroup = deadPanel.GetComponent<CanvasGroup>();
//         while (canvasGroup.alpha < 1)
//         {
//             // Debug.Log("FadeOut progressing...");
//             canvasGroup.alpha += Time.deltaTime / 2;
//             yield return null;
//         }
//         canvasGroup.interactable = false;
//         // Debug.Log("FadeOut done");
//         yield return new WaitForSeconds(3);
//         // Panel off
//         deadPanel.SetActive(false);
//         // Debug.Log("Panel Disactive");
//     }

//     public IEnumerator ToTestScene()
//     {
//         SceneManager.LoadScene("LobbyScene");
//         yield return null;
//     }
// }
