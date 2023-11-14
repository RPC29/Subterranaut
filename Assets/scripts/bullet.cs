using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    GameObject bulobj;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        bulobj = this.gameObject;
        bulobj.transform.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulobj.transform.position += bulobj.transform.up * speed/10f;
        speed -= 0.1f;
        if (speed < 0.1) Destroy(this.gameObject);
        this.gameObject.GetComponent<power>().strength = (int)speed * 2;
    }
}
