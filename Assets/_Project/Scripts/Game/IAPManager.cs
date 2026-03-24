using System;
using UnityEngine;
using LastLineDefense.Save;

namespace LastLineDefense.Game
{
    public class IAPManager : MonoBehaviour
    {
        public static IAPManager Instance { get; private set; }

        public event Action<string> OnPurchaseComplete;

        private SaveManager saveManager;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            saveManager = FindAnyObjectByType<SaveManager>();
        }

        public void PurchaseRemoveAds()
        {
            SimulatePurchase("remove_ads", () =>
            {
                if (saveManager != null)
                {
                    var data = saveManager.GetSaveData();
                    data.adRemovalPurchased = true;
                    saveManager.Save();
                }
                Debug.Log("[IAP] Ad removal purchased");
            });
        }

        public void PurchaseStarterPack()
        {
            SimulatePurchase("starter_pack", () =>
            {
                if (saveManager != null)
                {
                    saveManager.AddCoins(1000);
                    var data = saveManager.GetSaveData();
                    data.totalGems += 50;
                    saveManager.Save();
                }
                Debug.Log("[IAP] Starter pack purchased");
            });
        }

        public void PurchaseCoinPackSmall()
        {
            SimulatePurchase("coin_pack_s", () =>
            {
                if (saveManager != null)
                    saveManager.AddCoins(500);
                Debug.Log("[IAP] Coin Pack S purchased");
            });
        }

        public void PurchaseCoinPackMedium()
        {
            SimulatePurchase("coin_pack_m", () =>
            {
                if (saveManager != null)
                {
                    saveManager.AddCoins(1500);
                    var data = saveManager.GetSaveData();
                    data.totalGems += 10;
                    saveManager.Save();
                }
                Debug.Log("[IAP] Coin Pack M purchased");
            });
        }

        private void SimulatePurchase(string productId, Action onSuccess)
        {
            Debug.Log($"[IAP] Simulating purchase: {productId}");
            onSuccess?.Invoke();
            OnPurchaseComplete?.Invoke(productId);
        }
    }
}
