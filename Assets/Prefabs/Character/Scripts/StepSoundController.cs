using UnityEngine;
using System.Collections;
using System.Collections.Generic;




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

    [System.Serializable]
    public class DictOfMovementTypeSoundVolume : SerializableDictionary<MovementType, float> { }

    public DictOfMovementTypeSoundVolume movementSoundVol;

    public AudioArray[] stepSounds;

    public AudioSource audioSource;

    void Awake ()
    {
        movementSoundVol = new DictOfMovementTypeSoundVolume();
        movementSoundVol.Add(MovementType.NoMovement, 0.0f);
        movementSoundVol.Add(MovementType.Walk, 0.2f);
        movementSoundVol.Add(MovementType.Run, 1.0f);
    }

	public void PlayTerrainStepSound(MovementType moveType) {
        /*audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.volume = movementSoundVol[moveType];
        
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
        }*/
    }
}
