namespace DefaultNamespace
{
    public interface IEnemyFactory
    {
        EnemyController Create(Health hp);
    }
}