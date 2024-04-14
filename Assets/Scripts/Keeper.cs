using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Keeper : MonoBehaviour
{
    int currentScene;
    int duplicates = 0;
    bool destroyMyself = false;

    [SerializeField] Vector3 objectScale = new Vector3(0.75f, 0.75f, 0.75f);

    GameObject systemObject;  // Managers
    GameObject demonObject;   // Objects from Demonphase
    GameObject cultistObject; // Objects from Cultistphase

    List<Door> doors = new List<Door>();

    List<WeaponProjectile> weaponProjectiles = new List<WeaponProjectile>();

    List<BloodManager> bloodManagers = new List<BloodManager>(); 

    List<EnemyYEs> enemyYes = new List<EnemyYEs>();

    public List<GameObject> cultistList;
    public List<GameObject> demonList;

    public bool GrantCheckpoint { get; set; } = false;

    public Vector2 Checkpoint { get; private set; }

    public bool PlayOpeningAnimation { get; set; } = true;

    public bool IsLevelCleared { get; set; }

    void Start()
    {
        DontDestroyOnLoad(this);

        systemObject = transform.GetChild(0).gameObject;
        demonObject = transform.GetChild(1).gameObject;
        cultistObject = transform.GetChild(2).gameObject;

        currentScene = SceneManager.GetActiveScene().buildIndex;

        GameObject[] allKeepers = GameObject.FindGameObjectsWithTag("Keeper");

        if (allKeepers.Length > 1)
        {
            Destroy(gameObject);
        }


        
    }

    void Update()
    {
        Debug.Log("DemonPhase " + GrantCheckpoint);

        CheckCurrentScene();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void CheckCurrentScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != currentScene)
        {
            Destroy(gameObject);
        }
    }

    public bool SearchAndDestroy(GameObject gameObject)
    {
        if (!GrantCheckpoint) return false;

        if (GrantCheckpoint)
        {
            for (int i = 0; i < cultistList.Count; i++)
            {
                if (gameObject.transform.name == cultistList[i].name)
                {
                    destroyMyself = true;
                    break;
                }
                else
                {
                    destroyMyself = false;
                }
            }
        }


        return destroyMyself;
    }


    public void DoorInstance(Door door, GameObject gameObject)
    {
        //doors.Add(door);

        if (!SearchAndDestroy(gameObject))
        {
            if (GrantCheckpoint)
            {
                gameObject.transform.parent = demonObject.transform;
                demonList.Add(gameObject);
            }
            if (!GrantCheckpoint)
            {
                gameObject.transform.parent = cultistObject.transform;
                cultistList.Add(gameObject);
            }
        }
    }

    public void WeaponInstance(WeaponProjectile weaponProjectile, GameObject gameObject)
    {
        gameObject.transform.localScale = objectScale;

        if (!SearchAndDestroy(gameObject))
        {
            if (GrantCheckpoint)
            {
                gameObject.transform.parent = demonObject.transform;
                demonList.Add(gameObject);
            }
            if (!GrantCheckpoint)
            {
                gameObject.transform.parent = cultistObject.transform;
                cultistList.Add(gameObject);
            }
        }
    }

    public void BloodInstance(BloodManager bloodManager, GameObject gameObject)
    {
        if (!SearchAndDestroy(gameObject))
        {
            if (GrantCheckpoint)
            {
                gameObject.transform.parent = demonObject.transform;
                demonList.Add(gameObject);
            }
            if (!GrantCheckpoint)
            {
                gameObject.transform.parent = cultistObject.transform;
                cultistList.Add(gameObject);
            }
        }
    }

    public void CorpseInstance(BloodManager bloodManager, GameObject gameObject)
    {
        gameObject.transform.localScale = objectScale;

        if (!SearchAndDestroy(gameObject))
        {
            if (GrantCheckpoint)
            {
                gameObject.transform.parent = demonObject.transform;
                demonList.Add(gameObject);
            }
            if (!GrantCheckpoint)
            {
                gameObject.transform.parent = cultistObject.transform;
                cultistList.Add(gameObject);
            }
        }
    }

    public void EnemyInstance(EnemyYEs enemy, GameObject gameObject)
    {
        //enemyYes.Add(enemy);

        if (!SearchAndDestroy(gameObject))
        {
            GameObject nameHolder = new GameObject(enemy.name);

            if (GrantCheckpoint)
            {
                nameHolder.transform.parent = demonObject.transform;
                demonList.Add(nameHolder);
            }
            if (!GrantCheckpoint)
            {
                nameHolder.transform.parent = cultistObject.transform;
                cultistList.Add(nameHolder);
            }

        }
    }

    public void RecieveCheckpoint(Vector2 position)
    {
        GrantCheckpoint = true;
        Checkpoint = position;
    }

    public void StageEnd()
    {
        GrantCheckpoint = false;
        GrantCheckpoint = false;
        IsLevelCleared = false;
        PlayOpeningAnimation = true;

        WipeLists();
    }

    public void WipeLists() 
    {


        // If demonphase is true then wipe all lists from both the cultist and the demonobject
        // But if demonphase is false then only wipe the lists from the demonobject

        if (GrantCheckpoint)
        {
            for (int i = 0; i < demonObject.transform.childCount; i++)
            {
                Destroy(demonObject.transform.GetChild(i).gameObject);
            }
        }
        if (!GrantCheckpoint)
        {
            for (int i = 0; i < cultistObject.transform.childCount; i++)
            {
                Destroy(cultistObject.transform.GetChild(i).gameObject);
            }
        }

        if (GrantCheckpoint) { return; }

        cultistList.Clear();
        weaponProjectiles.Clear();
        bloodManagers.Clear();
        enemyYes.Clear();
        doors.Clear();

        for (int i = 0; i < cultistList.Count; i++)
        {
            cultistList.RemoveAt(i);
        }

        for (int i = 0; i < doors.Count; i++)
        {
            doors.RemoveAt(i);

            //doors[i].Replace();
        }

        for (int i = 0; i < weaponProjectiles.Count; i++)
        {
            weaponProjectiles.RemoveAt(i);

            //weaponProjectiles[i].Replace();
        }

        for (int i = 0; i < bloodManagers.Count; i++)
        {
            bloodManagers.RemoveAt(i);

            //bloodManagers[i].Replace();
        }

        for (int i = 0; i < enemyYes.Count; i++)
        {
            enemyYes.RemoveAt(i);

            //enemyYes[i].Replace();
        }
    }
}
