using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class elec : MonoBehaviour
{
    public bool evil;
    public GameObject playobj;
    int alivetime;
    Vector3 direction;
    public GameObject Light;

    // Start is called before the first frame update
    void Start()
    {
        alivetime = 160;
        this.transform.parent = null;
        playobj = GameObject.Find("Player");
        if (evil) direction = (playobj.transform.position - this.transform.position).normalized;
        else direction = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0) - this.transform.position).normalized;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        this.gameObject.layer = (evil) ? 8 : 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (alivetime < 155)
        {
            this.transform.position += direction * 8f * Time.deltaTime;
        }

        if (evil && this.gameObject.GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("weapon"))) { alivetime = -999; }

        if (evil) this.gameObject.GetComponent<SpriteRenderer>().color = player.mineralcolourtype[player.currentworld];

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
