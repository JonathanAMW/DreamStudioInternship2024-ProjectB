//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/04/03"
//----------------------------------------------------------------------

using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Class for managing audio in game
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [Header("AUDIO SOURCES")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;


        [field: Header("Music Clips")]
        [field : SerializeField] public AudioClip MainMenuMusic { get; private set; }

        
        [field: Header("SFX Clips")]
        [field: Header("Player")]
        [field : SerializeField] public AudioClip PlayerWalkSFX { get; private set; }

        [field: Header("Customer")]
        [field : SerializeField] public AudioClip CustomerSpawnSFX { get; private set; }

        [field: Header("Utensil")]
        [field : SerializeField] public AudioClip UtensilProcessingSFX { get; private set; }
        [field : SerializeField] public AudioClip UtensilGeneratingSFX { get; private set; }



        
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
