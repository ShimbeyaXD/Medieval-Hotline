using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Keeper : MonoBehaviour
{
    int currentScene;
    int duplicates = 0;
    bool destroyMyself = false;

    GameObject systemObject;  // Managers
    GameObject demonObject;   // Objects from Demonphase
    GameObject cultistObject; // Objects from Cultistphase

    List<Door> doors = new List<Door>();

    List<WeaponProjectile> weaponProjectiles = new List<WeaponProjectile>();

    List<BloodManager> bloodManagers = new List<BloodManager>(); 

    List<EnemyYEs> enemyYes = new List<EnemyYEs>();



    public List<GameObject> childrenList;

    public bool GrantCheckpoint { get; private set; } = false;

    public bool DemonPhase { get; set; } = false;

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
        Debug.Log("DemonPhase " + DemonPhase);

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

        for (int i = 0; i < childrenList.Count; i++)
        {
            if (gameObject.transform.name == childrenList[i].name)
            {
                destroyMyself = true;
                break;
            }
            else
            {
                destroyMyself = false;
            }
        }

        return destroyMyself;
    }


    public void DoorInstance(Door door, GameObject gameObject)
    {
        //doors.Add(door);

        if (!SearchAndDestroy(gameObject))
        {
            if (DemonPhase)
            {
                gameObject.transform.parent = demonObject.transform;
            }


            gameObject.transform.parent = this.transform;
            childrenList.Add(gameObject);
        }
    }

    public void WeaponInstance(WeaponProjectile weaponProjectile, GameObject gameObject)
    {
        //weaponProjectiles.Add(weaponProjectile);

        if (!SearchAndDestroy(gameObject))
        {
            gameObject.transform.parent = this.transform;
            childrenList.Add(gameObject);
        }
    }

    public void BloodInstance(BloodManager bloodManager, GameObject gameObject)
    {
        //bloodManagers.Add(bloodManager);

        if (!SearchAndDestroy(gameObject))
        {
            gameObject.transform.parent = this.transform;
            childrenList.Add(gameObject);
        }
    }

    public void CorpseInstance(BloodManager bloodManager, GameObject gameObject)
    {
        //bloodManagers.Add(bloodManager);

        if (!SearchAndDestroy(gameObject))
        {
            gameObject.transform.parent = this.transform;
            childrenList.Add(gameObject);
        }
    }

    public void EnemyInstance(EnemyYEs enemy, GameObject gameObject)
    {
        //enemyYes.Add(enemy);

        if (!SearchAndDestroy(gameObject))
        {
            GameObject nameHolder = new GameObject(enemy.name);
            nameHolder.transform.parent = this.transform;
            //gameObject.transform.parent = this.transform;
            childrenList.Add(nameHolder);
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
        DemonPhase = false;
        IsLevelCleared = false;
        PlayOpeningAnimation = true;

        WipeLists();
    }

    public void WipeLists() 
    {
        if (GrantCheckpoint) { return; }

        
        // If demonphase is true then wipe all lists from both the cultist and the demonobject
        // But if demonphase is false then only wipe the lists from the demonobject

        for (int i = 0; i < transform.childCount; i++)
        {
            if (DemonPhase)
            {
                Destroy(demonObject.transform.GetChild(i).gameObject);
            }
            else
            {
                Destroy(cultistObject.transform.GetChild(i).gameObject);
                Destroy(demonObject.transform.GetChild(i).gameObject);
            }
        }

        childrenList.Clear();
        weaponProjectiles.Clear();
        bloodManagers.Clear();
        enemyYes.Clear();
        doors.Clear();

        for (int i = 0; i < childrenList.Count; i++)
        {
            childrenList.RemoveAt(i);
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
