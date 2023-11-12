using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public bool evil = true;
    public GameObject playobj;
    int alivetime;

    // Start is called before the first frame update
    void Start()
    {
        alivetime = 120;
        this.transform.parent = null;
        playobj = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (evil) this.transform.position = Vector3.MoveTowards(this.transform.position, playobj.transform.position, 1.5f * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        alivetime--;
        if (alivetime < 0) Destroy(this.gameObject);
    }
}
