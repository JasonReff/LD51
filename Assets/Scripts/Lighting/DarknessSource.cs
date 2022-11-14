using System.Collections.Generic;
using UnityEngine;

public class DarknessSource : MonoBehaviour, IClassicLight
{
    private List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
    [SerializeField] private ParticleSystem _smokeSystem;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ToggleLight>(out ToggleLight lightToggle))
        {
            if (lightToggle.enabled)
                lightToggle.ChangeVisibility(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ToggleLight lightToggle))
        {
            if (lightToggle.enabled)
                lightToggle.OnDarknessLeft();
        }
    }

    public void SetClassicMode(bool classic)
    {
        if (classic)
        {
            _smokeSystem.Stop();
        }
        else
        {
            _smokeSystem.Play();
            enabled = false;
        }
    }
}