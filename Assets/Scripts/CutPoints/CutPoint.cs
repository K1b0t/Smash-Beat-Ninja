using UnityEngine;

public class CutPoint : MonoBehaviour
{
    public CutPointRenderer cutPointRenderer;

    public void RemovePoint()
    {
        cutPointRenderer.CheckIfAllPointsCut(this);
    }
}
