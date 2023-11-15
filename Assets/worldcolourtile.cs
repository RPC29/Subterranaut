using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class worldcolourtile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        this.gameObject.GetComponent<Tilemap>().color = player.watercolour[player.currentworld];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
