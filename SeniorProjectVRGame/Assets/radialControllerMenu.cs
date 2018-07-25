using UnityEngine;

public class radialControllerMenu : MonoBehaviour {

    public float rotationspeed;
    public SteamVR_TrackedController controllerLeft;
    public static GameObject selected;
    public GameObject defaultObj;
    static MenuItemSettings[] MenuItems;
    MenuType[] PlacementTypes;
    static int CurrentItem=1;
    Types Current_Mode;
    private MenuMode Mode;
    Types old = 0;
    private SteamVR_Controller.Device device;
    public GameObject MenuRoot;
    private bool doOnce;



    public enum Types
    {
        WallPlacement = 0,
        EnemyPlacement = 1,
        TrapPlacement = 2,
        PropPlacement = 3,
        Disabled = 4
    }
    // Use this for initialization
    void Start ()
    {
        doOnce = true;
        PlacementTypes = GetComponentsInChildren<MenuType>();
        Mode = GetComponent<MenuMode>();
        selected = defaultObj;
        Current_Mode = (Types)MenuMode.GetCurrentMode();
        SetupPlacement();
        var colliders = MenuRoot.GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider collider in colliders)
        {
            collider.enabled = false;
        }
        var meshes = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GetCurrentState() == GameStateManager.Types.Build)
        {
            if (doOnce)
            {
                var meshes = GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer mesh in meshes)
                {
                    mesh.enabled = true;
                }
                doOnce = false;
            }
            Current_Mode = (Types) MenuMode.GetCurrentMode();
            if (old != Current_Mode)
            {
                Debug.Log("Old not equal to new");
                SetupPlacement();
                old = Current_Mode;

            }
            if (Current_Mode == (Types) MenuMode.Mode.Disabled)
            {

                var meshes = MenuRoot.GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer mesh in meshes)
                {
                    mesh.enabled = false;
                }

                Destroy(gameObject);
            }
            if (controllerLeft)
            {
                device = SteamVR_Controller.Input((int) controllerLeft.GetComponentInParent<SteamVR_TrackedObject>()
                    .index);
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    if (Mathf.Abs(device.GetAxis().y) > Mathf.Abs(device.GetAxis().x))
                    {
                        if (device.GetAxis().y > 0)
                        {
                            MenuMode.SetMode((int) Current_Mode + 1);
                        }
                        else
                        {
                            MenuMode.SetMode((int) Current_Mode - 1);
                        }
                    }
                }
                PlacementMode();
            }
        }
        else
        {
            var meshes = GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = false;
            }
        }
    }
    public static GameObject getSelected()
    {
        return selected;
    }

    private void PlacementMode()
    {
        MenuItems[CurrentItem].transform.Rotate(Vector3.up * Time.deltaTime * rotationspeed, Space.Self);



        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (Mathf.Abs(device.GetAxis().x) > Mathf.Abs(device.GetAxis().y))
            {
                if (device.GetAxis().x < 0)
                {

                    transform.Rotate(0, 120, 0);
                    if (CurrentItem + 1 > MenuItems.Length - 1)
                    {
                        CurrentItem = 0;
                    }
                    else
                    {
                        CurrentItem++;
                    }
                    selected = MenuItems[CurrentItem].prefab;
                    ApplyMenuAppearance(MenuItems);
                }
                else
                {
                    transform.Rotate(0, -120, 0);
                    if (CurrentItem - 1 < 0)
                    {
                        CurrentItem = MenuItems.Length - 1;
                    }
                    else
                    {
                        CurrentItem--;
                    }
                    selected = MenuItems[CurrentItem].prefab;
                    ApplyMenuAppearance(MenuItems);
                }
            }

        }
        
    }

    public void ApplyMenuAppearance(MenuItemSettings[] MenuItems)
    {
        foreach (MenuItemSettings menuitem in MenuItems)
        {
            if (menuitem.prefab == selected)
            {
                menuitem.transform.localScale = menuitem.initial_scale * 1.5f;
                
                MeshRenderer[] meshs;
                meshs = GetMenuItemMesh(menuitem.gameObject);
                foreach (MeshRenderer mesh in meshs)
                {
                    mesh.material.shader = Shader.Find("Standard");

                }

            }
            else
            {
                menuitem.transform.localScale = menuitem.initial_scale;
                MeshRenderer[] meshs;
                meshs = GetMenuItemMesh(menuitem.gameObject);
                foreach (MeshRenderer mesh in meshs)
                {
                    mesh.material.shader = Shader.Find("Valve/VR/Silhouette");
                }
            }
        }
    }


    private MeshRenderer[] GetMenuItemMesh(GameObject menuItem)
    {
        MeshRenderer[] Children = menuItem.GetComponentsInChildren<MeshRenderer>();
        return Children;
    }
    private void SetupPlacement()
    {
        foreach(MenuType x in PlacementTypes)
        {
            if(x.placementType == MenuMode.GetCurrentMode())
            {
                x.gameObject.SetActive(true);
                MenuItems = x.gameObject.GetComponentsInChildren<MenuItemSettings>();
                Debug.Log("Set Wall AActive");
            }
            else
            {
                x.gameObject.SetActive(false);
            }
        }
        selected = MenuItems[CurrentItem].prefab;
        ApplyMenuAppearance(MenuItems);
    }

    public static int GetNumberPlacedSelectedItem()
    {
        return MenuItems[CurrentItem].placed;
    }

    public static void PlaceItem()
    {
        MenuItems[CurrentItem].placed++;
    }

    public static void DeleteItem(GameObject PlacementObejct)
    {
        foreach(MenuItemSettings menuitem in MenuItems)
        {
            if(menuitem.type == PlacementObejct.GetComponent<Location>().type)
            {
                menuitem.placed--;
            }
        }
    }



}
