﻿using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    private List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ToggleLight>(out ToggleLight lightToggle))
        {
            lightToggle.ChangeVisibility(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ToggleLight>(out ToggleLight lightToggle))
        {
            lightToggle.ChangeVisibility(false);
        }
    }
}