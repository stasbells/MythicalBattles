using Reflex.Extensions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLevelSelector
{
    public class LevelSelectionCarousel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Settings")]
        [SerializeField] private float _levelSpacing = 925f;
        [SerializeField] private float _snapSpeed = 5f;
        [SerializeField] private float _scaleFactor = 0.4f;
        [SerializeField] private float _dragThreshold = 50f;

        [Header("References")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _content;
        [SerializeField] private List<LevelButton> _levelButtons = new();
        [SerializeField] private TMP_Text _levelTimeRecord;
        [SerializeField] private TMP_Text _levelScore;
        [SerializeField] private GameObject _results;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;

        private int _currentLevelIndex;
        private bool _isUnlocked = true;
        private bool _isDragging = false;
        private Vector2 _startDragPosition;
        private IPersistentData _persistentData;

        public int CurrentLevelNumber => _currentLevelIndex + 1;

        private void Construct()
        {
            _persistentData = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPersistentData>();
        }

        private void Awake()
        {
            Construct();

            InitializeContent();

            _currentLevelIndex = _persistentData.GameProgressData.GetLastUnlockedLevelNumber() - 1;

            UpdateButtons();
        }

        private void OnEnable()
        {
            _scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
            _leftArrow.onClick.AddListener(ScrollLeft);
            _rightArrow.onClick.AddListener(ScrollRight);
        }

        private void OnDisable()
        {
            _scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
            _leftArrow.onClick.RemoveListener(ScrollLeft);
            _rightArrow.onClick.RemoveListener(ScrollRight);
        }

        private void Update()
        {
            if (!_isDragging)
                SnapToCurrentLevel();
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _startDragPosition = _content.anchoredPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;

            float dragDistance = _content.anchoredPosition.x - _startDragPosition.x;

            if (Mathf.Abs(dragDistance) > _dragThreshold)
            {
                if (dragDistance > 0 && _currentLevelIndex > 0)
                    _currentLevelIndex--;
                else if (dragDistance < 0 && _currentLevelIndex < _levelButtons.Count - 1)
                    _currentLevelIndex++;
            }

            UpdateButtons();
        }

        private void InitializeContent()
        {
            _content.sizeDelta = new Vector2(_levelButtons.Count * _levelSpacing, _content.sizeDelta.y);
        }

        private void OnScrollValueChanged(Vector2 value)
        {
            if (!_isDragging)
                return;

            UpdateVisuals();
        }

        private void SnapToCurrentLevel()
        {
            float targetPosition = (float)_currentLevelIndex / (_levelButtons.Count - 1);

            _scrollRect.horizontalNormalizedPosition = Mathf.Lerp
                (_scrollRect.horizontalNormalizedPosition, targetPosition, _snapSpeed * Time.deltaTime);

            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                float viewportX = _content.anchoredPosition.x + _levelButtons[i].RectTransform.anchoredPosition.x;
                float distance = Mathf.Abs(viewportX) / _levelSpacing;

                float scale = Mathf.Clamp(1 - distance * (1 - _scaleFactor), _scaleFactor, 1f);
                _levelButtons[i].RectTransform.localScale = Vector3.one * scale;
            }
        }

        private void ScrollLeft()
        {
            if (_currentLevelIndex > 0)
            {
                _currentLevelIndex--;

                UpdateButtons();
            }
        }

        private void ScrollRight()
        {
            if (_currentLevelIndex < _levelButtons.Count - 1)
            {
                _currentLevelIndex++;

                UpdateButtons();
            }
        }

        private void UpdateButtons()
        {
            int lastUnlockedLevelNumber = _persistentData.GameProgressData.GetLastUnlockedLevelNumber();

            _isUnlocked = lastUnlockedLevelNumber - 1 >= _currentLevelIndex;

            for (int i = 0; i < _levelButtons.Count; i++)
            {
                if (i <= _currentLevelIndex)
                {
                    _playButton.interactable = _isUnlocked;

                    if (_isUnlocked)
                    {
                        _results.SetActive(true);

                        _levelButtons[i].SetLocked(!_isUnlocked);

                        int score = (int)_persistentData.GameProgressData.GetLevelRecordPoints(i + 1);

                        _levelScore.text = score.ToString();

                        float time = _persistentData.GameProgressData.GetLevelRecordTime(i + 1);

                        _levelTimeRecord.text = TimeFormatter.GetTimeInString(time);
                    }
                    else
                    {
                        _results.SetActive(false);
                    }
                }
            }

            UpdateArrowsVisibility();
        }

        private void UpdateArrowsVisibility()
        {
            _leftArrow.interactable = _currentLevelIndex > 0;
            _rightArrow.interactable = _currentLevelIndex < _levelButtons.Count - 1;
        }
    }
}