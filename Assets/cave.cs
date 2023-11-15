using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cave : MonoBehaviour
{
    public bool entrance;
    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.name=="Player"){
            if(entrance && SceneManager.GetActiveScene().buildIndex != 2)
                SceneManager.LoadScene(sceneName:"CaveRoom");
            else if (entrance)
            {
                other.transform.position = new Vector3(0,-6,0);
                SceneManager.LoadScene(3);
            }
            else if (!entrance && SceneManager.GetActiveScene().buildIndex != 3)
                SceneManager.LoadScene(player.worldlayouts[player.currentworld] + 4);//change based on currentWorld index and scene number
            else
            {
                other.transform.position = new Vector3(1.5f, -1, 0);
                SceneManager.LoadScene(2);
            }
        }
    }
}
