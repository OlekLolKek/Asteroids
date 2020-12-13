namespace DefaultNamespace
{
    public sealed class Asteroid : EnemyController
    {
        public void DependencyInjectHealth(Health hp)
        {
            Health = hp;
        }
    }
}