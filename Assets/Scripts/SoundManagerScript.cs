using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHitSound, jumpSound, wallJumpSound, pickUpCogwheelSound, fixElevatorSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("death");
        jumpSound = Resources.Load<AudioClip>("jump");
        wallJumpSound = Resources.Load<AudioClip>("walljump");
        pickUpCogwheelSound = Resources.Load<AudioClip>("pickUpCogwheel");
        fixElevatorSound = Resources.Load<AudioClip>("fixElevator");
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

        }
    }
}
