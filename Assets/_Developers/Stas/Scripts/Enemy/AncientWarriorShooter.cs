namespace MythicalBattles
{
    public class AncientWarriorShooter : Shooter
    {
        protected override void Shoot()
        {
            Arrow arrow = (Arrow)_projectilePool.GetItem();
            ParticleEffect particle = (ParticleEffect)_effectPool.GetItem();

            arrow.gameObject.SetActive(true);
            arrow.Transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);
            arrow.SetParticle(particle);

            arrow.Rigidbody.velocity = _shootPoint.forward * _arrowVelcity;
        }
    }
}