using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    // List of sound clips
    public static AudioClip ThrusterClip, ImpactClip, ExplodeClip, VictoryClip;

    // Audio source
    static AudioSource am;

    [SerializeField]
    [Tooltip("Audio clip for the Thrusters.")]
    public AudioClip Thruster_SFX;
    [SerializeField]
    [Tooltip("Audio clip for the Impact.")]
    public AudioClip Impact_SFX;
    [SerializeField]
    [Tooltip("Audio clip for Explosions.")]
    public AudioClip Explode_SFX;
    [SerializeField]
    [Tooltip("Audio clip for when Victory is achieved.")]
    public AudioClip Victory_SFX;


    // Use this for initialization
    void Start ()
    {
        ThrusterClip = Thruster_SFX;
        ImpactClip = Impact_SFX;
        ExplodeClip = Explode_SFX;
        VictoryClip = Victory_SFX;

        am = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            // Thruster sound
            case "ThrusterClip":
                am.PlayOneShot(ThrusterClip, 1f);
                break;
            // Impact sound
            case "ImpactClip":
                am.PlayOneShot(ImpactClip, 1f);
                break;
            // Explode sound
            case "ExplodeClip":
                am.PlayOneShot(ExplodeClip, 1f);
                break;
            // Victory sound
            case "VictoryClip":
                am.PlayOneShot(VictoryClip, 1f);
                break;
        }
    }
}
