using Godot;

public partial class Bullet : Area2D
{
    [Export] public float Speed = 800f;
    public Vector2 Direction = Vector2.Right;

    public override void _Ready()
    {
        // clean up if it never hits anything
        GetTree().CreateTimer(3.0).Timeout += QueueFree;
        BodyEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Direction * Speed * (float)delta;
    }

    private void OnBodyEntered(Node2D body)
    {
        GD.Print("hit ", body.Name);
        QueueFree();
    }
}