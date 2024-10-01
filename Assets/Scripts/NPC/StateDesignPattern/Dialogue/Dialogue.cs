using System.Collections.Generic;
using UnityEngine;

public static class Dialogue{
    internal static Queue<string> DarianDialogue = new Queue<string>();
    internal static Queue<string> CesiyaDialogue = new Queue<string>();
    internal static List<string> LyraHintDialogue = new List<string>{
        "Use C to talk to NPC",
        "Use Left click to Attack! Aim first with Right click if you're a wizard",
        "Try clicking [F] or [R] 0-0b",
        "There are 3 NPC, do what you do best!",
        "You're ready! Go straight to the portal!"
    };

    public static void CheckQueue(){
        if(CesiyaDialogue.Count == 0){
            CesiyaDialogue.Enqueue("Please help save this town!");
            CesiyaDialogue.Enqueue("iloveyou");
            CesiyaDialogue.Enqueue("ihateyou");
            CesiyaDialogue.Enqueue("b u d i");
            CesiyaDialogue.Enqueue("Ko FT ganteng maksimal");
        }
        if(DarianDialogue.Count == 0){
            DarianDialogue.Enqueue("You got the power!");
            DarianDialogue.Enqueue("sayasayangangkatan221");
            DarianDialogue.Enqueue("231bebal");
            DarianDialogue.Enqueue("hesoyam");
            DarianDialogue.Enqueue("NJ best asisten hiya");
        }
    }
}