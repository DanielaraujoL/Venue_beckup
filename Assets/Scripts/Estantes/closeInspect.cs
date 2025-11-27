using UnityEngine;

public class CloseInspect : MonoBehaviour
{
    public GameObject inspectCanvas;

    public void Close()
    {
        inspectCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
