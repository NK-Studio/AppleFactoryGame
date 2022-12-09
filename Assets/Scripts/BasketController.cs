using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    private enum SoundKind
    {
        Apple,
        Bomb
    }

    private Camera _camera;
    [SerializeField] private Transform Basket;

    [SerializeField] private AudioSource SFXSource;

    [SerializeField] private AudioClip[] SFXClips;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ => Move())
            .AddTo(this);

        Basket.OnTriggerEnterAsObservable()
            .Subscribe(other => OnEnterObject(other))
            .AddTo(this);
    }

    private void OnEnterObject(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            PlaySound(SoundKind.Apple);
            UpdatePoint(+30);
        }
        else if (other.CompareTag("Bomb"))
        {
            PlaySound(SoundKind.Bomb);
            UpdatePoint(-15);
        }

        Destroy(other.gameObject);
    }

    private void UpdatePoint(int point)
    {
        int nextValue = GameManager.Instance.Point + point;
        GameManager.Instance.Point = Mathf.Clamp(nextValue, 0, int.MaxValue);
    }

    private void PlaySound(SoundKind kind)
    {
        SFXSource.PlayOneShot(SFXClips[(int)kind]);
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);

                Basket.position = new Vector3(x, 0, z);
            }
        }
    }
}