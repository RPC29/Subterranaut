using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldcolour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        this.gameObject.GetComponent<SpriteRenderer>().color = player.watercolour[player.currentworld];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
