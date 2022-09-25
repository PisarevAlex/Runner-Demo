using UnityEngine;

public interface IControllable
{
    void Run();
    void Stop();
    void Turn(Vector2 moveDirection);
}
