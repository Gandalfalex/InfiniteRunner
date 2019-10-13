using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CoinInterface : ObjectStatsInterface {

    int GetValue();

    void SetValue(int newValue);
}
