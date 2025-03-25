using UnityEngine;

public class LosingAudioManager : MonoBehaviour
{
    public static LosingAudioManager instance; // Singleton instance
    private AudioSource audioSource;
    public AudioClip loseSound; // Drag and drop the lose sound in Inspector

    void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps AudioManager across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource component
    }

    public void PlayLoseSound()
    {
        if (loseSound != null)
        {
            audioSource.PlayOneShot(loseSound); // Plays the lose sound
        }
    }
}



/* In MainScript add: 
 
 
 if (ordersPassed >= 3)
{
    LosingAudioManager.instance.PlayLoseSound(); // Play the lose sound
}


*/
