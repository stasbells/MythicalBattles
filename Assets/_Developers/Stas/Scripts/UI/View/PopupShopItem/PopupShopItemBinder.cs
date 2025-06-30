using Ami.BroAudio;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupB;
using Reflex.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.PopupShopItem
{
    public class PopupShopItemBinder : PopupBinder<PopupShopItemViewModel>
    {
        private const float AlphaChangeFactor = 0.4f;

        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Button _buyItemButton;
        [SerializeField] private TMP_Text _itemStatsText;
        [SerializeField] private TMP_Text _itemTypeText;
        [SerializeField] private TMP_Text _priceCountText;
        [SerializeField] private TMP_Text _priceText;

        private IWallet _wallet;
        private IPersistentData _persistentData;
        private IDataProvider _dataProvider;
        private IAudioPlayback _audioPlayback;
        private ShopItemView _shopItemView;
        private IShopItemVisitor _itemSelector;

        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _persistentData = container.Resolve<IPersistentData>();
            _dataProvider = container.Resolve<IDataProvider>();
            _wallet = container.Resolve<IWallet>();
            _audioPlayback = container.Resolve<IAudioPlayback>();
            _itemSelector = (IShopItemVisitor)container.Resolve<IItemSelector>();
        }

        protected override void Start()
        {
            base.Start();

            Construct();

            _shopItemView = ViewModel.ShopItemView;

            _backgroundImage.sprite = _shopItemView.Item.BackgroundImage;
            _contentImage.sprite = _shopItemView.Item.ItemImage;
            _itemStatsText.text = _shopItemView.Item.DisplayText;

            if (_shopItemView.Item is EquipmentItem item)
            {
                _itemTypeText.text = item.TypeText;
                _itemTypeText.color = item.GradeTextColor;
            }
            else
            {
                _itemTypeText.gameObject.SetActive(false);
            }

            if (_shopItemView.IsAvailableToBuy)
            {
                _priceCountText.text = _shopItemView.Price.ToString();
                
                _buyItemButton.onClick.AddListener(OnBuyItemButtonClicked);

                if (_wallet.GetCurrentCoins() < _shopItemView.Price)
                {
                    _priceCountText.color = Color.red;

                    _buyItemButton.interactable = false;

                    Image buttonImage = _buyItemButton.GetComponent<Image>();

                    Color buttonColor = buttonImage.color;

                    buttonColor.a = AlphaChangeFactor;

                    buttonImage.color = buttonColor;
                }
            }
            else
            {
                _priceCountText.gameObject.SetActive(false);
                _priceText.gameObject.SetActive(false);
                _buyItemButton.gameObject.SetActive(false);
            }
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _buyItemButton.onClick?.RemoveListener(OnBuyItemButtonClicked);
        }
        
        private void OnBuyItemButtonClicked()
        {
            if (_wallet.GetCurrentCoins() < _shopItemView.Price)
                return;
            
            _wallet.Spend(_shopItemView.Price);
            
            _itemSelector.Visit(_shopItemView.Item);

            SoundID paySound = _audioPlayback.AudioContainer.PayMoney;
            
            _audioPlayback.PlaySound(paySound);
            
            _dataProvider.SavePlayerData();
            
            ViewModel.ShopPanel.Refresh();
        }
    }
}