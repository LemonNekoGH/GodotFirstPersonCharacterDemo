using Godot;

public partial class Player : CharacterBody3D
{
    [Export] public float Gravity { get; set; } = 9.8f;
    [Export] public float Speed { get; set; } = 1f;
    [Export] public float RotationSpeed { get; set; } = 0.001f;

    private Vector3 _targetVelocity = Vector3.Zero;
    
    public override void _PhysicsProcess(double delta)
    {
        _targetVelocity.Z = 0;
        _targetVelocity.X = 0;

        var direction = new Vector3(-Mathf.Sin(Rotation.Y), 0, -Mathf.Cos(Rotation.Y));

        if (!IsOnFloor())
        {
            _targetVelocity.Y -= Gravity * (float)delta;
        }

        if (Input.IsKeyPressed(Key.W))
        {
            direction = direction.Normalized();
            _targetVelocity.X = direction.X * Speed;
            _targetVelocity.Z = direction.Z * Speed;
        }

        if (Input.IsKeyPressed(Key.Escape))
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }

        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }

        Velocity = _targetVelocity;
        MoveAndSlide();
    }

    public override void _Input(InputEvent e)
    {
        if (e is InputEventMouseMotion inputEvent && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            RotateY(-inputEvent.Relative.X * RotationSpeed);
            return;
        }
    }
}