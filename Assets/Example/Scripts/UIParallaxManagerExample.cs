using UnityEngine;
using UIParallax;

[RequireComponent(typeof(UIParallaxEffect))]
public class UIParallaxManagerExample : MonoBehaviour
{
    [SerializeField] private UIParallaxEffect parallaxEffect;

    private void Start()
    {
        if (parallaxEffect == null)
            parallaxEffect = GetComponent<UIParallaxEffect>();

        parallaxEffect.Initialize();
    }

    private void Update()
    {
        parallaxEffect.RefreshParallaxEffect();
    }
}