namespace MythicalBattles
{
    public class AncientWarriorShooter : Shooter
    {
        protected override void Shoot()
        {
            Arrow arrow = (Arrow)ProjectilePool.GetItem();
            ParticleEffect particle = (ParticleEffect)EffectPool.GetItem();

            arrow.gameObject.SetActive(true);
            arrow.Transform.SetPositionAndRotation(ShootPoint.position, ShootPoint.rotation);
            arrow.SetParticle(particle);

            arrow.Rigidbody.velocity = ShootPoint.forward * ArrowVelocity;
        }
    }
}