public interface IState
{
    //void Enter(StateMachine stateMachine);
    void Excute();
    void Exit();
    void Enter(StateMachine stateMachine, GameManager gameManager);
}
