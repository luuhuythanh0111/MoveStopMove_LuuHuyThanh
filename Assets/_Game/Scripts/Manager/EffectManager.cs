using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] private ParticleSystem deathParticlePrefab;
    [SerializeField] private ParticleSystem bloodParticlePrefab;
    [SerializeField] private ParticleSystem scaleUpParticlePrefab;

    [SerializeField] private GameObject wayPointMarkerCanvas;

    public void PlayBloodParticle(Transform target)
    {
        ParticlePool.Play(bloodParticlePrefab, target.position, target.rotation);
    }

    public void PlayDeathParticle(Transform target)
    {
        ParticlePool.Play(deathParticlePrefab, target.position, target.rotation);
    }

    public void PlayScaleUpParticle(Transform target)
    {
        ParticlePool.Play(scaleUpParticlePrefab, target.position, target.rotation);
    }

    public void WayPointMarkerSetActive(bool isTrue)
    {
        if(isTrue == true)
        {
            wayPointMarkerCanvas.SetActive(true);
        }
        else
        {
            wayPointMarkerCanvas.SetActive(false);
        }
    }
}
