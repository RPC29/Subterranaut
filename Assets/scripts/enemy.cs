using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int type; //0-3 
    public int behaviour; //0-1 melee or ranged
    GameObject playobj;
    GameObject eneobj;
    Rigidbody2D enecol;
    int ticks = 0;
    public int health = 0;
    int paintimer = -1;
    Color basecol;
    public GameObject fire;
    public GameObject elec;
    public GameObject earth;
    Vector3 offset;
    public float enemyspeed = 1.5f;
    bool offended = false;

    // Start is called before the first frame update
    void Start()
    {
        eneobj = this.gameObject;
        enecol = eneobj.GetComponent<Rigidbody2D>();
        playobj = GameObject.Find("Player");
        health = 15 * (player.currentworld + 1);
        behaviour = Random.Range(0, 2);
        enemyspeed = 5f;
        offended = false;
    }

    // Update is called once per frame
    void Update()
    {
        eneobj.GetComponent<Animator>().SetInteger("look", player.enemylook[player.currentworld]);
        
        type = (behaviour == 0) ? player.enemybehaviour[player.currentworld] : player.renemybehaviour[player.currentworld];
        basecol = player.enemycolour[player.currentworld];
        if (paintimer < 0)eneobj.GetComponent<SpriteRenderer>().color = basecol;

        //move everything above me to start() once everything is set up or don't idk it doesn't really matter

        if (Vector3.Distance(eneobj.transform.position, playobj.transform.position) < 4f) offended = true;

        if (paintimer < 0 && offended)
        {
            eneobj.transform.localScale = new Vector3((playobj.transform.position.x > eneobj.transform.position.x) ? 1 : -1, 1, 1);
            if (type == 0)
            {
                if (behaviour == 0)
                {
                    enecol.velocity =  Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position + offset, 3f * Time.deltaTime) - eneobj.transform.position; //move towards player
                }
                if (behaviour == 1)
                {
                    if (!(ticks % 100 < 60 && ticks % 100 > 40)) enecol.velocity = Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position + offset, 1f * Time.deltaTime) - eneobj.transform.position; //fire
                    else enecol.velocity = Vector2.zero;
                }
            }
            if (type == 1)
            {
                if (behaviour == 0)
                {
                    if ((ticks % 100 < 80 && ticks % 100 > 30)) enecol.velocity =  Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position + offset , 6f * Time.deltaTime) - eneobj.transform.position; //move towards player in bursts
                    else enecol.velocity = Vector2.zero;
                }
                if (behaviour == 1)
                {
                    if (Vector3.Distance(eneobj.transform.position, playobj.transform.position) > 5f) enecol.velocity =  Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position + offset, 3f * Time.deltaTime) - eneobj.transform.position;
                    else if (Vector3.Distance(eneobj.transform.position, playobj.transform.position) < 4f) enecol.velocity =  Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position + offset, -3f * Time.deltaTime) - eneobj.transform.position;
                    else enecol.velocity = Vector2.zero;
                }
            }
            if (type == 2)
            {
                if (behaviour == 0)
                {
                    enecol.velocity = (Vector2)(Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position + offset, 3f * Time.deltaTime) - eneobj.transform.position) + 3 * Vector2.Perpendicular(new Vector2((Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position + offset, 3f * Time.deltaTime) - eneobj.transform.position).x, (Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position + offset, 3f * Time.deltaTime) - eneobj.transform.position).y)); //move towards player but in spirals
                }
                if (behaviour == 1)
                {
                    enecol.velocity =  Vector3.MoveTowards(eneobj.transform.position, playobj.transform.position, 2f * Time.deltaTime) - eneobj.transform.position;
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

        enecol.velocity = enecol.velocity.normalized * enemyspeed;
    }

    private void FixedUpdate()
    {
        if (paintimer < 0 && offended)
        {
            ticks++;
            if (ticks % 100 == 0) offset = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);
            if (type == 0)
            {
                if (behaviour == 0)
                {
                    
                }
                if (behaviour == 1)
                {
                    if (ticks % 100 == 40) GameObject.Instantiate(fire, this.transform);

                    if (ticks % 100 == 50) GameObject.Instantiate(fire, this.transform);

                    if (ticks % 100 == 60) GameObject.Instantiate(fire, this.transform);
                }
            }
            if (type == 1)
            {
                if (behaviour == 0)
                {

                }
                if (behaviour == 1)
                {
                    if (ticks % 100 == 40) GameObject.Instantiate(elec, this.transform);
                }
            }
            if (type == 2)
            {
                if (behaviour == 0)
                {

                }
                if (behaviour == 1)
                {
                    if (ticks % 50 == 0) GameObject.Instantiate(earth, this.transform);
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
            offended = true;
        }
    }
}
