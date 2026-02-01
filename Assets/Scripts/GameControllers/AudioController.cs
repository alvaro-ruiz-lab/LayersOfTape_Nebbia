using UnityEngine;

namespace AlvaroRuiz.Projects.GameControll.Audio
{
    public class AudioController : MonoBehaviour
    {
        // VARIABLES/CAMPOS
        // Referencias propias
        [Header("Audio Sources Music & SFX")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource environmentSource1;
        [SerializeField] private AudioSource environmentSource2;
        [SerializeField] private AudioSource sFXSource;

        // Instancias
        /* Referencias de los clips */
        [Header("Clips de Musica")]
        [SerializeField] private AudioClip mainLoopMusicClip;

        // Variables necesarias
        private static AudioClip currentSFXClip;
        private static float currentSFXClipEndTime;

        // Propiedades
        public static AudioController Instance { get; private set; }
        public static AudioSource MusicSource => Instance.musicSource;
        public static AudioSource EnvironmentSource1 => Instance.environmentSource1;
        public static AudioSource EnvironmentSource2 => Instance.environmentSource2;
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



        // HERRAMIENTAS-------------------------------------------------------------
        // Poner MUSICA
        public static void PlayMusic(string clipRefName)
        {
            AudioClip newMusicClip = null;
            switch(clipRefName)
            {
                case "MainLoop":
                    newMusicClip = Instance.mainLoopMusicClip;
                    break;

                default:
                    Debug.LogWarning("AudioController: No se ha encontrado el clip de musica con el nombre: " + clipRefName);
                    break;
            }

            if (MusicSource.clip == newMusicClip) return;
            MusicSource.clip = newMusicClip;
            MusicSource.Play();
        }

        // Poner ENVIRONMENT
        public static void PlayEnvSound(AudioClip newEnvClip, int sourceSelection)
        {
            AudioSource source = sourceSelection switch
            {
                1 => EnvironmentSource1,
                2 => EnvironmentSource2,
                _ => null
            };

            if (source == null) return;

            if (newEnvClip != null && source.clip != newEnvClip)
            {
                source.clip = newEnvClip;
            }

            // Si clip null, se usa el asignado en inspector
            source.Play();
        }

        // Usar SFX
        public static void PlaySFX(AudioClip newSoundClip)
        {
            // Si es el mismo clip y todavia esta sonando, no repetir
            if (currentSFXClip == newSoundClip && Time.time < currentSFXClipEndTime) return;

            currentSFXClip = newSoundClip;
            currentSFXClipEndTime = Time.time + newSoundClip.length;

            SFXSource.PlayOneShot(newSoundClip);
        }
    }
}
