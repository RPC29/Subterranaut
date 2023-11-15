using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oreMine : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer)=="weapon"){
            if(gameObject.name.StartsWith("keyranium")){
                //playerInv.add("keyranium");
                //prompt("+1 Keyranium")
            }
            else{
                //playerInv.add(player.minerals[player.currentworld]);
                //prompt("+1 "+player.minerals[player.currentworld]);
            }
            Destroy(gameObject);
        }
    }
}
