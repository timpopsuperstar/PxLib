using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyGenerics
{
    //Actions
    public abstract class Action<T>
    {
        public abstract void Act(T t);
    }

    public class BattleAction<T> : Action<T> where T : BattleActor
    {
        public override void Act(T t) { }
    }
    public class BattleAction : BattleAction<BattleActor> { }
    public class HeroAction : BattleAction{ }

    public class EnemyAction : BattleAction { }


    //Actors
    public abstract class Actor { }
    public class BattleActor : Actor { }
    public class Hero : BattleActor { }
    public class Enemy : BattleActor { }

    public interface IState<T,U> where T : BattleActor where U : BattleAction<T>
    {
        T Actor { get; set; }
        U Action { get; set; }
    }
    public class BattleActorState : IState<BattleActor, BattleAction<BattleActor>>
    {
        public BattleActor actor;

        public BattleActor Actor { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public BattleAction<BattleActor> Action { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
    public class Demo
    {
        private void RunDemo()
        {
            var hero = new Hero();
            var enemy = new Enemy();
            var battleactor = new BattleActor();
            var battleAction = new BattleAction();
            var heroAction = new HeroAction();
            var enemyAction = new EnemyAction();

            battleAction.Act(hero);
            battleAction.Act(enemy);
            heroAction.Act(hero);
            enemyAction.Act(enemy);

            var state = new BattleActorState();
            state.actor = enemy;
            state.actor = hero;
            state.Action = battleAction;
        }
    }
}
