using System.Collections;
using UnityEngine;


public class BlobShadow : MonoBehaviour
{
    [SerializeField] private GameObject _shadowPrefab;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _scaleFactor = 1.0f;
    [SerializeField] private float _maxScale = 1.0f;
    [SerializeField] private float _minScale = 0.5f;

    private GameObject _shadowInstance;

    private void Start()
    {
        _shadowInstance = Instantiate(_shadowPrefab);
        _shadowInstance.SetActive(false);
        StartCoroutine(updateShadow());

    }

    IEnumerator updateShadow()
    {
        while (true)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 100, _groundLayer))
            {
                if (!_shadowInstance.activeInHierarchy)
                {
                    _shadowInstance.SetActive(true);
                }
                _shadowInstance.transform.position = hit.point + Vector3.up * 0.1f;
                _shadowInstance.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                _shadowInstance.transform.localScale = Vector3.one * Mathf.Clamp(_scaleFactor * hit.distance, _minScale, _maxScale);
            }
            else
            {
                if (_shadowInstance.activeInHierarchy)
                {
                    _shadowInstance.SetActive(false);
                }
                Destroy(_shadowInstance);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
        
    

    void OnDisable()
    {
        if (_shadowInstance != null)
        {
            Destroy(_shadowInstance);
        }
    }

    // draw gizmo of the Raycast
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * 100);

        // draw a sphere at the hit point
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100, _groundLayer))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }
}