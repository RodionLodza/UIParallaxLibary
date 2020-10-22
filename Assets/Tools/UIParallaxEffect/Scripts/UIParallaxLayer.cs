using UnityEngine;
using System;

namespace UIParallax
{
    [Serializable]
    public class UIParallaxLayer
    {
        public string ParallaxLayerName;
        public RectTransform[] ObjectsRect;
        public float PosXMultiplier;
        public float PosYMultiplier;
        public float RotationMultiplier;
        public float InterpolationSpeed;
    }
}