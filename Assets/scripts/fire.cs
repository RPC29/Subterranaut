using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class fire : MonoBehaviour
{
    public bool evil = true;
    public GameObject playobj;
    public GameObject Light;
    int alivetime;

    // Start is called before the first frame update
    void Start()
    {
        alivetime = 240;
        this.transform.parent = null;
        playobj = GameObject.Find("Player");


        this.gameObject.layer = (evil) ? 8 : 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (evil) this.transform.position = Vector3.MoveTowards(this.transform.position, playobj.transform.position, 4f * Time.deltaTime);
        else this.transform.position = Vector3.MoveTowards(this.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 20f * Time.deltaTime);

        if (evil && this.gameObject.GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("weapon"))) { alivetime = -999; }

        if (evil)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = player.mineralcolourtype[player.currentworld];
        }

        else
        {
            Color a = Color.black;
            ColorUtility.TryParseHtmlString(player.mineralcolours[player.weaponminerals[4]], out a);
            this.gameObject.GetComponent<SpriteRenderer>().color = a;
        }

        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 8) Light.SetActive(true);
        else Light.SetActive(false);
    }

    private void FixedUpdate()
    {
        alivetime--;
        if (alivetime < 0) Destroy(this.gameObject);
    }
}
