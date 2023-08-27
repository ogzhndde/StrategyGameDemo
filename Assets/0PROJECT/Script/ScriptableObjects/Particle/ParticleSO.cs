using UnityEngine;

/// <summary>
/// Scriptable object that holds data of particles
/// </summary>

[CreateAssetMenu(fileName = "ParticleData", menuName = "Particle/Particle Data", order = 1)]
public class ParticleSO : ScriptableObject
{
    public GameObject ExplodeParticle;
    public GameObject HitParticle;
}
