using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ore1, ore2, oreKey;
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
        Instantiate(oreKey, player.caves[currentworld][3][0]+new Vector2(0.5f,0.5f), Quaternion.identity);
        gd.visualizeCave(player.caves[currentworld][0], player.worldcolour[currentworld]);
        GameObject.Find("bgSquare").GetComponent<SpriteRenderer>().sprite=darkdirt;
        GameObject.Find("bgSquare").GetComponent<SpriteRenderer>().color=player.worldcolour[currentworld];
    }
}
