using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Robot assingedRobot;
    public float ArrowDistance = 10f;
    public double distance;

    private void Update()
    {
        distance = CalculateDistance(assingedRobot.transform.position.x, assingedRobot.transform.position.y, MapGenerator.instance.activePlayer.transform.position.x, MapGenerator.instance.activePlayer.transform.position.y);

        transform.localScale = GetScale(distance); 
        transform.position = MapGenerator.instance.activePlayer.transform.position + new Vector3(0,-0.8f,0) + 3 * ((assingedRobot.transform.position - MapGenerator.instance.activePlayer.transform.position).normalized);
        Vector3 look = transform.InverseTransformPoint(assingedRobot.transform.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
        transform.Rotate(0, 0, angle + 90);
    }
    public double CalculateDistance(float StartX, float StartY, float EndX, float EndY)
    {
        double exit = Mathf.Sqrt(Mathf.Pow((StartX) - (EndX), 2) + Mathf.Pow((StartY) - (EndY), 2));
        return exit;
    }
    public Vector3 GetScale(double distance)
    {
        if (distance < 3) return new Vector3(0, 0, 0);
        else if (distance < 30) return new Vector3((float)(distance / 30), (float)(distance / 30), 0);
        else return new Vector3(1, 1, 1);
    }
}
