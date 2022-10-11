using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    private List<Collider2D> contacts = new List<Collider2D>();
    private bool _isSnuffed;
    public bool IsSnuffed { get => _isSnuffed; }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        _isSnuffed = false;
    }

    private void OnDisable()
    {
        SnuffOut();
    }

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

    private void SnuffOut()
    {
        _isSnuffed = true;
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), contacts);
        foreach (var contact in contacts)
            if (contact.TryGetComponent(out ToggleLight light))
                light.ChangeVisibility(false);
    }
}
