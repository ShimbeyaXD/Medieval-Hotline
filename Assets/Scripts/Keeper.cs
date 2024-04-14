using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Keeper : MonoBehaviour
{
    int currentScene;
    int duplicates = 0;
    bool destroyMyself = false;

    List<Door> doors = new List<Door>();

    List<WeaponProjectile> weaponProjectiles = new List<WeaponProjectile>();

    List<BloodManager> bloodManagers = new List<BloodManager>(); 

    public List<GameObject> childrenList;

    public GameObject Checkpoint { get; private set; }

    public bool IsLevelCleared { get; set; }

    void Start()
    {
        DontDestroyOnLoad(this);



        currentScene = SceneManager.GetActiveScene().buildIndex;



        GameObject[] allKeepers = GameObject.FindGameObjectsWithTag("Keeper");

        if (allKeepers.Length > 1)
        {
            Destroy(gameObject);
        }


        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].Replace();
        }

        for (int i = 0; i < weaponProjectiles.Count; i++)
        {
            weaponProjectiles[i].Replace();
        }
    }

    void Update()
    {
        Debug.Log("Door count is " + doors.Count);

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
        for (int i = 0; i < childrenList.Count; i++)
        {
            Debug.Log(childrenList[i]);

            if (gameObject.transform.name == childrenList[i].name)
            {
                destroyMyself = true;
                Debug.Log("Destroy door: " + gameObject.name);
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
        doors.Add(door);

        if (!SearchAndDestroy(gameObject))
        {
            gameObject.transform.parent = this.transform;
            childrenList.Add(gameObject);
        }
    }

    public void WeaponInstance(WeaponProjectile weaponProjectile, GameObject gameObject)
    {
        weaponProjectiles.Add(weaponProjectile);

        if (!SearchAndDestroy(gameObject))
        {
            gameObject.transform.parent = this.transform;
            childrenList.Add(gameObject);
        }
    }

    public void BloodInstance()
    {

    }

    public void CorpseInstance()
    {

    }

    public void RecieveCheckpoint(GameObject checkPoint)
    {
        Checkpoint = checkPoint;
    }
}
