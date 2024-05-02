//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/04/03"
//----------------------------------------------------------------------

using Unity.VisualScripting;
using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Class for managing audio in game
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [Header("<size=16><b>============</b></size><color=#FFFFFF>AUDIO SOURCES</color><size=16><b>============</b></size>")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;


        [field: Header("<size=16><b>============</b></size><color=#FFFFFF>MUSIC CLIPS</color><size=16><b>============</b></size>")]
        [field : SerializeField] public AudioClip MainMenuMusic { get; private set; }
        [field : SerializeField] public AudioClip LevelMusic { get; private set; }

        
        [field: Header("<size=16><b>============</b></size><color=#FFFFFF>SFX CLIPS</color><size=16><b>============</b></size>")]
        [field: Header("Player")]
        [field : SerializeField] public AudioClip PlayerWalkSFX { get; private set; }

        [field: Header("Customer")]
        [field : SerializeField] public AudioClip CustomerSpawnSFX { get; private set; }
        [field : SerializeField] public AudioClip CustomerOrderUIPopupSFX { get; private set; }
        [field : SerializeField] public AudioClip CustomerServedSFX { get; private set; }

        [field: Header("Utensil")]
        [field : SerializeField] public AudioClip UtensilProcessingSFX { get; private set; }
        [field : SerializeField] public AudioClip UtensilGeneratingSFX { get; private set; }
        [field : SerializeField] public AudioClip UtensilFoodReadySFX { get; private set; }



        
        private bool _isMusicMuted;
        private bool _isSFXMuted;
        
        public void PlaySFX(AudioClip clip)
        {
            if(clip == null) return;
            
            _sfxSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip)
        {
            if(clip == null) return;

            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void SetMusicMuted(bool isMuted)
        {
            _isMusicMuted = isMuted;
            _musicSource.mute = isMuted;
        }

        public void SetSFXMuted(bool isMuted)
        {
            _isSFXMuted = isMuted;
            _sfxSource.mute = isMuted;
        }

    }
}
