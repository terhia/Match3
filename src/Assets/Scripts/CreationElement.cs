using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
        [Serializable]
        public class CreationElement : BaseElement
        {
                //[Tooltip("Процент появления элемента")] [SerializeField] public int percentOfAppearance;

                //public int PercentOfAppearance
                //{
                //        // ReSharper disable once ConvertPropertyToExpressionBody //only C# 4.0+
                //        get { return percentOfAppearance; }
                //}

                [Tooltip("Картинка создаваемого элемента")] [SerializeField] private Image image;

                public Image Image
                {
                        // ReSharper disable once ConvertPropertyToExpressionBody //only C# 4.0+
                        get { return image; }
                }

        }
}