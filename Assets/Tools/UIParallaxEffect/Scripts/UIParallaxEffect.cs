using UnityEngine;

namespace UIParallax
{
    public class UIParallaxEffect : MonoBehaviour
    {
        [SerializeField] private bool autoUpdate = true;
        [SerializeField] private UIParallaxLayer[] parallaxLayers = null;

        private Vector3[][] startPosition = null;

        private void Start()
        {
            if (autoUpdate)
            {
                Initialize();
            }
        }

        private void Update()
        {
            if (autoUpdate)
            {
                RefreshParallaxEffect();
            }
        }

        public void Initialize()
        {
            startPosition = new Vector3[parallaxLayers.Length][];

            for (int i = 0; i < parallaxLayers.Length; i++)
            {
                int objectsCount = parallaxLayers[i].ObjectsRect.Length;
                startPosition[i] = new Vector3[objectsCount];

                for (int j = 0; j < objectsCount; j++)
                {
                    startPosition[i][j] = parallaxLayers[i].ObjectsRect[j].anchoredPosition;
                }
            }
        }

        public void RefreshParallaxEffect()
        {
            Vector3 input = Input.acceleration;

            for (int i = 0; i < parallaxLayers.Length; i++)
            {
                int countObjects = parallaxLayers[i].ObjectsRect.Length;

                for (int j = 0; j < countObjects; j++)
                {
                    float posX = Mathf.Clamp(input.x * parallaxLayers[i].PosXMultiplier + startPosition[i][j].x, 
                        -parallaxLayers[i].PosXMultiplier + startPosition[i][j].x, 
                        parallaxLayers[i].PosXMultiplier + startPosition[i][j].x);
                    float posY = Mathf.Clamp(input.y * parallaxLayers[i].PosYMultiplier + startPosition[i][j].y, 
                        -parallaxLayers[i].PosYMultiplier + startPosition[i][j].y, 
                        parallaxLayers[i].PosYMultiplier + startPosition[i][j].y);

                    float rotX = Mathf.Clamp(-input.y * parallaxLayers[i].RotationMultiplier, -parallaxLayers[i].RotationMultiplier, parallaxLayers[i].RotationMultiplier);
                    float rotY = Mathf.Clamp(input.x * parallaxLayers[i].RotationMultiplier, -parallaxLayers[i].RotationMultiplier, parallaxLayers[i].RotationMultiplier);

                    parallaxLayers[i].ObjectsRect[j].anchoredPosition = 
                        Vector3.Lerp(parallaxLayers[i].ObjectsRect[j].anchoredPosition, new Vector2(posX, posY), parallaxLayers[i].InterpolationSpeed);

                    parallaxLayers[i].ObjectsRect[j].localRotation 
                        = Quaternion.Lerp(parallaxLayers[i].ObjectsRect[j].localRotation, Quaternion.Euler(rotX, rotY, 0f), parallaxLayers[i].InterpolationSpeed);
                }
            }
        }
    }
}