using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class istherekeyraniumtutorial : MonoBehaviour
{
    public bool locked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.haskey && locked) this.gameObject.SetActive(false);
        if (player.haskey && !locked) this.gameObject.SetActive(true);
        if (!player.haskey && locked) this.gameObject.SetActive(true);
        if (!player.haskey && !locked) this.gameObject.SetActive(false);
    }
}
