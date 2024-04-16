using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Keeper : MonoBehaviour
{
    private static Keeper keeperInstance;

    [SerializeField] Vector3 objectScale = new Vector3(0.75f, 0.75f, 0.75f);

    GameObject systemObject;  // Managers
    GameObject horseObject;   // Horse 
    GameObject demonObject;   // Objects from Demonphase
    GameObject cultistObject; // Objects from Cultistphase

    List<Door> doors = new List<Door>();

    List<WeaponProjectile> weaponProjectiles = new List<WeaponProjectile>();

    List<BloodManager> bloodManagers = new List<BloodManager>();

    List<EnemyYEs> enemyYes = new List<EnemyYEs>();

    [Header("Scenes")]
    [SerializeField] int level1Scene;
    [SerializeField] int level2Scene;
    [SerializeField] int level3Scene;
    [SerializeField] int statScene;
    [SerializeField] int mainMenuScene;
    [SerializeField] List<int> popeScenes;
    int currentScene;

    [Header("Audio")]
    [SerializeField] AudioClip level1Theme;
    [SerializeField] AudioClip level2Theme;
    [SerializeField] AudioClip level3Theme;
    [SerializeField] AudioClip menuTheme;
    [SerializeField] AudioClip popeTheme;
    [Range(0,1)]
    [SerializeField] float volume;
    AudioSource audioStereo;

    int duplicates = 0;
    bool destroyMyself = false;

    public List<GameObject> cultistList;
    public List<GameObject> demonList;

    // KILLS, DEATHS, AND TIME
    public int deathCount1 { get; private set; }

    public int deathCount2 { get; private set; }

    public int deathCount3 { get; private set; }
    
    // KILLS, DEATHS, AND TIMEset; } 

    public bool GrantCheckpoint { get; set; } = false;

    public bool PlayOpeningAnimation { get; set; } = true;

    public int dialogueLine { get; private set; } = 0;

    public Vector2 Checkpoint { get; private set; }

    public bool IsLevelCleared { get; set; }

    public bool LevelEnded { get; set; }


    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (keeperInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            keeperInstance = this;
        }

        audioStereo = transform.GetChild(0).transform.GetChild(0).GetComponent<AudioSource>();
    }

    void Start()
    {
        systemObject = transform.GetChild(0).gameObject;
        horseObject = transform.GetChild(1).gameObject;
        demonObject = transform.GetChild(2).gameObject;
        cultistObject = transform.GetChild(3).gameObject;

        

    }

    void Update()
    {


        if (audioStereo == null)
        {
            audioStereo = transform.GetChild(0).transform.GetChild(0).GetComponent<AudioSource>();
        }

        audioStereo.volume = volume;

    }

    public void UpdateCurrentScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 0)
        {
            deathCount1 = 0;
            deathCount2 = 0;
            deathCount3 = 0;
        }
    }

    public void MusicStereo()
    {
        UpdateCurrentScene();

        if (currentScene == level1Scene && audioStereo.clip != level1Theme)
        {
            audioStereo.Stop();
            audioStereo.clip = level1Theme;
            audioStereo.Play();

            return;
        }
        if (currentScene == level2Scene && audioStereo.clip != level2Theme)
        {
            audioStereo.Stop();
            audioStereo.clip = level2Theme;
            audioStereo.Play();

            return;
        }
        if (currentScene == level3Scene && audioStereo.clip != level3Theme)
        {
            audioStereo.Stop();
            audioStereo.clip = level3Theme;
            audioStereo.Play();

            return;
        }
        if (currentScene == mainMenuScene && audioStereo.clip != menuTheme)
        {
            audioStereo.Stop();
            audioStereo.clip = menuTheme;
            audioStereo.Play();

            return;
        }
        for (int i = 0; i < popeScenes.Count; i++)
        {
            if (currentScene == popeScenes[i] && audioStereo.clip != popeTheme)
            {
                audioStereo.Stop();
                audioStereo.clip = popeTheme;
                audioStereo.Play();

                return;
            }
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
                gameObject.layer = LayerMask.NameToLayer("Blood"); 
            }
            if (!GrantCheckpoint)
            {
                gameObject.transform.parent = cultistObject.transform;
                cultistList.Add(gameObject);
                gameObject.layer = LayerMask.NameToLayer("Blood"); 
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
                gameObject.layer = LayerMask.NameToLayer("Blood"); 
            }
            if (!GrantCheckpoint)
            {
                gameObject.transform.parent = cultistObject.transform;
                cultistList.Add(gameObject);
                gameObject.layer = LayerMask.NameToLayer("Blood");
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

    public void HorseInstance(GameObject horse)
    {
        if (!SearchAndDestroy(horse))
        {
            horseObject = transform.GetChild(1).gameObject;

            horse.transform.parent = horseObject.transform;

            //cultistList.Add(manager);
        }
    }

    public void RecieveCheckpoint(Vector2 position)
    {
        NextDialogueObject();

        GrantCheckpoint = true;
        Checkpoint = position;
    }

    public void RecieveDeath()
    {
        if (currentScene == level1Scene)
        {
            deathCount1++;
        }
        if (currentScene == level2Scene)
        {
            deathCount2++;
        }
        if (currentScene == level3Scene)
        {
            deathCount3++;
        }
    }

    public void NextDialogueObject()
    {
        dialogueLine = 1;
    }


    public void StageEnd()
    {
        GrantCheckpoint = false;
        //IsLevelCleared = false;

        WipeLists(true);
    }

    public void WipeLists(bool wipeAll) 
    {
        dialogueLine = 1;

        // If demonphase is true then wipe all lists from both the cultist and the demonobject
        // But if demonphase is false then only wipe the lists from the demonobject


        if (GrantCheckpoint || wipeAll)
        {
            for (int i = 0; i < demonObject.transform.childCount; i++)
            {
                Destroy(demonObject.transform.GetChild(i).gameObject);
            }
        }
        if (!GrantCheckpoint || wipeAll)
        {
            for (int i = 0; i < cultistObject.transform.childCount; i++)
            {
                Destroy(cultistObject.transform.GetChild(i).gameObject);
            }
        }
 
        if (wipeAll)
        {
            for (int i = 0; i < horseObject.transform.childCount; i++)
            {
                Destroy(horseObject.transform.GetChild(i).gameObject);
            }
        }

        if (GrantCheckpoint) { return; }

        Debug.Log("Clearing all lists");

        cultistList.Clear();
        demonList.Clear();
        weaponProjectiles.Clear();
        bloodManagers.Clear();
        enemyYes.Clear();
        doors.Clear();
        dialogueLine = 0;


        for (int i = 0; i < cultistList.Count; i++)
        {
            cultistList.RemoveAt(i);
        }

        for (int i = 0; i < demonList.Count; i++)
        {
            demonList.RemoveAt(i);
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

    [Header("Audio")]
    public AudioFileData[] audioDataArrey;

    [System.Serializable]
    public class AudioFileData
    {
        public string audioName;
        public AudioClip audioClip;
        [Range(0f, 1f)]
        public float volume;
    }

}
