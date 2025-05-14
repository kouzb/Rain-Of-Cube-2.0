using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
[RequireComponent (typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private MeshRenderer _meshRenderer;
    private float _colorChangeDuration = 3f;

    public event Action<Bomb> Exploded;

    private void Start()
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
        Color startColor = _meshRenderer.material.color;
        float timer = 0f;

        while(timer < _colorChangeDuration )
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / _colorChangeDuration );
            _meshRenderer.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        Explode();
        yield return null;  
        gameObject.SetActive(false);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        Exploded?.Invoke(this);
    }

    private void OnDisable()
    {
        StopCoroutine(ChangeColor());
    }
}
