using UnityEngine;

public class LightSource : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ToggleLight>(out ToggleLight toggleLight))
        {
            toggleLight.ChangeVisibility(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ToggleLight>(out ToggleLight toggleLight))
        {
            toggleLight.ChangeVisibility(false);
        }
    }
}