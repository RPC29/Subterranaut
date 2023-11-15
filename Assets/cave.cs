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
            if(entrance)
                SceneManager.LoadScene(sceneName:"CaveRoom");
            else
                SceneManager.LoadScene(sceneName:"Overworld");//based on currentWorld stuff
        }
    }
}
