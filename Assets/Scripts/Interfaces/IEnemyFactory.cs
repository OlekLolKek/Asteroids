namespace DefaultNamespace
{
    public interface IEnemyFactory
    {
        EnemyController Create(Health hp, EnemyData data);
    }
}