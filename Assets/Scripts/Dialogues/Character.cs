using System;
using UnityEngine;

namespace Dialogue_Helper
{
    public class Character 
    {
        public Sprite[] Character_Images;
        [Serializable]
        public struct Char_Info
        {
            public string Name;
            public Sprite Profile;
            public Position pos;
        }
        public enum Position
        {
            Left,
            Center,
            Right
        }
    }
}