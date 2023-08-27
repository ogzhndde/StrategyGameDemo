using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// The class that produces all particles that can be produced.
/// There is a main production class called ParticleFactory, and it provides the production of particles where necessary by pulling the specific data of the particle types.
/// There are 2 types of particles now: Hit and Explode particles.
/// If a new type of particle is to be added, it is sufficient to add it here and particle ScriptableObject from project files.
/// </summary>

namespace ParticleFactoryStatic
{
    public static class ParticleFactory
    {
        //It stores all particle types and production classes in a dictionary.
        private static Dictionary<ParticleType, Func<ParticleProperties>> particleFactories = new Dictionary<ParticleType, Func<ParticleProperties>>
        {
            { ParticleType.ExplodeParticle, () => new ParticleExplode() },
            { ParticleType.HitParticle, () => new ParticleHit() }
        };

        //The class in which particles are spawned.
        public static ParticleProperties SpawnParticle(ParticleType particleType, Vector3 spawnPosition)
        {
            if (particleFactories.TryGetValue(particleType, out var factory))
            {
                var particle = factory.Invoke();
                particle.SpawnParticle(particleType, spawnPosition);
                return particle;
            }
            else
            {
                return null;
            }
        }
    }

    //All necessary data for particles is drawn from scriptable objects and sent to the factory for production.
    public class ParticleExplode : ParticleProperties
    {
        GameManager manager;

        public override void SpawnParticle(ParticleType particleType, Vector3 spawnPosition)
        {
            manager = GameManager.Instance;
            var spawnedParticle = ObjectPoolManager.SpawnObjects(manager.SO.ParticleData.ExplodeParticle, spawnPosition, Quaternion.identity);

            EventManager.Broadcast(GameEvent.OnPlaySound, "SoundBuildingCollapse");
        }
    }

    //All necessary data for particles is drawn from scriptable objects and sent to the factory for production.
    public class ParticleHit : ParticleProperties
    {
        GameManager manager;

        public override void SpawnParticle(ParticleType particleType, Vector3 spawnPosition)
        {
            manager = GameManager.Instance;
            var spawnedParticle = ObjectPoolManager.SpawnObjects(manager.SO.ParticleData.HitParticle, spawnPosition, Quaternion.identity);

            EventManager.Broadcast(GameEvent.OnPlaySound, "SoundSwordHit" + Random.Range(1, 4));
        }
    }
}
