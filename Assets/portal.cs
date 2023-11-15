using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        print(other);
        if (other.gameObject.layer != 3) return;
        if (player.tutorial && player.haskey)
        {
            player.tutorial = false;
            SceneManager.LoadScene(player.worldlayouts[player.currentworld] + 4);
        }
        if (player.haskey)
        {
            
            if (player.currentworld == 3)
            {
                Destroy(GameObject.Find("Musicer"));
                Destroy(GameObject.Find("Player"));
                SceneManager.LoadScene(11);
            }
            player.currentworld++;
            SceneManager.LoadScene(player.worldlayouts[player.currentworld] + 4);
            player.haskeyranium = false;
            player.haskey = false;
        }
    }
}
