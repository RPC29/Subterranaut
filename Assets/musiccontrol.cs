using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musiccontrol : MonoBehaviour
{

    public AudioSource[] overworldmusics;
    public AudioSource[] cavemusics;
    int prevworld = 999;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        prevworld = 999;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex != prevworld)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == player.cavemusic[player.currentworld] && (SceneManager.GetActiveScene().buildIndex == 8))
                {
                    if (!cavemusics[i].isPlaying) cavemusics[i].Play();
                }
                else if (cavemusics[i].isPlaying) cavemusics[i].Pause();
                if (i == player.worldmusic[player.currentworld] && !(SceneManager.GetActiveScene().buildIndex == 8))
                {
                    if (!overworldmusics[i].isPlaying) overworldmusics[i].Play();
                }
                else if (overworldmusics[i].isPlaying) overworldmusics[i].Pause();
            }
            prevworld = SceneManager.GetActiveScene().buildIndex;
        }
    }
}
