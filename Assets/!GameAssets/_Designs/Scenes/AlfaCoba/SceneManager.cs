using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnderworldCafe
{
    /// <summary>
    /// Class for controlling scene transitions and main menu functionalities
    /// </summary>
    public class SceneTransitionController : MonoBehaviour
    {
        public Animator transitionAnimator;

        // Fungsi untuk memulai transisi ke scene baru
        public void LoadNextScene(string LevelMenu)
        {
            StartCoroutine(LoadSceneCoroutine(LevelMenu));
        }

        IEnumerator LoadSceneCoroutine(string LevelMenu)
        {
            // Memainkan animasi transisi
            transitionAnimator.SetTrigger("btnPlay");

            // Menunggu beberapa saat agar animasi transisi selesai
            yield return new WaitForSeconds(1);

            // Memuat scene baru
            SceneManager.LoadScene(LevelMenu);
        }

        // Fungsi untuk menuju ke scene pemilihan level
        public void ToSelectLevel()
        {
            LoadNextScene("LevelMenu"); // Ganti "SelectLevel" dengan nama scene pemilihan level Anda
        }

        // Fungsi untuk memicu animasi saat tombol "Start" ditekan
        public void TriggerStartAnimation()
        {
            transitionAnimator.SetTrigger("btnPlay");
            // Di sini Anda dapat menambahkan kode lain yang ingin dijalankan saat animasi dimulai
        }
    }
}