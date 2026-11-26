using UnityEngine;

/// <summary>
/// Builds a simple, asset-free village blockout from Unity primitives.
/// Add this component to an empty GameObject and press Play.
/// Replace primitives with original art assets as the project grows.
/// </summary>
public class BangladeshVillageBuilder : MonoBehaviour
{
    [Header("Village size")]
    [SerializeField] private int villageSeed = 2026;
    [SerializeField] private float groundSize = 220f;

    private Material grass, road, wall, roof, water, treeGreen, trunk, concrete;

    private void Start()
    {
        Random.InitState(villageSeed);
        CreateMaterials();
        BuildGround();
        BuildRoads();
        BuildPond();
        BuildHouses();
        BuildMarket();
        BuildMosque();
        BuildTrees();
    }

    private void CreateMaterials()
    {
        grass = Mat(new Color(0.28f, 0.48f, 0.22f));
        road = Mat(new Color(0.22f, 0.22f, 0.20f));
        wall = Mat(new Color(0.78f, 0.70f, 0.54f));
        roof = Mat(new Color(0.48f, 0.22f, 0.15f));
        water = Mat(new Color(0.12f, 0.42f, 0.55f, 0.85f));
        treeGreen = Mat(new Color(0.12f, 0.34f, 0.12f));
        trunk = Mat(new Color(0.30f, 0.19f, 0.10f));
        concrete = Mat(new Color(0.62f, 0.62f, 0.58f));
    }

    private Material Mat(Color color)
    {
        var m = new Material(Shader.Find("Standard"));
        m.color = color;
        return m;
    }

    private GameObject Cube(string name, Vector3 pos, Vector3 scale, Material mat)
    {
        var o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        o.name = name;
        o.transform.SetParent(transform);
        o.transform.position = pos;
        o.transform.localScale = scale;
        o.GetComponent<Renderer>().material = mat;
        return o;
    }

    private void BuildGround()
    {
        Cube("Village Ground", new Vector3(0, -0.6f, 0),
            new Vector3(groundSize, 1, groundSize), grass);
    }

    private void BuildRoads()
    {
        Cube("Main Village Road", new Vector3(0, -0.02f, 0),
            new Vector3(12, 0.12f, groundSize), road);
        Cube("Cross Village Road", new Vector3(0, 0.00f, 0),
            new Vector3(groundSize, 0.12f, 8), road);
        Cube("Market Lane", new Vector3(-48, 0.02f, 0),
            new Vector3(7, 0.12f, 75), road);
    }

    private void BuildPond()
    {
        var p = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        p.name = "Village Pond";
        p.transform.SetParent(transform);
        p.transform.position = new Vector3(58, -0.18f, 48);
        p.transform.localScale = new Vector3(24, 0.12f, 17);
        p.GetComponent<Renderer>().material = water;
    }

    private void BuildHouse(Vector3 position, bool tinRoof, string houseName)
    {
        float w = Random.Range(7f, 10f);
        float d = Random.Range(6f, 9f);
        Cube(houseName + " Walls", position + Vector3.up * 2f,
            new Vector3(w, 4f, d), wall);
        Cube(houseName + (tinRoof ? " Tin Roof" : " Roof"),
            position + Vector3.up * 4.15f,
            new Vector3(w + 0.8f, 0.45f, d + 0.8f), roof);
        Cube(houseName + " Door", position + new Vector3(0, 1.1f, -d / 2f - 0.03f),
            new Vector3(1.3f, 2.2f, 0.12f), trunk);
    }

    private void BuildHouses()
    {
        int index = 1;
        for (int z = -75; z <= 75; z += 25)
        {
            BuildHouse(new Vector3(-28, 0, z), true, "Village House " + index++);
            BuildHouse(new Vector3(30, 0, z + 8), Random.value > 0.4f, "Village House " + index++);
        }
    }

    private void BuildMarket()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 p = new Vector3(-62, 0, -28 + i * 14);
            Cube("Village Shop " + (i + 1) + " Body", p + Vector3.up * 2,
                new Vector3(9, 4, 9), concrete);
            Cube("Village Shop " + (i + 1) + " Signboard", p + new Vector3(0, 3.1f, -4.6f),
                new Vector3(6, 0.8f, 0.2f), roof);
        }
    }

    private void BuildMosque()
    {
        Vector3 p = new Vector3(62, 0, -52);
        Cube("Mosque Main Hall", p + Vector3.up * 3,
            new Vector3(18, 6, 13), concrete);
        Cube("Mosque Roof", p + Vector3.up * 6.2f,
            new Vector3(19, 0.6f, 14), roof);
        Cube("Mosque Minaret", p + new Vector3(8, 8, -4),
            new Vector3(2, 16, 2), concrete);
        var dome = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        dome.name = "Mosque Dome";
        dome.transform.SetParent(transform);
        dome.transform.position = p + new Vector3(-2, 7, 0);
        dome.transform.localScale = new Vector3(5, 3, 5);
        dome.GetComponent<Renderer>().material = treeGreen;
    }

    private void BuildTrees()
    {
        for (int i = 0; i < 45; i++)
        {
            float x = Random.Range(-100f, 100f);
            float z = Random.Range(-100f, 100f);
            if (Mathf.Abs(x) < 10 || Mathf.Abs(z) < 7) continue;
            Vector3 p = new Vector3(x, 0, z);

            var t = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            t.name = "Village Tree Trunk";
            t.transform.SetParent(transform);
            t.transform.position = p + Vector3.up * 2f;
            t.transform.localScale = new Vector3(0.8f, 2f, 0.8f);
            t.GetComponent<Renderer>().material = trunk;

            var leaves = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            leaves.name = "Village Tree Canopy";
            leaves.transform.SetParent(transform);
            leaves.transform.position = p + Vector3.up * 5f;
            leaves.transform.localScale = new Vector3(5, 5, 5);
            leaves.GetComponent<Renderer>().material = treeGreen;
        }
    }
}
