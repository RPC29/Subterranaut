using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth : MonoBehaviour
{
    public bool evil;
    public GameObject playobj;
    public GameObject enemyobj;
    public GameObject sprite;
    int alivetime;

    // Start is called before the first frame update
    void Start()
    {
        playobj = GameObject.Find("Player");
        if (!evil) this.transform.parent = playobj.transform;
        alivetime = 150;
        if (evil)enemyobj = transform.parent.gameObject;
        if (!evil)transform.position += (new Vector3 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0) - this.transform.position).normalized * 2f;
        else transform.position -= new Vector3(0, -2f, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), 240f * Time.deltaTime);
        if (evil && this.gameObject.GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("weapon"))) { alivetime = -999; }
        sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (evil) sprite.gameObject.GetComponent<SpriteRenderer>().color = player.mineralcolourtype[player.currentworld];

        else
        {
            Color a = Color.black;
            ColorUtility.TryParseHtmlString(player.mineralcolours[player.weaponminerals[4]], out a);
            sprite.GetComponent<SpriteRenderer>().color = a;
        }
    }

    private void FixedUpdate()
    {
        alivetime--;
        if (alivetime < 0) Destroy(this.gameObject);
    }
}
