using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerang : MonoBehaviour
{
    GameObject boomobj;
    public GameObject spinny;
    public float speed;
    GameObject playobj;


    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        boomobj = this.gameObject;
        boomobj.transform.parent = null;
        playobj = GameObject.Find("Player");

        Color tempcol = Color.black;
        ColorUtility.TryParseHtmlString(player.mineralcolours[player.weaponminerals[3]], out tempcol);
        this.gameObject.GetComponent<SpriteRenderer>().color = tempcol;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        boomobj.transform.position += boomobj.transform.up * speed/10f;
        speed -= 0.1f;
        if (speed < 0) boomobj.transform.position = Vector3.MoveTowards(boomobj.transform.position, playobj.transform.position, -speed);
        this.gameObject.GetComponent<power>().strength = Mathf.Abs((int)speed * 2);
    }

    private void Update()
    {
        spinny.transform.rotation = Quaternion.Euler(0, 0, spinny.transform.rotation.eulerAngles.z + speed*10);
    }
}
