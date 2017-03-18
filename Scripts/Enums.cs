using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class EnumsAndConstants
    {
        public enum SpellDirection
        {
            None,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft,
            Left,
            UpLeft,
            Up
        }
        public enum Mood
        {
            Normal,
            Angry
        }

        public enum EffectType
        {
            Haste,
            Shield,
            Normal,
            Poison,
            Burn,
            Freeze,
            Stunned,
            Fear,
            Slow
        }

        public enum BuffType
        {
            None,
            Haste,
            Power,
            Shield,
            Healing,
            MagicReguvination
        }
    }
}