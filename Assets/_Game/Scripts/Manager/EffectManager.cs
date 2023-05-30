using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] private ParticleSystem deathParticlePrefab;
    [SerializeField] private ParticleSystem bloodParticlePrefab;


    public void PlayBloodParticle(Transform target)
    {
        ParticlePool.Play(bloodParticlePrefab, target.position, target.rotation);
    }

    public void PlayDeathParticle(Transform target)
    {
        ParticlePool.Play(deathParticlePrefab, target.position, target.rotation);
    }
}
