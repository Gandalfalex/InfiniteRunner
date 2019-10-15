using System;
public interface PowerUpInterface : ObjectStatsInterface
{

    float Duration();

    PowerUp_Type GetPowerUp_Type();


    void OnEvent();
    event EventHandler myCustomEvent;
}

