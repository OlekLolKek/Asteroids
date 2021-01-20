namespace DefaultNamespace
{
    public interface IEnemyFactory
    {
        Enemy Create(Health hp, EnemyData data);
    }
}