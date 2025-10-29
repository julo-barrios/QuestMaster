using UnityEngine;
using System.Collections.Generic;
using System.IO;

// Un enum para identificar fácilmente qué curva de progresión queremos usar.
public enum ProgressionType
{
    Adventurer,
    Guild
}

public class ProgressionManager : MonoBehaviour
{
    private static ProgressionManager _instance;

    // ANTES: private Dictionary<int, LevelData> levelDataMap;
    // AHORA: Un diccionario de diccionarios. La clave es el nombre del tipo de progresión.
    private Dictionary<ProgressionType, Dictionary<int, LevelData>> progressionTables;

    // Una lista pública para que puedas asignar los archivos CSV en el Inspector.
    public List<ProgressionTableConfig> progressionConfigs;

    private static bool applicationIsQuitting = false;
    public static ProgressionManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            if (_instance == null)
            {
                var prefab = Resources.Load<GameObject>("GameManagers");
                Instantiate(prefab);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        applicationIsQuitting = false;
        DontDestroyOnLoad(gameObject);
        LoadAllProgressionData();
    }

    private void LoadAllProgressionData()
    {
        progressionTables = new Dictionary<ProgressionType, Dictionary<int, LevelData>>();

        // Recorremos la lista que configuramos en el Inspector.
        foreach (var config in progressionConfigs)
        {
            var levelDataMap = new Dictionary<int, LevelData>();
            TextAsset csvFile = config.csvFile; // Usamos el archivo asignado

            if (csvFile == null) continue;

            StringReader reader = new StringReader(csvFile.text);
            reader.ReadLine(); // Saltar encabezados

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                LevelData data = new LevelData
                {
                    Level = int.Parse(values[0]),
                    XP_Required = int.Parse(values[1]),
                };
                if (!levelDataMap.ContainsKey(data.Level))
                {
                    levelDataMap.Add(data.Level, data);
                }
            }

            // Guardamos el diccionario de niveles en nuestro diccionario de tablas.
            progressionTables.Add(config.type, levelDataMap);
            Debug.Log($"Cargada tabla de progresión '{config.type}' con {levelDataMap.Count} niveles.");
        }
    }

    // El método ahora necesita saber QUÉ tabla de progresión consultar.
    public LevelData GetLevelData(ProgressionType type, int level)
    {
        if (progressionTables.ContainsKey(type) && progressionTables[type].ContainsKey(level))
        {
            return progressionTables[type][level];
        }
        return null; // Devolver null si el nivel o tipo no existe.
    }
    
    public void OnDestroy()
    {
        if (_instance == this)
        {
            applicationIsQuitting = true;
        }
    }
}

// Una pequeña clase auxiliar para que la configuración en el Inspector sea amigable.
[System.Serializable]
public class ProgressionTableConfig
{
    public ProgressionType type;
    public TextAsset csvFile;
}