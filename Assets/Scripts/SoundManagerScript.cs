using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip 
        playerHitSound, 
        jumpSound, 
        wallJumpSound, 
        pickUpCogwheelSound, 
        fixElevatorSound, 
        checkpointSound, 
        ropeSound, 
        ghostSound,
        hookSound;

    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("death");
        jumpSound = Resources.Load<AudioClip>("jump");
        wallJumpSound = Resources.Load<AudioClip>("walljump");
        pickUpCogwheelSound = Resources.Load<AudioClip>("pickUpCogwheel");
        fixElevatorSound = Resources.Load<AudioClip>("fixElevator");
        checkpointSound = Resources.Load<AudioClip>("checkpoint");
        ropeSound = Resources.Load<AudioClip>("rope");
        hookSound= Resources.Load<AudioClip>("hook");
        ghostSound = Resources.Load<AudioClip>("ghostChase");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "walljump":
                audioSrc.PlayOneShot(wallJumpSound);
                break;
            case "death":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "pickUpCogwheel":
                audioSrc.PlayOneShot(pickUpCogwheelSound);
                break;
            case "fixElevator":
                audioSrc.PlayOneShot(fixElevatorSound);
                break;
            case "checkpoint":
                audioSrc.PlayOneShot(checkpointSound);
                break;
            case "rope":
                audioSrc.PlayOneShot(ropeSound);
                break;
            case "hook":
                audioSrc.PlayOneShot(hookSound);
                break;
            case "ghost":
                audioSrc.PlayOneShot(ghostSound);
                break;

        }
    }
}
