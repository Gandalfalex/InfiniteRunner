using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoinInterface : IObjectStatsInterface {

    int GetValue();

    void SetValue(int newValue);
}
