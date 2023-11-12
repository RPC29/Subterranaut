using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    GameObject playobj;
    public GameObject sword;
    public GameObject bow;
    public SpriteRenderer playspr;
    public SpriteRenderer bowspr;
    public Sprite move1, move2, move3, move4;
    public Sprite bow1, bow2, bow3, bow4;
    int movetimer = 0;
    bool moving = false;
    int weapon = 0;
    // 0 - sword, 1 - bow
    int swordatking = 0;
    int swordcooldown = 0;
    int bowatking = 0;
    int bowstage = 0;
    public static int laststate = 0;
    public GameObject arow;
    public Camera cam;
    int paintimer = -1;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        playobj = this.gameObject;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("x") > 0)
        {
            playobj.transform.position += new Vector3(5 * Time.deltaTime, 0, 0);
        }
        if (Input.GetAxisRaw("x") < 0)
        {
            playobj.transform.position += new Vector3(-5 * Time.deltaTime, 0, 0);
        }
        if (Input.GetAxisRaw("y") > 0)
        {
            playobj.transform.position += new Vector3(0, 5 * Time.deltaTime, 0);
        }
        if (Input.GetAxisRaw("y") < 0)
        {
            playobj.transform.position += new Vector3(0, -5 * Time.deltaTime, 0);
        }
        if (Input.GetAxisRaw("x") == 0 && Input.GetAxisRaw("y") == 0) moving = false;
        else moving = true;

        if (health < 1) Destroy(this.gameObject);
        if (paintimer > 0)
        {
            paintimer--;
        }
        if (paintimer == 0)
        {
            playspr.color = new Color(1,1,1,1);
            paintimer--;
        }

        if (Input.GetMouseButtonDown(0) && weapon == 0 && swordcooldown < 1)
        {
            swordatking = 20;
            swordcooldown = 60;
            lookAt2D(sword, cam.ScreenToWorldPoint(Input.mousePosition));
            sword.transform.rotation = Quaternion.Euler(0, 0, sword.transform.rotation.eulerAngles.z - 30);
        }
        if (Input.GetMouseButtonDown(0) && weapon == 1)
        {
            bowstage = 1;
            bowatking = 80;
        }
        if (Input.GetMouseButtonUp(0) && weapon == 1)
        {
            bowatking = 0;
            laststate = bowstage;
            bowstage = 0;
            GameObject.Instantiate(arow, bow.transform);
        }
        if (weapon == 1)
        {
            bow.SetActive(true);
            lookAt2D(bow, cam.ScreenToWorldPoint(Input.mousePosition));
        }
        else bow.SetActive(false);
        if (Input.GetMouseButtonDown(1)) weapon++;
    }
    private void FixedUpdate()
    {
        if (moving) movetimer++;
        else movetimer = 0;
        if ((movetimer / 10) % 4 == 0) playspr.sprite = move1;
        if ((movetimer / 10) % 4 == 1) playspr.sprite = move2;
        if ((movetimer / 10) % 4 == 2) playspr.sprite = move3;
        if ((movetimer / 10) % 4 == 3) playspr.sprite = move4;

        if (swordatking > 0) swordatking--;
        if (swordcooldown > 0) swordcooldown--;
        if (swordatking > 0) sword.transform.rotation = Quaternion.Euler(0, 0, sword.transform.rotation.eulerAngles.z + 3);
        if (swordatking > 0) sword.SetActive(true);
        else sword.SetActive(false);


        if (bowatking > 0 && Input.GetMouseButton(0) && weapon == 1)
        {
            bowatking--;
            bowstage = Mathf.Clamp(((80 - bowatking) / 20),1,3);

        }
        if (bowstage == 0) bowspr.sprite = bow1;
        if (bowstage == 1) bowspr.sprite = bow2;
        if (bowstage == 2) bowspr.sprite = bow3;
        if (bowstage == 3) bowspr.sprite = bow4;
        Debug.Log(bowstage);
    }

    public static void lookAt2D(GameObject a, Vector3 b)
    {
        a.transform.rotation = Quaternion.LookRotation(Vector3.forward, b - a.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && paintimer < 0) 
        {
            health -= ((power)(collision.gameObject.GetComponent("power"))).strength;
            paintimer = ((power)(collision.gameObject.GetComponent("power"))).strength * 3;
            playspr.color = new Color(0.6f, 0, 0, 1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 7 && paintimer < 0)
        {
            health -= ((power)(collision.gameObject.GetComponent("power"))).strength;
            paintimer = ((power)(collision.gameObject.GetComponent("power"))).strength * 3;
            playspr.color = new Color(0.6f, 0, 0, 1);
        }
    }
}
