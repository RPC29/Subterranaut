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
        speed = 7f;
        bulobj = this.gameObject;
        bulobj.transform.parent = null;

        Color tempcol = Color.black;
        ColorUtility.TryParseHtmlString(player.mineralcolours[player.weaponminerals[2]], out tempcol);
        this.gameObject.GetComponent<SpriteRenderer>().color = tempcol;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulobj.transform.position += bulobj.transform.up * speed/10f;
        speed -= 0.1f;
        if (speed < 0.1) Destroy(this.gameObject);
        this.gameObject.GetComponent<power>().strength = (int)speed * 3;
    }
}
