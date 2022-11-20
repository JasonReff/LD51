using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering.Universal;

public class CustomCompositeShadow : CompositeShadowCaster2D
{
    private List<ShadowCaster2D> _shadowCasters = new List<ShadowCaster2D>();
    // Start is called before the first frame update
    void Start()
    {
        _shadowCasters = GetComponentsInChildren<ShadowCaster2D>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
