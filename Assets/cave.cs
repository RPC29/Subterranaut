using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cave : MonoBehaviour
{
    public bool entrance;
    bool standing;

    private void Start()
    {
        if (!entrance) standing = true; else standing = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.name=="Player" && !standing){
            if(entrance && SceneManager.GetActiveScene().buildIndex != 2)
                SceneManager.LoadScene(sceneName:"CaveRoom");
            else if (entrance)
            { 
                SceneManager.LoadScene(3);
            }
            else if (!entrance && SceneManager.GetActiveScene().buildIndex != 3)
                SceneManager.LoadScene(player.worldlayouts[player.currentworld] + 4);//change based on currentWorld index and scene number
            else
            {
                SceneManager.LoadScene(2);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && standing) standing = false;
    }
}
