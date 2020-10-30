

public interface IEnemyState
{
    void EnterState(Enemy parent);

    void UpdateState();

    void ExitState();
    
}
