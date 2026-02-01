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
        [SerializeField] private AudioSource environmentSource;

        // Instancias
        /* Referencias de los clips */
        [Header("Clips de Musica")]
        [SerializeField] private AudioClip mainLoopMusicClip;
        [SerializeField] private AudioClip a;
        [SerializeField] private AudioClip b;
        [SerializeField] private AudioClip c;

        // Variables necesarias
        private static AudioClip currentSoundClip;
        private static float currentSoundClipEndTime;

        // Propiedades
        public static AudioController Instance { get; private set; }
        public static AudioSource MusicSource => Instance.musicSource;
        public static AudioSource EnvironmentSource => Instance.environmentSource;
        public static AudioSource SFXSource => Instance.sFXSource;



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



        public static void PlayMainMusic(string clipRefName)
        {
            switch(clipRefName)
            {
                case "MainLoop":
                    PlayMusic(Instance.mainLoopMusicClip);
                    break;

                default:
                    Debug.LogWarning("AudioController: No se ha encontrado el clip de musica con el nombre: " + clipRefName);
                    break;
            }
        }

        public static void PlayMainMusic(string clipRefName)
        {
            switch(clipRefName)
            {
                case "a":
                    PlayMusic(Instance.mainLoopMusicClip);
                    break;

                case "b":
                    PlayMusic(Instance.mainLoopMusicClip);
                    break;

                case "c":
                    PlayMusic(Instance.mainLoopMusicClip);
                    break;

                default:
                    Debug.LogWarning("AudioController: No se ha encontrado el clip de environment con el nombre: " + clipRefName);
                    break;
            }
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
        private static void PlayMusic(AudioClip newMusicClip)
        {
            //Por si el cambio de escena te lleva a una con el mismo clip
            if (MusicSource.clip == newMusicClip) return;
            MusicSource.clip = newMusicClip;
            MusicSource.Play();
        }

        // Poner ENVIRONMENT
        public static void PlayEnvSound(AudioClip newEnvClip)
        {
            if (EnvironmentSource.clip == newEnvClip) return;
            EnvironmentSource.clip = newEnvClip;
            EnvironmentSource.Play();
        }

        // Usar SFX
        public static void PlaySound(AudioClip newSoundClip)
        {
            // Si es el mismo clip y todavia esta sonando, no repetir
            if (currentSoundClip == newSoundClip && Time.time < currentSoundClipEndTime) return;

            currentSoundClip = newSoundClip;
            currentSoundClipEndTime = Time.time + newSoundClip.length;

            SFXSource.PlayOneShot(newSoundClip);
        }



        //GETTERS Y SETTERS------------------------------------------------------------------------------------------------
        public AudioSource GetMusicSource() { return musicSource; }
        public AudioSource GetSoundsSource() { return sFXSource; }
    }
}
