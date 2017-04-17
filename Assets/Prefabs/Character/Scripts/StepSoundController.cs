using UnityEngine;
using System.Collections;

public class StepSoundController : MonoBehaviour {
    [System.Serializable]
    public class AudioArray
    {
        public AudioClip[] sounds;
        public AudioClip getRandomSound()
        {
            return sounds[Random.Range(0, sounds.Length)];
        }
    }

	public AudioArray[] stepSounds;

    public AudioSource audioSource;
    
	public void PlayTerrainStepSound() {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        int textureId = TerrainSurface.GetMainTexture (transform.position);
        if(!audioSource.isPlaying)
        {
            switch (textureId)
            {
                case 1:
                    if (stepSounds[1] != null)
                    {
                        audioSource.PlayOneShot(stepSounds[1].getRandomSound());
                    }
                    break;
                default:
                    if (stepSounds[0] != null)
                    {
                        audioSource.PlayOneShot(stepSounds[0].getRandomSound());
                    }
                    break;
            }
        }
    }
}
