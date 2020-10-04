using UnityEngine;

public interface ISpaceShipActionsScanner
{
    void scanMoveRight();
    void scanMoveLeft();
    void scanMoveUp();
    void scanMoveDown();

    void scanSpawnRocket();
}
