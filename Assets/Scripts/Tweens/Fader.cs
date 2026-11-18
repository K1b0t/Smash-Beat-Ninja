using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class Fader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeTime = 1f;

    public UnityEvent onFadeOutEvent; 
    public UnityEvent onFadeInEvent; 

    void Start()
    {
        fadeImage.raycastTarget = false;
        Color startColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        fadeImage.color = startColor;
        FadeOut();
    }

    public void FadeOut()
    {
        fadeImage.DOFade(0, fadeTime).OnComplete(() => onFadeOutEvent?.Invoke());
    }

    public void FadeIn()
    {
        fadeImage.raycastTarget = true; // Bloquea clics mientras oscurece
        fadeImage.DOFade(1, fadeTime).OnComplete(() => onFadeInEvent?.Invoke());
    }
}
