using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ToggleLight : MonoBehaviour
{

    public void ChangeVisibility(bool visibility)
    {
        if (visibility)
            gameObject.layer = 6;
        else
            gameObject.layer = 7;
    }

}
