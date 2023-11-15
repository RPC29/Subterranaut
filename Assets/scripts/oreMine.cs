using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oreMine : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (gameObject.name.StartsWith("keyranium") && (player.haskeyranium || player.haskey)) Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6) { 
            if(gameObject.name.StartsWith("keyranium")){
                //playerInv.add("keyranium");
                //prompt("+1 Keyranium")
                player.haskeyranium = true;
            }
            else{
                //playerInv.add(player.minerals[player.currentworld]);
                //prompt("+1 "+player.minerals[player.currentworld]);
                player.mineralcount[player.currentworld]++;
            }
            Destroy(gameObject);
        }
    }
}
