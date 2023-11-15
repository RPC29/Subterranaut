using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    GameObject arrobj;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        speed = player.laststate * 7f/4f;
        arrobj = this.gameObject;
        arrobj.transform.parent = null;

        Color tempcol = Color.black;
        ColorUtility.TryParseHtmlString(player.mineralcolours[player.weaponminerals[1]], out tempcol);
        this.gameObject.GetComponent<SpriteRenderer>().color = tempcol;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        arrobj.transform.position += arrobj.transform.up * speed/10f;
        speed -= 0.1f;
        if (speed < 0.1) Destroy(this.gameObject);
        this.gameObject.GetComponent<power>().strength = (int)speed * 2;
    }
}
