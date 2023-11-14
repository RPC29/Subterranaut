using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int type; //0-6 
    public int look; //0-??? 
    public int behaviour; //0-1 melee or ranged
    GameObject playobj;
    public GameObject eneobj;
    public Sprite blub1, blub2;
    int ticks = 0;
    public int health = 0;
    int paintimer = -1;
    Color basecol;
    public GameObject fire;

    // Start is called before the first frame update
    void Start()
    {
        eneobj = this.gameObject;
        playobj = GameObject.Find("Player");
        health = 50;
        basecol = eneobj.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (paintimer < 0)
        {
            eneobj.transform.localScale = new Vector3((playobj.transform.position.x > eneobj.transform.position.x) ? 1 : -1, 1, 1);
            if (type == 0)
            {
                if (behaviour == 0)
                {
                    eneobj.transform.position = Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position, 1f * Time.deltaTime);
                }
                if (behaviour == 1)
                {
                    if (!(ticks % 100 < 60 && ticks % 100 > 40)) eneobj.transform.position = Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position, 1f * Time.deltaTime);
                }
            }

        }
        if (health < 1) Destroy(this.gameObject);
        if (paintimer > 0) 
        {
            paintimer--;
        }
        if (paintimer == 0)
        {
            eneobj.GetComponent<SpriteRenderer>().color = basecol;
            paintimer--;
        }
    }

    private void FixedUpdate()
    {
        if (paintimer < 0)
        {
            ticks++;
            if (type == 0)
            {
                if (behaviour == 0)
                {
                    
                }
                if (behaviour == 1)
                {
                    if (ticks%100 == 40) GameObject.Instantiate(fire, this.transform);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && paintimer < 0) 
        {
            health -= ((power)(collision.gameObject.GetComponent("power"))).strength;
            paintimer = ((power)(collision.gameObject.GetComponent("power"))).strength * 3;
            eneobj.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0, 0, 1);
        }
    }
}
