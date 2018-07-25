using UnityEngine;
using System.Collections;
using System;
using Valve.VR.InteractionSystem;

public class GridBehavior : MonoBehaviour
{
    private GameObject LastObjectPlaced;
    public Transform pointerOriginTransform = null;
    public GameObject prefabPlacementObject;
    public GameObject prefabOK;
    public GameObject prefabFail;
    public GameObject SmallprefabOK;
    public GameObject SmallprefabFail;
    public SteamVR_TrackedController controller1;
    public float LargeGrid = 2.0f;
    public float SmallGrid = 1.0f;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private SteamVR_TrackedObject trackedObj;
    public RaycastHit hitInfo;
    private Ray ray;
    private bool recentChange = false;
    private bool solvable = false;
    // Store which spaces are in use
    int[,] LargeUsedSpace;
    int[,] SmallUsedSpace;
    GameObject placementObject = null;
    GameObject areaObject = null;
    GameObject[] WallObjects;
    Vector3 lastPos;
    Vector3 LargeSlots;
    Vector3 SmallSlots;
    SteamVR_Controller.Device device;
    Vector3 LargeHalfSlots;
    Vector3 SmallHalfSlots;
    public int[] PlacedItems = new int[12];
    public int[] MaxItems = new int[12];
    bool doOnce;
    private bool placeSwitch = true;

    // Use this for initialization

    void Start()
    {
        doOnce = true;
        LargeHalfSlots = GetComponent<Renderer>().bounds.size / 2.0f;
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        LargeSlots = GetComponent<Renderer>().bounds.size / LargeGrid;
        LargeUsedSpace = new int[Mathf.CeilToInt(LargeSlots.x), Mathf.CeilToInt(LargeSlots.z)];
        solvable = true;
        for (var x = 0; x < Mathf.CeilToInt(LargeSlots.x); x++)
        {
            for (var z = 0; z < Mathf.CeilToInt(LargeSlots.z); z++)
            {
                LargeUsedSpace[x, z] = 0;
            }
        }
        SmallHalfSlots = GetComponent<Renderer>().bounds.size / 2.0f;
        SmallSlots = GetComponent<Renderer>().bounds.size / (SmallGrid);
        SmallUsedSpace = new int[Mathf.CeilToInt(SmallSlots.x), Mathf.CeilToInt(SmallSlots.z)];
        for (var x = 0; x < Mathf.CeilToInt(SmallSlots.x); x++)
        {
            for (var z = 0; z < Mathf.CeilToInt(SmallSlots.z); z++)
            {
                SmallUsedSpace[x, z] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doOnce)
        {
            doOnce = false;
            WallObjects = GameObject.FindGameObjectsWithTag("PlacementObject");
        }
        if (GameStateManager.GetCurrentState() == GameStateManager.Types.Build&&controller1)
        {
            Vector3 point;
            device = SteamVR_Controller.Input((int)controller1.GetComponent<SteamVR_TrackedObject>().index);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {

                var Layers = 1 << 12;
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, Layers))
                {
                    var hit = hitInfo.collider.gameObject.GetComponentInParent<Collider>().gameObject;
                    var gx = hit.GetComponent<Location>().x;
                    var gz = hit.GetComponent<Location>().y;
                    var gt = hit.GetComponent<Location>().gridType;
                    if (gt == 0)
                    {
                        LargeUsedSpace[gx, gz] = 0;
                    }
                    else if (gt == 1)
                    {
                        SmallUsedSpace[gx, gz] = 0;
                    }
                    Destroy(hit);
                    radialControllerMenu.DeleteItem(hit.gameObject);
                    recentChange = true;

                }
            }
            // Check for mouse ray collision with this object
            if (getTargetLocation(out point))
            {
                var old = prefabPlacementObject;
                prefabPlacementObject = radialControllerMenu.getSelected();
                var updated = prefabPlacementObject;
                if (old != updated && placementObject)
                {
                    placementObject.transform.position = new Vector3(0, -1, 0);
                    placementObject = null;
                }

                int x = 0;
                int z = 0;
                if (prefabPlacementObject.GetComponent<Location>().gridType == 0)
                {
                    // Transform position is the center point of this object, x and z are grid slots from 0..slots-1
                    x = (int)Math.Round(Math.Round(point.x - transform.position.x + LargeHalfSlots.x - LargeGrid / 2.0f) / LargeGrid);
                    z = (int)Math.Round(Math.Round(point.z - transform.position.z + LargeHalfSlots.z - LargeGrid / 2.0f) / LargeGrid);
                    if (x > LargeSlots.x)
                    {
                        x = (int)LargeSlots.x;
                    }
                    if (x < 0)
                    {
                        x = 0;
                    }
                    if (z > LargeSlots.z)
                    {
                        z = (int)LargeSlots.z;
                    }
                    if (z < 0)
                    {
                        z = 0;
                    }


                    // Calculate the quantized world coordinates on where to actually place the object
                    point.x = (float)(x) * LargeGrid - LargeHalfSlots.x + transform.position.x + LargeGrid / 2.0f;
                    point.z = (float)(z) * LargeGrid - LargeHalfSlots.z + transform.position.z + LargeGrid / 2.0f;
                }
                else if (prefabPlacementObject.GetComponent<Location>().gridType == 1)
                {
                    // Transform position is the center point of this object, x and z are grid slots from 0..slots-1
                    x = (int)Math.Round(Math.Round(point.x - transform.position.x + SmallHalfSlots.x - SmallGrid / 2.0f) / SmallGrid);
                    z = (int)Math.Round(Math.Round(point.z - transform.position.z + SmallHalfSlots.z - SmallGrid / 2.0f) / SmallGrid);
                    if (x > SmallSlots.x)
                    {
                        x = (int)SmallSlots.x;
                    }
                    if (x < 0)
                    {
                        x = 0;
                    }
                    if (z > SmallSlots.z)
                    {
                        z = (int)SmallSlots.z;
                    }
                    if (z < 0)
                    {
                        z = 0;
                    }


                    // Calculate the quantized world coordinates on where to actually place the object
                    point.x = (float)(x) * SmallGrid - SmallHalfSlots.x + transform.position.x + SmallGrid / 2.0f;
                    point.z = (float)(z) * SmallGrid - SmallHalfSlots.z + transform.position.z + SmallGrid / 2.0f;
                }
                // Create an object to show if this area is available for building
                // Re-instantiate only when the slot has changed or the object not instantiated at all
                if (lastPos.x != x || lastPos.z != z || areaObject == null)
                {
                    lastPos.x = x;
                    lastPos.z = z;
                    if (areaObject != null)
                    {
                        areaObject.transform.position = new Vector3(0, 0, 0);
                    }
                    try
                    {
                        var tempPoint = point;

                        if (prefabPlacementObject.GetComponent<Location>().gridType == 0)
                        {
                            tempPoint.y = tempPoint.y + 4;
                            areaObject = LargeUsedSpace[x, z] == 0 && placementObject.GetComponent<Location>().GetCanPlace() && (prefabPlacementObject.GetComponent<Location>().NumberAllowed > radialControllerMenu.GetNumberPlacedSelectedItem() && solvable) ? prefabOK : prefabFail;
                        }
                        else if (prefabPlacementObject.GetComponent<Location>().gridType == 1)
                        {
                            tempPoint.y = tempPoint.y + 1.2f;
                            areaObject = SmallUsedSpace[x, z] == 0 && placementObject.GetComponent<Location>().GetCanPlace() && (prefabPlacementObject.GetComponent<Location>().NumberAllowed > radialControllerMenu.GetNumberPlacedSelectedItem()) && solvable ? SmallprefabOK : SmallprefabFail;
                        }
                        areaObject.transform.position = tempPoint;
                    }
                    catch (Exception ex)
                    {

                        Debug.Log(ex.Message);
                    }
                    if (areaObject)
                    {
                        Color color = areaObject.GetComponent<MeshRenderer>().material.color;
                        color.a = 0.3f;
                        areaObject.GetComponent<MeshRenderer>().material.color = color;
                    }
                }





                // Create or move the object
                if (!placementObject && prefabPlacementObject)
                {
                    foreach (GameObject wall in WallObjects)
                    {
                        Debug.Log("PlacementObject" + prefabPlacementObject.name);
                        Debug.Log("WallObject" + wall.name);
                        if (wall.GetComponent<Location>().type == prefabPlacementObject.GetComponent<Location>().type)
                        {
                            placementObject = wall;
                        }
                    }
                    placementObject.GetComponent<Location>().disableNavmeshObst();
                    placementObject.transform.position = point;
                }
                else if (placementObject)
                {
                    placementObject.transform.position = point;
                }


                // On left click, insert the object to the area and mark it as "used"
                    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)&&solvable)
                {
                    if (prefabPlacementObject.GetComponent<Location>().NumberAllowed >
                        radialControllerMenu.GetNumberPlacedSelectedItem())
                    {

                        // Place the object
                        if (prefabPlacementObject.GetComponent<Location>().gridType == 0)
                        {
                            if (LargeUsedSpace[x, z] == 0 && placementObject.GetComponent<Location>().GetCanPlace())
                            {
                                Debug.Log("Placement Position: " + x + ", " + z);
                                LargeUsedSpace[x, z] = 1;


                                // ToDo: place the result somewhere..
                                recentChange = true;
                                var temp = Instantiate(prefabPlacementObject, point, Quaternion.identity);
                                LastObjectPlaced = temp;
                                radialControllerMenu.PlaceItem();
                                temp.transform.rotation = placementObject.transform.rotation;
                                temp.GetComponent<Location>().x = x;
                                temp.GetComponent<Location>().y = z;
                                temp.layer = LayerMask.NameToLayer("PlacedObjects");


                            }
                        }
                        else if (prefabPlacementObject.GetComponent<Location>().gridType == 1)
                        {
                            if (SmallUsedSpace[x, z] == 0 && placementObject.GetComponent<Location>().GetCanPlace())
                            {
                                Debug.Log("Placement Position: " + x + ", " + z);
                                SmallUsedSpace[x, z] = 1;


                                // ToDo: place the result somewhere..

                                var temp = Instantiate(prefabPlacementObject, point, Quaternion.identity);
                                radialControllerMenu.PlaceItem();
                                LastObjectPlaced = temp;
                                temp.transform.rotation = placementObject.transform.rotation;
                                temp.GetComponent<Location>().x = x;
                                temp.GetComponent<Location>().y = z;
                                temp.layer = LayerMask.NameToLayer("PlacedObjects");
                                recentChange = true;

                            }
                        }
                    }
                }
                device = SteamVR_Controller.Input((int)controller1.GetComponent<SteamVR_TrackedObject>().index);
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {


                    if (device.GetAxis().x != 0)
                    {
                        if (device.GetAxis().x > 0)
                        {
                            placementObject.GetComponent<Location>().RotateRight();
                        }
                        else
                        {
                            placementObject.GetComponent<Location>().RotateLeft();
                        }
                    }
                }


            }
            else
            {
                if (placementObject)
                {
                    placementObject.transform.position = new Vector3(0, -1, 0);
                    placementObject = null;
                }
                if (areaObject)
                {
                    areaObject.transform.position = new Vector3(0, -1, 0);
                    areaObject = null;
                }
            }
        }
        else
        {
            if (placementObject)
            {
                placementObject.transform.position = new Vector3(0, -1, 0);
                placementObject = null;
            }
            if (areaObject)
            {
                areaObject.transform.position = new Vector3(0, -1, 0);
                areaObject = null;
            }
           
        }
    }
    bool getTargetLocation(out Vector3 point)
    {

        try
        {
            var ignoreLayers = 1 << 10;
            ignoreLayers |= (1 << 12);
            ray = new Ray(GetOriginPosition(), GetOriginForward());

            hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, ~ignoreLayers))
            {
                if (hitInfo.collider == GetComponent<Collider>())
                {
                    point = hitInfo.point;
                    return true;
                }
                else
                {
                    point = new Vector3(lastPos.x, lastPos.y, lastPos.z);
                    return false; ;
                }
            }
            point = Vector3.zero;
            return false;
        }
        catch (Exception)
        {
            point = new Vector3(lastPos.x, lastPos.y, lastPos.z);
            return false;
        }
    }


    protected virtual Vector3 GetOriginForward()
    {
        return (pointerOriginTransform ? pointerOriginTransform.forward : transform.forward);
    }
    protected virtual Vector3 GetOriginPosition()
    {
        return (pointerOriginTransform ? pointerOriginTransform.position : transform.position);
    }
    public void SetRecentChange(bool input)
    {
        recentChange = input;
    }
    public bool GetRecentChange()
    {
        return recentChange;
    }
    public bool GetSolvable()
    {
        return solvable;
    }
    public void SetSolvable(bool x)
    {
        solvable = x;
    }
    public Vector2 WorldToGridPositionWallLayer(Vector3 worldpos)
    {
        return (new Vector2((worldpos.x + LargeHalfSlots.x - transform.position.x - (LargeGrid / 2.0f)) / LargeGrid, (worldpos.z + LargeHalfSlots.z - transform.position.z - (LargeGrid / 2.0f)) / LargeGrid));
    }
    public GameObject GetLastObjectPlaced()
    {
        return LastObjectPlaced;
    }
    public void RemoveLastObject()
    {
        var hit = LastObjectPlaced;
        var gx = hit.GetComponent<Location>().x;
        var gz = hit.GetComponent<Location>().y;
        var gt = hit.GetComponent<Location>().gridType;
        if (gt == 0)
        {
            LargeUsedSpace[gx, gz] = 0;
        }
        else if (gt == 1)
        {
            SmallUsedSpace[gx, gz] = 0;
        }
        Destroy(hit);
        radialControllerMenu.DeleteItem(hit.gameObject);
        recentChange = true;
    }
}