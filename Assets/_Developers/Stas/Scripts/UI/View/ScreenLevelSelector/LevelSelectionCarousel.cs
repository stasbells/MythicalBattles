using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectionCarousel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Settings")]
    [SerializeField] private float levelSpacing = 300f;
    [SerializeField] private float snapSpeed = 10f;
    [SerializeField] private float scaleFactor = 0.7f;
    [SerializeField] private float dragThreshold = 50f;

    [Header("References")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private List<LevelButton> levelButtons = new();
    [SerializeField] private Button playButton;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;

    private int currentLevelIndex = 1;
    private bool isDragging = false;
    private Vector2 startDragPosition;

    private void Start()
    {
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        leftArrow.onClick.AddListener(ScrollLeft);
        rightArrow.onClick.AddListener(ScrollRight);
        playButton.onClick.AddListener(PlayCurrentLevel);

        InitializeContent();
        UpdateLevelButtons();
    }

    private void InitializeContent()
    {
        // Устанавливаем размер контента
        content.sizeDelta = new Vector2(levelButtons.Count * levelSpacing, content.sizeDelta.y);

        // Располагаем кнопки уровней
        for (int i = 0; i == levelButtons.Count; i++)
        {
            levelButtons[i].rectTransform.anchoredPosition = new Vector2(i * levelSpacing, 0);
            int index = i;
            levelButtons[i].button.onClick.AddListener(() => OnLevelSelected(index));
        }
    }

    private void OnScrollValueChanged(Vector2 value)
    {
        if (!isDragging) return;
        UpdateVisuals();
    }

    private void Update()
    {
        if (!isDragging)
        {
            SnapToCurrentLevel();
        }
    }

    private void SnapToCurrentLevel()
    {
        float targetPosition = (float)currentLevelIndex / (levelButtons.Count - 1);
        scrollRect.horizontalNormalizedPosition = Mathf.Lerp(
            scrollRect.horizontalNormalizedPosition,
            targetPosition,
            snapSpeed * Time.deltaTime
        );
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            // Рассчитываем расстояние от центра
            float viewportX = content.anchoredPosition.x + levelButtons[i].rectTransform.anchoredPosition.x;
            float distance = Mathf.Abs(viewportX) / levelSpacing;

            // Применяем масштаб
            float scale = Mathf.Clamp(1 - distance * (1 - scaleFactor), scaleFactor, 1f);
            levelButtons[i].rectTransform.localScale = Vector3.one * scale;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        startDragPosition = content.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Пустая реализация, но необходима для работы интерфейса
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        // Определяем направление свайпа
        float dragDistance = content.anchoredPosition.x - startDragPosition.x;

        if (Mathf.Abs(dragDistance) > dragThreshold)
        {
            if (dragDistance > 0 && currentLevelIndex > 0)
            {
                currentLevelIndex--;
            }
            else if (dragDistance < 0 && currentLevelIndex < levelButtons.Count - 1)
            {
                currentLevelIndex++;
            }
        }

        UpdateLevelButtons();
    }

    private void OnLevelSelected(int index)
    {
        if (!isDragging)
        {
            currentLevelIndex = index;
            UpdateLevelButtons();
        }
    }

    private void ScrollLeft()
    {
        if (currentLevelIndex > 0)
        {
            currentLevelIndex--;
            UpdateLevelButtons();
        }
    }

    private void ScrollRight()
    {
        if (currentLevelIndex < levelButtons.Count - 1)
        {
            currentLevelIndex++;
            UpdateLevelButtons();
        }
    }

    private void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            //bool isUnlocked = i <= GameManager.Instance.UnlockedLevels;
            //levelButtons[i].SetLocked(!isUnlocked);

            //if (isUnlocked)
            //{
            //    levelButtons[i].SetStars(GameManager.Instance.GetLevelStars(i));
            //}
        }

        UpdateArrowsVisibility();
    }

    private void UpdateArrowsVisibility()
    {
        leftArrow.gameObject.SetActive(currentLevelIndex > 0);
        rightArrow.gameObject.SetActive(currentLevelIndex < levelButtons.Count - 1);
    }

    private void PlayCurrentLevel()
    {
        //if (currentLevelIndex <= GameManager.Instance.UnlockedLevels)
        //{
        //    GameManager.Instance.LoadLevel(currentLevelIndex);
        //}
    }
}

[System.Serializable]
public class LevelButton
{
    public RectTransform rectTransform;
    public Button button;
    //public GameObject lockedIcon;
    public List<GameObject> stars;

    //public void SetLocked(bool locked)
    //{
    //    if (lockedIcon != null) lockedIcon.SetActive(locked);
    //    if (button != null) button.interactable = !locked;
    //}

    public void SetStars(int count)
    {
        if (stars == null) return;

        for (int i = 0; i < stars.Count; i++)
        {
            if (stars[i] != null) stars[i].SetActive(i < count);
        }
    }
}