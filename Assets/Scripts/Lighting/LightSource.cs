using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSource : MonoBehaviour, IClassicLight
{
    private List<Collider2D> contacts = new List<Collider2D>();
    [SerializeField] private Animator _animator;
    private bool _isSnuffed;
    public bool IsSnuffed { get => _isSnuffed; }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        Relight();
        if (_animator != null)
            _animator.SetBool("Lit", true);
    }

    private void OnDisable()
    {
        SnuffOut();
        if (_animator != null)
            _animator.SetBool("Lit", false);
    }

    public void Snuff()
    {
        enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isSnuffed)
            return;
        if (collision.TryGetComponent<ToggleLight>(out ToggleLight lightToggle))
        {
            if (lightToggle.enabled)
                lightToggle.ChangeVisibility(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ToggleLight>(out ToggleLight lightToggle))
        {
            if (lightToggle.enabled)
                lightToggle.ChangeVisibility(false);
        }
    }

    private void SnuffOut()
    {
        _isSnuffed = true;
        SetContacts(false);
    }

    private void SetContacts(bool visible)
    {
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), contacts);
        foreach (var contact in contacts)
            if (contact.TryGetComponent(out ToggleLight light))
                light.ChangeVisibility(visible);
    }

    private void Relight()
    {
        if (_isSnuffed)
        {
            _isSnuffed = false;
            SetContacts(true);
        }
    }

    public void SetClassicMode(bool classic)
    {
        if (TryGetComponent(out Light2D light))
        {
            light.enabled = !classic;
        }
        if (!classic)
        {
            enabled = false;
            SetContacts(true);
        }
    }
}
