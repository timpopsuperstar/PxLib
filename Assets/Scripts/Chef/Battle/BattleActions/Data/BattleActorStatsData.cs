using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleActorStatsData 
{
    public static BattleActorStats Hero => new BattleActorStats
    (
        BattleActorStats.Type.Player,
        50, //Hp
        0, //Bp
        10, //Speed
        5 //Power
    );
    public static BattleActorStats Egg => new BattleActorStats
    (
        BattleActorStats.Type.Enemy,
        50, //Hp
        0, //Bp
        10, //Speed
        5 //Power
    );
}
