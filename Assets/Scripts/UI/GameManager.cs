using System;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UIDocument GameUI;
    public UIDocument GameEnd;

    public FloatReactiveProperty TimeCount { get; set; } = new(60f);
    public int Point { get; set; }

    public Label PointText;
    public Label ScorePointText;

    private CompositeDisposable _disposables = new(); // field

    private void Awake()
    {
        VisualElement root = GameUI.rootVisualElement;
        PointText = root.Q<Label>("TimeCount");
        ScorePointText = root.Q<Label>("ScorePoint");

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Where(_ => TimeCount.Value > 0)
            .Subscribe(_ => TimeCount.Value -= 1)
            .AddTo(_disposables);

        TimeCount.Where(value => value < 1)
            .Subscribe(_ =>
            {
                ShowEnd(Point);
                _disposables.Clear();
                Time.timeScale = 0;
            })
            .AddTo(this);
    }

    private void Update()
    {
        PointText.text = $"{TimeCount:N0}";
        ScorePointText.text = $"{Point} : Point";
    }

    private void ShowEnd(int value)
    {
        GameEnd.gameObject.SetActive(true);

        var root = GameEnd.rootVisualElement;
        var resultText = root.Q<Label>("ResultPoint");

        resultText.text = $"당신의 점수는 {value}점 입니다.";
    }

    [Button("게임 끝내기")]
    private void OnTestDead()
    {
        TimeCount.Value = 2;
    }

    [Button("점수 올리기")]
    private void OnTestPointUp()
    {
        UpdatePoint(+30);
    }

    [Button("점수 내리기")]
    private void OnTestPointDown()
    {
        UpdatePoint(-15);
    }

    private void UpdatePoint(int point)
    {
        Point += point;
        Point = Mathf.Clamp(Point, 0, int.MaxValue);
    }


    private void OnDestroy()
    {
        _disposables.Clear();
    }
}