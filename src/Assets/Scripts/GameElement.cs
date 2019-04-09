using System;
using UnityEngine;

namespace Assets.Scripts
{
        public class GameElement : BaseElement
        {
                private GameObject obj;

                public GameObject Obj
                {
                        get { return obj; }
                        set { obj = value; }
                }

                public GameElement()
                {
                }

                public GameElement(GameObject obj, int id)
                {
                        Obj = obj;
                        ID = id;
                }
        }
}