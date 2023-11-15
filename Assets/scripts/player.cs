using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class player : MonoBehaviour
{

    public static string[] minerals = { "Wood", "Diamond", "Ruby", "Sapphire", "Emerald", "Amethyst", "Topaz", "Opal", "Garnet", "Aquamarine", "Citrine", "Tanzanite", "Peridot", "Tourmaline", "Morganite", "Lapis Lazuli", "Malachite", "Pearl", "Onyx", "Pyrite", "Amber", "Jasper", "Moonstone", "Turquoise", "Rhodonite", "Azurite", "Hematite", "Sunstone", "Fluorite", "Apatite", "Obsidian", "Celestite", "Rhodochrosite", "Carnelian", "Serpentine", "Jade", "Larimar", "Zircon", "Agate", "Sardonyx", "Selenite", "Howlite", "Chalcedony", "Sphene", "Rhodonite", "Prehnite", "Kunzite", "Chrysoberyl", "Dioptase", "Cuprite", "Axinite", "Smithsonite", "Kyanite", "Danburite", "Rhodochrosite", "Staurolite", "Vanadinite", "Wulfenite", "Andalusite", "Chrysocolla", "Charoite", "Covellite", "Amazonite", "Smithsonite", "Aventurine", "Labradorite", "Rhodolite", "Titanite", "Axinite", "Sunstone", "Zoisite", "Iolite", "Uvarovite", "Petrified Wood", "Red Jasper", "Blue Lace Agate", "Blue Chalcedony", "Sodalite", "Fire Agate", "Poop", "Green Fluorite" };
    public static string[] mineralcolours = { "#966f33", "#b9f2ff", "#9b111e", "#082567", "#50c878", "#9966cc", "#ffc87c", "#a8c3bc", "#733635", "#7fffd4", "#e4d00a", "#0d4684", "#90ee90", "#84c7c1", "#ffb6c1", "#1f4788", "#0bda51", "#f0e6d2", "#353839", "#d4af37", "#ffbf00", "#d73e4d", "#a9a9a9", "#40e0d0", "#733b4f", "#2e4b8c", "#434c4d", "#e29c45", "#8f72b8", "#5ba4cf", "#1a1a1a", "#b2dfee", "#ff9999", "#b31b1b", "#3cb371", "#00a86b", "#0077cc", "#f4f2e8", "#b8b396", "#ff4500", "#f0e6f6", "#e6e6e6", "#9cb3c2", "#ffd700", "#733b4f", "#b4cd9c", "#d8bfd8", "#e6b422", "#008080", "#b87333", "#7d7d7d", "#80c7c9", "#0f52ba", "#f8f8ff", "#ff9999", "#8b4513", "#7d2828", "#ffdb58", "#964b00", "#008080", "#551a8b", "#7e7e7e", "#00c4b0", "#80c7c9", "#007f00", "#6a5acd", "#b7410e", "#ff6347", "#7d7d7d", "#e29c45", "#3b5998", "#8674a1", "#3b5323", "#8b4513", "#d73e4d", "#b0c4de", "#6495ed", "#1e90ff", "#e25822", "#7a5901", "#4caf50" };
    public static string[] colours = { "#ff6f61", "#6a0572", "#d45087", "#f06", "#1a2a6c", "#b4c5e4", "#ff85a1", "#fff4e0", "#2ec4b6", "#fea82f", "#ecf7f7", "#fdb827", "#04ab70", "#f0a6ca", "#fffdd0", "#96ded1", "#c06c84", "#93a8ac", "#ed6a5a", "#ffa69e", "#6a0572", "#f3722c", "#dbe2ef", "#f9f4f3", "#ffdbe1", "#e85d75", "#aa96da", "#f8f1f1", "#5f4b8b", "#ff5e78", "#8cc7a1", "#fae3d9", "#ed8a63", "#7ae582", "#00adb5", "#f38181", "#ffb997", "#ffcc5c", "#61c0bf", "#fff9eb", "#b3cdd1" };

    public static int[] cavetype = { 0, 0, 0, 0 }; //decides cave types for every world
    public static int[] mineraltype = { 0, 0, 0, 0 }; //decides mineral types for every world
    public static Color[] mineralcolourtype = { Color.black, Color.black, Color.black, Color.black }; //overworld colours for every world
    public static Color[] worldcolour = { Color.black, Color.black, Color.black, Color.black }; //overworld colours for every world
    public static Color[] watercolour = { Color.black, Color.black, Color.black, Color.black }; //overworld colours for every world
    public static Color[] enemycolour = { Color.black, Color.black, Color.black, Color.black }; //enemy colours for every world
    public static int[] enemylook = { 0, 0, 0, 0 };
    public static int[] enemybehaviour = { 0, 0, 0, 0 }; //for melee
    public static int[] renemybehaviour = { 0, 0, 0, 0 }; //for ranged
    public static int[] worldlayouts = { 0, 0, 0, 0 }; //which overworlds
    public static int[] weapons = { 0, 0, 0, 0 }; //weapons you have rn
    public static int[] weaponminerals = { 0, 0, 0, 0, 0 }; //the minerals those weapons are made out of
    public static int[] worldweapons = { 0, 0, 0, 0 }; //weapons in every world
    public static int[] worldmusic = { 0, 0, 0, 0 }; //weapons in every world
    public static int[] cavemusic = { 0, 0, 0, 0 }; //weapons in every world
    public static bool[] recievedweapons = { false, false, false, false };
    public static int weaponcount = 1;

    public static int currentworld = 0;

    public List<List<List<Vector2>>> caves = new List<List<List<Vector2>>>();

    public static bool tutorial;
    public static int[] mineralcount = { 0, 0, 0, 0 };
    public static bool haskeyranium = false;
    public static bool haskey = false;
    

    public Text[] weaponshow;
    public Slider healthbar;
    public Image speakbox;
    public Text speaktext;
    public Text counttext;
    public static bool speaking;
    public static string dialogue;

    GameObject playobj;
    public Rigidbody2D playbody;
    public GameObject sword, bow, gun, boomeranger, spell;
    public SpriteRenderer playspr, bowspr;
    public Sprite bow1, bow2, bow3, bow4, dirt;
    public int playerSpeed;
    Animator playerAnim;
    
    int weapon = 0;
    int weaponindex = 0;
    // 0 - sword, 1 - bow, 2 - gun, 3 - boomerang, 4 - magic
    public static string[] weaponnames = { "Sword", "Bow", "Gun", "Boomerang", "Magic" };
    int swordatking = 0;
    int swordcooldown = 0;
    int bowatking = 0;
    int bowstage = 0;
    public static bool bettersword = false;
    public static int laststate = 0;
    int reloadtimer = 0;
    int reloads = 6;
    int boomerangs = 2;
    public static int magictype = 0; //0 - fire, 1 - lightning, 2 - earth
    int magiccooldown = 0;
    public GameObject arow;
    public GameObject bulet;
    public GameObject boomer;
    public GameObject firey, elecy, earthy;
    public GameObject Light;
    public Camera cam;
    int paintimer = -1;
    public float health;
    public GenerateDungeons gd;

    // Start is called before the first frame update
    void Start()
    {

        weaponcount = 1;
        weaponminerals[4] = 0;


        gd=GetComponentInChildren<GenerateDungeons>();
        RandomizeWorld();

        // Random.InitState(10);
        playobj = gameObject;
        health = 100;
        playerAnim = GetComponent<Animator>();

        // the stuff above goes inside the onawake function of cave scene
        reloads = 6;
        boomerangs = 2;
        GameObject.DontDestroyOnLoad(this.gameObject);

        tutorial = true;

        haskeyranium = false;
        haskey = false;

        SceneManager.LoadScene(2);

        weaponcount = 1;

        bettersword = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = 0.5f + ((float)health/200f);
        if (speaking) speakbox.gameObject.SetActive(true);
        else speakbox.gameObject.SetActive(false);   
        if (speaktext) speaktext.text = dialogue;

        if (SceneManager.GetActiveScene().buildIndex == 3|| SceneManager.GetActiveScene().buildIndex == 8) Light.SetActive(true);
        else Light.SetActive(false);

        counttext.text = (tutorial?"Wood":(minerals[mineraltype[currentworld]])) + " - " + mineralcount[currentworld] + ((haskey)?"\nKey - 1":"\nKeyranium - " + ((haskeyranium)?"1":"0"));

        if (!(Input.GetAxisRaw("x") == 0 && Input.GetAxisRaw("y") == 0))//movement
        {
            playbody.velocity = (new Vector3(Input.GetAxisRaw("x"), Input.GetAxisRaw("y"), 0).normalized * Time.deltaTime * playerSpeed);
            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalk"))
                playerAnim.SetTrigger("PlayerWalk");
        }
        else
        {
            playbody.velocity = Vector2.zero;
            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalk"))
                playerAnim.SetTrigger("PlayerIdle");
        }

        
        if (health <= 0) {
            //restart function --------------------------------------
            SceneManager.LoadScene(9);
            Destroy(GameObject.Find("Musicer"));
            Destroy(this.gameObject);
        } //health check


        if (paintimer >= 0) paintimer--;
        if (paintimer == -1)playspr.color = new Color(1,1,1,1); //reset player color

        //sword
        if (Input.GetMouseButtonDown(0) && weapon == 0 && (swordcooldown < 1 || bettersword))
        {
            swordatking = 20;
            swordcooldown = 20;
            lookAt2D(sword, cam.ScreenToWorldPoint(Input.mousePosition));
            sword.transform.rotation = Quaternion.Euler(0, 0, sword.transform.rotation.eulerAngles.z - 30);
            {
                Color tempcol = Color.black;
                ColorUtility.TryParseHtmlString(player.mineralcolours[player.weaponminerals[0]], out tempcol);
                sword.GetComponent<SpriteRenderer>().color = tempcol;
            }
            if (bettersword) sword.GetComponent<power>().strength = 20;
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
            {
                Color tempcol = Color.black;
                ColorUtility.TryParseHtmlString(player.mineralcolours[player.weaponminerals[1]], out tempcol);
                bow.GetComponent<SpriteRenderer>().color = tempcol;
            }
        }
        else bow.SetActive(false);

        //gun
        if (reloads > 0 && reloadtimer < 1)
        {
            if (Input.GetMouseButtonDown(0) && weapon == 2)
            {
                GameObject.Instantiate(bulet, gun.transform);
                reloads--;
            }
        }
        if (reloads < 1)
        {
            reloads = 6;
            reloadtimer = 40;
        }
        if (weapon == 2)
        {

            gun.SetActive(true);
            lookAt2D(gun, cam.ScreenToWorldPoint(Input.mousePosition));
            gun.transform.localScale = (gun.transform.rotation.eulerAngles.z < 180) ? new Vector3(-1,1,1) :  new Vector3(1, 1, 1);
            {
                Color tempcol = Color.black;
                ColorUtility.TryParseHtmlString(player.mineralcolours[player.weaponminerals[2]], out tempcol);
                gun.GetComponent<SpriteRenderer>().color = tempcol;
                if (reloadtimer > 0) gun.GetComponent<SpriteRenderer>().color = tempcol - new Color(0.1f, 0.1f, 0.1f, 0);
            }
        }
        else gun.SetActive(false);

        //boomerang
        if (boomerangs > 0)
        {
            if (Input.GetMouseButtonDown(0) && weapon == 3)
            {
                GameObject.Instantiate(boomer, boomeranger.transform);
                boomerangs--;
            }
        }
        if (weapon == 3)
        {
            boomeranger.SetActive(true);
            lookAt2D(boomeranger, cam.ScreenToWorldPoint(Input.mousePosition));
            {
                Color tempcol = Color.black;
                ColorUtility.TryParseHtmlString(player.mineralcolours[player.weaponminerals[3]], out tempcol);
                boomeranger.GetComponent<SpriteRenderer>().color = tempcol;
                if (boomerangs < 1) boomeranger.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0);
            }
        }
        else boomeranger.SetActive(false);

        //magic
        if (magiccooldown < 1)
        {
            if (Input.GetMouseButtonDown(0) && weapon == 4)
            {
                GameObject.Instantiate((magictype == 0)? firey: (magictype == 1) ? elecy  : earthy, spell.transform);
                magiccooldown = 20;
            }
        }
        if (weapon == 4)
        {
            //if (magiccooldown > 0) spell.SetActive(false);
            spell.SetActive(true);
            lookAt2D(spell, cam.ScreenToWorldPoint(Input.mousePosition));
            {
                Color tempcol = Color.black;
                ColorUtility.TryParseHtmlString(player.mineralcolours[player.weaponminerals[4]], out tempcol);
                spell.GetComponent<SpriteRenderer>().color = tempcol;
            }
        }
        else spell.SetActive(false);


        if (Input.GetMouseButtonDown(1)) weapon=weapons[((++weaponindex)%weaponcount)];
    }
    private void FixedUpdate()
    {

        if (swordatking > 0) swordatking--;
        if (bettersword) if (swordatking > 0) swordatking--;
        if (swordcooldown > 0) swordcooldown--;
        if (reloadtimer > 0) reloadtimer--;
        if (magiccooldown > 0) magiccooldown--;
        if (swordatking > 0) sword.transform.rotation = Quaternion.Euler(0, 0, sword.transform.rotation.eulerAngles.z + 6);
        if (swordatking > 0) sword.SetActive(true);
        else sword.SetActive(false);

        if ((Input.GetAxisRaw("x") == 0 && Input.GetAxisRaw("y") == 0))
        {
            health = Mathf.Clamp(health + 0.1f, 0f, 100f);
        }

        if (bowatking > 0 && Input.GetMouseButton(0) && weapon == 1)
        {
            bowatking--;
            bowatking--;
            bowatking--;
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

        if (collision.gameObject.layer == 6 && collision.gameObject.name == "boomerang(Clone)") 
        {
            if (collision.gameObject.GetComponent<boomerang>().speed < 0)
            {
                boomerangs++;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 && paintimer < 0) // enemy
        {
            health -= ((power)(collision.gameObject.GetComponent("power"))).strength;
            paintimer = ((power)(collision.gameObject.GetComponent("power"))).strength * 3;
            playspr.color = new Color(0.6f, 0, 0, 1);
        }
    }

    private void RandomizeWorld() 
    {

        for (int i = 0; i < 4; i++) 
        {

            mineraltype[i] = Random.Range(1, minerals.Length);
            ColorUtility.TryParseHtmlString(mineralcolours[mineraltype[i]], out mineralcolourtype[i]);
            ColorUtility.TryParseHtmlString(colours[Random.Range(0, colours.Length)], out worldcolour[i]);
            ColorUtility.TryParseHtmlString(colours[Random.Range(0, colours.Length)], out watercolour[i]);
            ColorUtility.TryParseHtmlString(colours[Random.Range(0, colours.Length)], out enemycolour[i]);

            enemylook[i] = Random.Range(0,6);
            enemybehaviour[i] = Random.Range(0,3);
            renemybehaviour[i] = Random.Range(0,3);
            worldlayouts[i] = Random.Range(0,4);
            worldmusic[i] = Random.Range(0,5);
            cavemusic[i] = Random.Range(0,5);
            cavetype[i] = Random.Range(0, 4); //you can change this part if you want just don't while loop crash the thing
            worldweapons[i] = Random.Range(0, 5);
            recievedweapons[i] = false;
            weaponminerals[i] = 0;
            weapons[i] = 0;
            caves.Add(gd.generateCave(cavetype[currentworld], 0.005f * i));//change spawnrate as worldIndex increases
        }
        weapons[0] = 0;
        List<int> aaaa = new List<int>() { 1, 2, 3, 4 };
        for (int i = 0; i < 4; i++) 
        {
            int j = Random.Range(0, aaaa.Count);
            worldweapons[i] = aaaa[j];
            aaaa.RemoveAt(j);
        }
        worldweapons[Random.Range(0, 4)] = 0;
    }

    public static void OfferWeapon()
    {
        if (!recievedweapons[currentworld])
        {
            if (worldweapons[currentworld] != 0)
            {
                weapons[weaponcount] = worldweapons[currentworld];
                weaponcount++;
                if (worldweapons[currentworld] == 4) magictype = renemybehaviour[currentworld];
                recievedweapons[currentworld] = true;
            }
            else bettersword = true;
            weaponminerals[worldweapons[currentworld]] = mineraltype[currentworld];
            
        }
    }
}
