namespace DefaultNamespace
{
    public sealed class Asteroid : BaseEnemyController
    {
        public Asteroid(EnemyData enemyData, IEnemyFactory factory) 
            : base(enemyData, factory)
        {
        }
    }
}