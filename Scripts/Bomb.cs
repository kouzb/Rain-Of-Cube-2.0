using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
[RequireComponent (typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _colorChangeDuration = 3f;

    public event Action<Bomb> Exploded;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(ChangeColor());
    }

    public Material GetMaterial()
    {
        return _meshRenderer.material;
    }

    private IEnumerator ChangeColor()
    {
        float timer = 0f;
        Color startColor = _meshRenderer.material.color;

        while(timer < _colorChangeDuration )
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / _colorChangeDuration );
            _meshRenderer.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        Exploded?.Invoke(this);
        yield return null;  
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopCoroutine(ChangeColor());
    }
}
