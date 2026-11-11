using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CubeTweem : MonoBehaviour
{
    [Header("MoveTween")]
    public float moveTime;
    public Transform destination;
    public KeyCode moveKey;

    [Header("FlickerTween")]
    public float flickerTime;
    public Color flickerColor;
    public Renderer rend;
    public int loop;

    public KeyCode flickerKey;
    private Tween flickerTween;

    void Update()
    {
        if(Input.GetKeyDown(moveKey))
        {
            Move();
        }
        if (Input.GetKeyDown(flickerKey))
        {
            Flicker();
        }


    }

    void Move()
    {
        transform.DOMove(destination.position, moveTime);
    }

    void Flicker()
    {
        flickerTween?.Kill(true);

        /*Tween t3 = */ flickerTween = rend.material.DOColor(flickerColor, flickerTime).
            SetLoops(loop, LoopType.Incremental );

    }

}
