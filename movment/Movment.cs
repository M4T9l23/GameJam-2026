using Godot;
using System;

public partial class Movment : CharacterBody2D{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("Movment script is running on: ", Name);
    }

    public float Speed = 400f;
    public float JumpVelocity = -600f;   // negative = up
    public float Gravity = 980f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        /*if (!IsOnFloor())
            velocity.Y += Gravity * (float)delta;*/ 

        /*if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
            velocity.Y = JumpVelocity;*/ 

        float dir = Input.GetAxis("ui_left", "ui_right");
        velocity.X = dir * Speed;
        
        float dir_y = Input.GetAxis("ui_up", "ui_down");
        velocity.Y = dir_y * Speed;

        Velocity = velocity;
        MoveAndSlide();
    }
    
    [Export] public PackedScene BulletScene;

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mb && mb.Pressed && mb.ButtonIndex == MouseButton.Left)
            Shoot();
    }
    
    private void Shoot()
    {
        if (BulletScene == null) return;
        GD.Print("fsdlkjsafjafoiajfoiajfi");
        Bullet bullet = BulletScene.Instantiate<Bullet>();
        bullet.Direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
        bullet.GlobalPosition = GlobalPosition + bullet.Direction * 30f;

        GetTree().CurrentScene.AddChild(bullet);
    }
}

