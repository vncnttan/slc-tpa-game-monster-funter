using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;

public class AimController : MonoBehaviour {

    [SerializeField] private CinemachineFreeLook aimCam;
    [SerializeField] private Image CrossHair; 
    [SerializeField] private LayerMask aimLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    private PlayerStateManager conn;
    [SerializeField] private Transform pfProjectile;
    [SerializeField] private Transform spawnBullet;
    private void Awake(){
        conn = GetComponent<PlayerStateManager>();
    }

    void Update(){
        Vector3 MousePosition = Vector3.zero;

        Vector2 ScreenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimLayerMask)){
            // transform.position = raycastHit.point;
            debugTransform.position = raycastHit.point;
            MousePosition = raycastHit.point;
        }

        if(Input.GetKeyDown(KeyCode.Mouse1)){
            conn.isAiming = true;
        } else if (Input.GetKey(KeyCode.Mouse1)) {
            aimCam.gameObject.SetActive(true);
            CrossHair.gameObject.SetActive(true);

            // Rigging
            Vector3 worldAimTarget = MousePosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            conn.transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 25f);
        } else if (Input.GetKeyUp(KeyCode.Mouse1)) {
            conn.isAiming = false;
        } else {
            aimCam.gameObject.SetActive(false);
            CrossHair.gameObject.SetActive(false);
        } 

        if(conn.isAiming && Input.GetKeyDown(KeyCode.Mouse0)){
            StartCoroutine(KnockbackCam());
            Vector3 aimDirection = (MousePosition - spawnBullet.position).normalized;
            Instantiate(pfProjectile, spawnBullet.position, Quaternion.LookRotation(aimDirection, Vector3.up));
        }

    }

    IEnumerator KnockbackCam(){
        aimCam.m_Lens.FieldOfView = 42f;
        yield return new WaitForSeconds(0.03f);
        aimCam.m_Lens.FieldOfView = 44f;
        yield return new WaitForSeconds(0.03f);
        aimCam.m_Lens.FieldOfView = 46f;
        yield return new WaitForSeconds(0.03f);
        aimCam.m_Lens.FieldOfView = 44f;
        yield return new WaitForSeconds(0.03f);
        aimCam.m_Lens.FieldOfView = 42f;
        yield return new WaitForSeconds(0.03f);
        aimCam.m_Lens.FieldOfView = 40f;
    }
}