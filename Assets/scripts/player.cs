using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    GameObject playobj;
    public GameObject sword, bow;
    public SpriteRenderer playspr, bowspr;
    public Sprite bow1, bow2, bow3, bow4;
    public int playerSpeed;
    Animator playerAnim;
    
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
    public GenerateDungeons gd;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(10);
        playobj = gameObject;
        health = 100;
        playerAnim = GetComponent<Animator>();
        gd=GameObject.Find("DungeonGenerator").GetComponent<GenerateDungeons>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))gd.generateCorridors();
        if(Input.GetKeyDown(KeyCode.RightShift))gd.generateRooms();
        if(Input.GetKeyDown(KeyCode.Backspace))gd.clearTiles();

        if(!(Input.GetAxisRaw("x") == 0 && Input.GetAxisRaw("y") == 0)) {//movement
            playobj.transform.position+=new Vector3(Input.GetAxis("x") , Input.GetAxis("y"), 0).normalized* Time.deltaTime*playerSpeed;
            if(!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalk"))
                playerAnim.SetTrigger("PlayerWalk");
        }
        else if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalk"))
            playerAnim.SetTrigger("PlayerIdle");


        if (health < 1) Destroy(this.gameObject); //health check


        if (paintimer >= 0) paintimer--;
        if (paintimer == -1)playspr.color = new Color(1,1,1,1); //reset player color

        //sword
        if (Input.GetMouseButtonDown(0) && weapon == 0 && swordcooldown < 1)
        {
            swordatking = 20;
            swordcooldown = 60;
            lookAt2D(sword, cam.ScreenToWorldPoint(Input.mousePosition));
            sword.transform.rotation = Quaternion.Euler(0, 0, sword.transform.rotation.eulerAngles.z - 30);
        }

        //bow
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
        if (Input.GetMouseButtonDown(1)) weapon=((weapon+1)%2);
    }
    private void FixedUpdate()
    {

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
    }

    public static void lookAt2D(GameObject a, Vector3 b)
    {
        a.transform.rotation = Quaternion.LookRotation(Vector3.forward, b - a.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && paintimer < 0) //fire
        {
            health -= ((power)(collision.gameObject.GetComponent("power"))).strength;
            paintimer = ((power)(collision.gameObject.GetComponent("power"))).strength * 3;
            playspr.color = new Color(0.6f, 0, 0, 1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 7 && paintimer < 0) // enemy
        {
            health -= ((power)(collision.gameObject.GetComponent("power"))).strength;
            paintimer = ((power)(collision.gameObject.GetComponent("power"))).strength * 3;
            playspr.color = new Color(0.6f, 0, 0, 1);
        }
    }
}
