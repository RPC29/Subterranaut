using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villager : MonoBehaviour
{
    public int villagertype; //0 - tutorial, 1 - blacksmith/swordsmith, 2 - keysmith
    int dialogueset = 0;
    string[] lines = {"Hello Traveller! (Left click to continue)", "It appears you need to activate the portal. Unfortunately, that requires a rare mineral called keyranium to be actived", "You can mine keyranium by attacking it, bring me some keyranium and I'll forge you a key"/**/, "Ah, keyranium! Here's your key. You can activate the portal now.", "You can find keyranium across all dimensions so don't worry about getting stuck.", "Good luck on your journeys!",/**/ "Welcome to our humble dimension, travller. I notice that you have a wooden sword.", "Our dimension is known for it's amazing swords. Having yours be present here is just an insult to us", "I can upgrade this sword, if you get me 10 $minerals", /**/"Ah, $mineral! Here's your upgraded sword.", "It swings faster and deals more damage now.", "You can finally throw away that garbage you called a wooden sword",/**/ "Hello fellow traveler. Did you know that our dimension is known for its exquisite $weapons made out of $mineral", "And I am the one responsible for crafting them.","You know what? Bring me some $mineral and I'll make you a $weapon out of it", /**/"Ah %mineral! Here is your $weapon", "Never forget who gave it to you!","Good luck on your journeys, traveller!", /**/"Welcome traveller, to our magical village.", "The creatures you'll find underground, have stolen the power of $minerals found here", "Bring me some $minerals and I can brew a potion to givw you the same powers.",/**/ "Ah, $mineral. Here is your potion", "You could have taken your time you had no need to chug it but alright", "Have fun fighting enemies with the same weapons they use against you",/**/"It is I, the great keysmith.", "Bring me some keyranium and I will forge a key out of it.","You can use that key to activate the portal", /**/"Ah! Keyranium, here is your key.", "You can use this to activate the portal", "Good luck on your journeys traveller" };
    string message = "";
    int outof3 = 0;
    bool speaking = false;
    bool haswhatIwant = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!speaking) outof3 = 0;

        if (Input.GetMouseButtonDown(0)) outof3 = ((outof3 + 1) % 3);
        if (villagertype == 0 && !haswhatIwant) dialogueset = 0;
        else if (villagertype == 0 && haswhatIwant) dialogueset = 1;
        else if (villagertype == 1 && !haswhatIwant && player.worldweapons[player.currentworld] == 0) dialogueset = 2;
        else if (villagertype == 1 && haswhatIwant && player.worldweapons[player.currentworld] == 0) dialogueset = 3;
        else if (villagertype == 1 && !haswhatIwant && player.worldweapons[player.currentworld] == 4) dialogueset = 6;
        else if (villagertype == 1 && haswhatIwant && player.worldweapons[player.currentworld] == 4) dialogueset = 7;
        else if (villagertype == 2 && !haswhatIwant) dialogueset = 8;
        else if (villagertype == 2 && haswhatIwant) dialogueset = 9;
        else if (!haswhatIwant) dialogueset = 5;
        else dialogueset = 4;

        message = lines[(dialogueset * 3) + outof3];
        player.dialogue = message.Replace("$mineral", player.minerals[player.currentworld]).Replace("$weapon", player.weaponnames[player.worldweapons[player.currentworld]]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) 
        {
            player.speaking = true;
            speaking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            player.speaking = false;
            speaking = false;
        }
    }
}
