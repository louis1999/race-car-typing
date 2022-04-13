using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class GenerateCar : MonoBehaviour
{
    private string car;

    public GameObject CICADA_PARENT;
    public GameObject Free_Racing_Car_Yellow;
    public GameObject MuscleCar_Single;
    public PathCreator path;
    public SliderScripts fuelBar;
    public GameObject mainCamera;
    private FuelHandler fuelHandler;
    private PathCreation.Examples.PathFollower pathFollower;
    public GameObject CarGO;

    void Start(){
        if(car=="CICADA_PARENT"){
            CarGO = Instantiate(CICADA_PARENT);

        }else if(car=="Free_Racing_Car_Yellow"){
            CarGO = Instantiate(Free_Racing_Car_Yellow);

        }else if(car=="MuscleCar_Single"){
            CarGO = Instantiate(MuscleCar_Single);
            
        }


        
        fuelHandler = CarGO.AddComponent<FuelHandler>();
        pathFollower = CarGO.AddComponent<PathCreation.Examples.PathFollower>();
        pathFollower.pathCreator= path;
        fuelHandler.fuelBar=fuelBar;
        
        CameraFollow cameraView = mainCamera.GetComponent<CameraFollow>();

        Transform carTransform = CarGO.GetComponent<Transform>();
        cameraView.target = carTransform;

    }

    void OnEnable()
    {
        car  =  PlayerPrefs.GetString("car");
    }
}
