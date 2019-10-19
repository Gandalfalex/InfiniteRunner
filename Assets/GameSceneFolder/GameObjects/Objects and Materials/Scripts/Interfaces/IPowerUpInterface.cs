using System;
public interface IPowerUpInterface : IObjectStatsInterface
{

    float Duration();

    PowerUp_Type GetPowerUp_Type();

    void OnRaiseEvent();
}

