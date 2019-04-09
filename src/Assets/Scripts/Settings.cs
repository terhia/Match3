using UnityEngine;

namespace Assets.Scripts
{
        public class Settings : ScriptableObject
        {
                [Tooltip("Количество элементов по горизонтали")]
                public int M = 6;

                [Tooltip("Количество элементов по вертикали")]
                public int N = 7;
        }
}