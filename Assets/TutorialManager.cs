using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] GameObject enemy;
    [SerializeField] Transform player;
    [SerializeField] GameObject[] arrows;
    [SerializeField] InterfaceManager interfaceManager;
    [SerializeField] BulletManager shootManager;

    int tutorialPhase = 0;
    int lastTutorialPhase = 0;
    int enemiesAlive = 0;
    bool spawnedEnemies = false;
    bool usedDebuff = false;

    string[] phrases = {"Use left joystick to move",
                        "Use right joystick to aim",
                        "Press R1 or R2 to shoot",
                        "Kill these enemies",
                        "Use L1 to debuff your enemies",
                        "Press Square to use your secondary attack and kill these enemies",
                        "Additional notes (Press X to continue)",
                        "There are 4 other universes, your help is needed",
                        "One at a time, you will need to sync with the other universes and respective people",
                        "You will not be able to damage enemies from that universe",
                        "But you can make the life easier or harder for the other person",
                        "The contrary applies to our universe",
                        "Harder enemies give more points, easier ones give less",
                        "Killing multiple enemies within a short period of time leads to a combo",
                        "If you take damage, it will also be multiplied by your current combo, BE CAREFUL",
                        "Press X to leave tutorial"};

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject a in arrows){
            a.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorialText.text == ""){
            tutorialText.text = phrases[tutorialPhase];
        }
        switch(tutorialPhase){
            case -1:
                break;
            case 0:
                if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f){
                    StartCoroutine(getNextTutorialPhase());
                }
                break;
            case 1:
                if(Mathf.Abs(Input.GetAxis("MoveWeaponHorizontal")) > 0.75f || Mathf.Abs(Input.GetAxis("MoveWeaponVertical")) > 0.75f){
                    StartCoroutine(getNextTutorialPhase());
                }
                break;
            case 2:
                if(Input.GetAxis("FireNew") > 0.1f || Input.GetButton("Fire")){
                    StartCoroutine(getNextTutorialPhase());
                }
                break;
            case 3:
                if(!spawnedEnemies){
                    enemiesAlive = 5;
                    for(int i = 0; i < 5; i++){
                        Vector3 enemyOffset = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(Random.Range(3f, 5f), Random.Range(3f, 5f), 0);
                        GameObject enemyspawned = Instantiate(enemy, player.position + enemyOffset, Quaternion.Euler(0, 0, 0));
                        enemyspawned.GetComponent<EnemyBehavior>().setTutorialFlag();
                        if(i == 1){
                            enemyspawned.GetComponent<EnemyBehavior>().buff();
                        }
                        if(i == 2){
                            enemyspawned.GetComponent<EnemyBehavior>().receiveDamage(1f,Vector3.zero,0f);
                        }
                        if(i == 3){
                            enemyspawned.GetComponent<EnemyBehavior>().receiveDamage(1f,Vector3.zero,0f);
                            enemyspawned.GetComponent<EnemyBehavior>().receiveDamage(1f,Vector3.zero,0f);
                        }
                        if(i == 4){
                            enemyspawned.GetComponent<EnemyBehavior>().nerf();
                        }
                    }
                    spawnedEnemies = true;
                }else{
                    if(enemiesAlive == 0){
                        StartCoroutine(getNextTutorialPhase());
                    }
                }
                break;
            case 4:
                if(!spawnedEnemies){
                    enemiesAlive = 5;
                    spawnedEnemies = true;
                    arrows[0].SetActive(true);
                    for (int i = 0; i < 5; i++){
                        Vector3 enemyOffset = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(Random.Range(3f, 5f), Random.Range(3f, 5f), 0);
                        GameObject enemyspawned = Instantiate(enemy, player.position + enemyOffset, Quaternion.Euler(0, 0, 0));
                        enemyspawned.GetComponent<EnemyBehavior>().setTutorialFlag();
                    }
                }else{
                    if(enemiesAlive == 0 && usedDebuff){
                        foreach(GameObject a in arrows)
                        {
                            a.SetActive(false);
                        }
                        StartCoroutine(getNextTutorialPhase());
                    }else{
                        if(enemiesAlive == 0)
                            spawnedEnemies = false;
                        if (Input.GetButtonDown("ChooseEffect"))
                        {
                            StartCoroutine(showSecondArrow());
                        }
                        else if (Input.GetButtonUp("ChooseEffect"))
                        {
                            arrows[1].SetActive(false);
                            arrows[0].SetActive(true);
                        }
                    }
                }
                break;
            case 5:
                shootManager.canShoot = false;
                arrows[2].SetActive(true);
                if (!spawnedEnemies)
                {
                    enemiesAlive = 5;
                    spawnedEnemies = true;
                    arrows[2].SetActive(true);
                    for (int i = 0; i < 5; i++)
                    {
                        Vector3 enemyOffset = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(Random.Range(3f, 5f), Random.Range(3f, 5f), 0);
                        GameObject enemyspawned = Instantiate(enemy, player.position + enemyOffset, Quaternion.Euler(0, 0, 0));
                        enemyspawned.GetComponent<EnemyBehavior>().nerf();
                        enemyspawned.GetComponent<EnemyBehavior>().setTutorialFlag();
                    }
                }
                else
                {
                    if (enemiesAlive == 0 && usedDebuff)
                    {
                        arrows[2].SetActive(false);
                        shootManager.canShoot = true;
                        StartCoroutine(getNextTutorialPhase());
                    }
                }
                break;
        }
        if(tutorialPhase == phrases.Length-1 && Input.GetButtonDown("Continue"))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        if(tutorialPhase > 5 && Input.GetButtonDown("Continue"))
        {
            tutorialPhase = tutorialPhase + 1;
            tutorialText.text = "";
        }
        
    }

    IEnumerator getNextTutorialPhase(){
        lastTutorialPhase = tutorialPhase;
        tutorialPhase = -1;
        tutorialText.color = new Color(0f,204,0f,1f);
        Color tmp = tutorialText.color;

        while(tmp.a > 0f){
            tmp.a -= 0.01f;
            tutorialText.color = tmp;
            yield return new WaitForFixedUpdate();
        }
        tutorialText.text = "";
        tutorialText.color = new Color(255f,255f,255f,1f);
        tutorialPhase = lastTutorialPhase + 1;
        spawnedEnemies = false;
        yield return null;
    }

    IEnumerator showSecondArrow()
    {
        yield return new WaitForSeconds(0.1f);
        if (interfaceManager.isMenuOpen)
        {
            arrows[1].SetActive(true);
            arrows[0].SetActive(false);
        }
        yield return null;
    }

    internal void decreaseEnemyCount()
    {
        enemiesAlive -= 1;
    }

    internal void getEffect(int effect){
        if(effect == 3 && tutorialPhase == 4){
            usedDebuff = true;
        }
    }
}