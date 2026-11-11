using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; //librería de DOTween
using UnityEngine.Events; //librería para usar UnityEvents

public class Fader : MonoBehaviour
{

    public Image fadeImage;
    public float fadeTime;

    public UnityEvent onFadeOutEvent;

    void Start()
    {
        fadeImage.raycastTarget = false; //asegurarse que la imagen no sea interactiva
        Color startColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        fadeImage.color = startColor; //asegurarse que eñ color de la imagen siempre inicie opaco.
        FadeOut();
    }

    public void FadeOut ()
    {
        fadeImage.DOFade(0, fadeTime).OnComplete(onFadeOutEvent.Invoke);
    }

    public void FadeIn()
    {
        fadeImage.DOFade(1, fadeTime);
    }

}
