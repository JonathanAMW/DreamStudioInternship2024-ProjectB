using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for controlling scene transitions and main menu functionalities
    /// </summary>
    public class SceneTransitionController : MonoBehaviour
    {
        public GameObject transitionSceneObject;
        [SerializeField] GameManager gameManager;

        // Fungsi untuk memulai transisi ke scene baru
        public void LoadNextScene(string LevelMenu)
        {
            StartCoroutine(LoadSceneCoroutine(LevelMenu));
        }

        IEnumerator LoadSceneCoroutine(string LevelMenu)
        {
            transitionSceneObject.SetActive(true);

            // Menunggu beberapa saat agar animasi transisi selesai
            //yield return new WaitForSeconds(2);

            Animator anim = transitionSceneObject.GetComponent<Animator>();
            while (anim.GetCurrentAnimatorStateInfo(0).IsName("Transisi") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null; // Wait for the next frame
            }

            // Memuat scene baru
            gameManager.SceneHandler.LoadScene(LevelMenu);
        }

        // Fungsi untuk menuju ke scene pemilihan level
        public void ToSelectLevel()
        {
            LoadNextScene("LevelMenu"); // Ganti "SelectLevel" dengan nama scene pemilihan level Anda
        }

        // Fungsi untuk memicu animasi saat tombol "Start" ditekan
        public void TriggerStartAnimation()
        {
            //transitionAnimator.SetTrigger("btnPlay");
            // Di sini Anda dapat menambahkan kode lain yang ingin dijalankan saat animasi dimulai
        }
    }
}