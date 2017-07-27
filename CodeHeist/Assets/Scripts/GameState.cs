using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState  {

    public static string causeOfFail;
    public static string message;
    public static int index;
    public enum LoseCondition { Wall, Trap, Door };
    public static LoseCondition loseCondition;

   
}
