public class EnemyPatrolStrategy : IEnemyStrategy
{
    private readonly WaypointPatroller _patroller;
    private readonly TargetChaser _chaser;

    public EnemyPatrolStrategy(WaypointPatroller patroller, TargetChaser chaser)
    {
        _patroller = patroller;
        _chaser = chaser;
    }

    public void Tick()
    {
        _chaser.Stop();
        _patroller.TickPatrol();
    }
}
