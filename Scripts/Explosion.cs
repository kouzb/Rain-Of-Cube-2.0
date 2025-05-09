using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private Pool<Bomb> _bombPool;

    public void Initialize(Pool<Bomb> bombPool)
    {
        _bombPool = bombPool;
    }

    public void Explode(Bomb bomb)
    {
        float explosionRadius = CalculateRadius(bomb);
        float explosionForce = CalculateForce(bomb);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        Material bombMaterial = bomb.GetMaterial();
        Color color = bombMaterial.color;

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(explosionForce, bomb.transform.position, explosionRadius);
            }
        }

        _bombPool.Release(bomb);
    }

    private float CalculateForce(Bomb bomb)
    {
        return _explosionForce / bomb.transform.localScale.magnitude;
    }

    private float CalculateRadius(Bomb bomb)
    {
        return _explosionRadius * bomb.transform.localScale.magnitude;
    }
}