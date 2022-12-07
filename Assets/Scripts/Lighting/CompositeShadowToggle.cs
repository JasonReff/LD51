using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace StormlightManor
{
    [RequireComponent(typeof(CompositeShadowCaster2D))]
    [ExecuteInEditMode]
    public class CompositeShadowToggle : MonoBehaviour
    {
        private CompositeShadowCaster2D _shadowCaster;
        private void OnEnable()
        {
            _shadowCaster = GetComponent<CompositeShadowCaster2D>();
            if (GetComponentsInChildren<ShadowCaster2D>().Length > 0)
            {
                _shadowCaster.enabled = true;
            }
            else _shadowCaster.enabled = false;
        }

        private void OnDisable()
        {
            _shadowCaster.enabled = false;
        }
    }
}
