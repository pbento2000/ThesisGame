using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] GameObject enemy;
    [SerializeField] Transform player;

    int tutorialPhase = 0;
    int lastTutorialPhase = 0;
    int enemiesAlive = 0;
    bool spawnedEnemies = false;
    bool usedDebuff = false;

    string[] phrases = {"Use left joystick to move",
                        "Use right joystick to aim",
                        "Press R1 or R2 to shoot",
                        "Kill these enemies",
                        "Use L1 to debuff your enemies"};

    // Start is called before the first frame update
    void Start()
    {
        
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
                if(Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Vertical") > 0.1f){
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
                    for(int i = 0; i < 5; i++){
                        Vector3 enemyOffset = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(Random.Range(3f, 5f), Random.Range(3f, 5f), 0);
                        GameObject enemyspawned = Instantiate(enemy, player.position + enemyOffset, Quaternion.Euler(0, 0, 0));
                        enemyspawned.GetComponent<EnemyBehavior>().setTutorialFlag();
                    }
                }else{
                    if(enemiesAlive == 0 && usedDebuff){
                        StartCoroutine(getNextTutorialPhase());
                    }else{
                        if(enemiesAlive == 0)
                            spawnedEnemies = false; 
                    }
                }
                break;
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