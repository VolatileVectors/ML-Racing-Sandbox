using UnityEngine;
using UnityEngine.UI;

public class CarDataVisualizer : MonoBehaviour
{
    [Header("References")] public CarController smartCar;
    public Text distanceSensorText;
    public Toggle leftInfraRed;
    public Toggle middleInfraRed;
    public Toggle rightInfraRed;

    private void Update()
    {
        distanceSensorText.text = $"Distance: {smartCar.Ultrasonic_get_distance()} cm";
        leftInfraRed.isOn = smartCar.GPIO_input("IR01");
        middleInfraRed.isOn = smartCar.GPIO_input("IR02");
        rightInfraRed.isOn = smartCar.GPIO_input("IR03");
    }
}