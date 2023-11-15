using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ore1, ore2, oreKey, portalClosed, portalOpen, exit;
    public Sprite darkdirt;
    public player player;
    public GenerateDungeons gd;
    void Awake()
    {
        player=GameObject.Find("Player").GetComponent<player>();
        int currentworld=player.currentworld;
        gd.clearTiles();
        ore1.GetComponent<SpriteRenderer>().color=player.mineralcolourtype[currentworld];
        ore2.GetComponent<SpriteRenderer>().color=player.mineralcolourtype[currentworld];

        gd.addEnemies(player.caves[currentworld][1]);
        List<Vector2> ores = player.caves[currentworld][2];

        foreach(Vector2 ore in ores)
            Instantiate(Random.value>=0.5?ore1:ore2, ore+new Vector2(0.5f,0.5f), Quaternion.identity);
        player.gameObject.transform.position = player.caves[currentworld][4][0];
        int dir = Random.Range(0, 5);
        Instantiate(exit, (player.transform.position + new Vector3(0,-1, 0)), Quaternion.Euler(0,0,0) , null);
        if(!player.haskeyranium && !player.haskey) Instantiate(oreKey, player.caves[currentworld][3][0]+new Vector2(0.5f,0.5f), Quaternion.identity);
        gd.visualizeCave(player.caves[currentworld][0], player.worldcolour[currentworld]);
        GameObject.Find("bgSquare").GetComponent<SpriteRenderer>().sprite=darkdirt;
        GameObject.Find("bgSquare").GetComponent<SpriteRenderer>().color=player.worldcolour[currentworld];

        if (player.haskey)
        Instantiate(portalOpen, player.caves[currentworld][5][0], Quaternion.identity);
        else
        Instantiate(portalClosed, player.caves[currentworld][5][0], Quaternion.identity);


    }
}
