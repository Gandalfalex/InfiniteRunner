using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JoyStick{
 

    public static Vector2 CalculateInput(Vector2 start, Vector2 end) {
        Vector2 temp = end - start;
        Vector2 squaredTemp = temp * temp;
        if (squaredTemp.x < squaredTemp.y)
            temp.x = 0;
        else
            temp.y = 0;
        return temp.normalized;       
    }

}
