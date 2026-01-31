using UnityEngine;

namespace AlvaroRuiz.Projects.GameControll.Audio
{
    public class AudioController : MonoBehaviour
    {
        // VARIABLES/CAMPOS
        // Referencias propias
        [Header("Audio Sources Music & SFX")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sFXSource;

        // Instancias
        /* Referencias de los clips */
        [Header("Clips de Musica")]
        [SerializeField] private AudioClip templateMusicClip;

        /* Clips de efectos de sonido */
        [Header("Clips de SFX")]
        [SerializeField] private AudioClip templateSFXClip;

        // Variables necesarias
        private AudioClip currentSoundClip;
        private float currentSoundClipEndTime;

        // Propiedades
        public static AudioController Instance { get; private set; }



        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }



        // HERRAMIENTAS-------------------------------------------------------------
        private void CoroutineStopper(Coroutine currentCoroutine)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
            }
        }



        // Poner MUSICA
        public void PlayMusic(AudioClip newMusicClip)
        {
            //Por si el cambio de escena te lleva a una con el mismo clip
            if (musicSource.clip == newMusicClip) return;
            musicSource.clip = newMusicClip;
            musicSource.Play();
        }

        // Usar SFX
        public void PlaySound(AudioClip newSoundClip)
        {
            // Si es el mismo clip y todavia esta sonando, no repetir
            if (currentSoundClip == newSoundClip && Time.time < currentSoundClipEndTime) return;

            currentSoundClip = newSoundClip;
            currentSoundClipEndTime = Time.time + newSoundClip.length;

            sFXSource.PlayOneShot(newSoundClip);
        }



        //GETTERS Y SETTERS------------------------------------------------------------------------------------------------
        public AudioSource GetMusicSource() { return musicSource; }
        public AudioSource GetSoundsSource() { return sFXSource; }
    }
}
