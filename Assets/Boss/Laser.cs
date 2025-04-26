using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float laserRange = 999f;
    [SerializeField] private Transform laserEnd;
    [SerializeField] private Transform laserStart;
    [SerializeField] private float laserDuration = 2f;
    [SerializeField] private LayerMask _layermask;

    [SerializeField] private ParticleSystem _contactParticles;
    private LineRenderer _lineRenderer;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void FireLaser()
    {

        RaycastHit hitinfo;
        if (Physics.Linecast(laserStart.position, (laserEnd.position - laserStart.position).normalized * laserRange, out hitinfo, _layermask))
        {

            _lineRenderer.SetPosition(1, hitinfo.point);
            if (hitinfo.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.OnDamage(.1f);
            }
            ContactFeedback(hitinfo.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, (laserEnd.position - laserStart.position).normalized * laserRange);
        }

        _lineRenderer.SetPosition(0, laserStart.position);

        print("test");


    }


    private void ContactFeedback(Vector3 hitpoint)
    {
        //particles
        if (_contactParticles != null)
        {
            _contactParticles = Instantiate(_contactParticles, hitpoint, Quaternion.identity);
        }

    }

}
