using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ParticleFactoryStatic
{
    public static class ParticleFactory
    {
        private static Dictionary<ParticleType, Func<ParticleProperties>> particleFactories = new Dictionary<ParticleType, Func<ParticleProperties>>
        {
            { ParticleType.ExplodeParticle, () => new ParticleExplode() },
            { ParticleType.HitParticle, () => new ParticleHit() }
        };

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
