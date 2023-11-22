public abstract class BaseCharacterState : IState
{

    protected readonly PlayerController _playerController;
    protected readonly InputReader _input;

    protected BaseCharacterState(PlayerController player, InputReader input)
    {
        this._playerController = player;
        this._input = input;
    }
    
    public virtual void OnEnter()
    {
        //noop
    }

    public virtual void Update()
    {
        //noop
    }

    public virtual void FixedUpdate()
    {
        //noop
    }

    public virtual void OnExit()
    {
        //noop
    }
}