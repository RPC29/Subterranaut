using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        print(other);
        // if(player.inventory.contains("portalkey")){
        //     nextWorld()
        // }
        // else{
        // some kind of failure
        // }
    }
}
