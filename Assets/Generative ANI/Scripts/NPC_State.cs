using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State
{
    public enum AnimateState
    {
        Idle,
        Walk,
        Run,
        Jump,
        Attack,
        Die
    }

    public enum AttitudeState
    {
        Friendly,
        Neutral,
        Adversarial,
        Hostile
    }

    public enum CharacterGender
    {
        Male,
        Female,
        NonBinary,
        Unknown
    }

    public enum Genre
    {
        Fantasy,
        SciFi,
        Horror,
        Romance,
        Mystery,
        Thriller,
        Comedy,
        Drama,
        Action,
        Adventure,
        Historical,
        Western,
        NonFiction,
        Unknown
    }

    public class NPCState
    {
        public AnimateState animateState;
        public AttitudeState attitudeState;
        public bool isAttacking;
        public bool isDead;
        public bool isJumping;
        public bool isWalking;
        public bool isRunning;
        public bool isIdle;
    }
}
