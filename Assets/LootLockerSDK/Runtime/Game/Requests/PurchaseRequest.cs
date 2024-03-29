﻿using LootLocker.Requests;
using System;
using LootLocker.LootLockerEnums;
#if LOOTLOCKER_USE_NEWTONSOFTJSON
using Newtonsoft.Json;
#else
using LLlibs.ZeroDepJson;
#endif

namespace LootLocker.LootLockerEnums
{
    public enum SteamPurchaseRedemptionStatus
    {
        Init,
        Approved,
        Succeeded,
        Failed,
        Refunded,
        PartialRefund,
        ChargedBack,
        RefundedSuspectedFraud,
        RefundedFriendlyFraud
    }
}

namespace LootLocker.Requests
{
    #region Legacy Purchasing
    //TODO: Deprecate legacy purchasing

    public class LootLockerPurchaseRequests
    {
    }

    public class LootLockerNormalPurchaseRequest
    {
        public int asset_id { get; set; }
        public int variation_id { get; set; }
    }

    public class LootLockerRentalPurchaseRequest
    {
        public int asset_id { get; set; }
        public int variation_id { get; set; }
        public int rental_option_id { get; set; }
    }


    public class LootLockerPurchaseResponse : LootLockerResponse
    {
        public bool overlay { get; set; }
        public int order_id { get; set; }
    }

    public class LootLockerIosPurchaseVerificationRequest
    {
        public string receipt_data { get; set; }
    }

    public class LootLockerAndroidPurchaseVerificationRequest
    {
        public int asset_id { get; set; }
        public string purchase_token { get; set; }
    }

    public class LootLockerPurchaseCatalogItemResponse : LootLockerResponse
    {

    }

    public class LootLockerPurchaseOrderStatus : LootLockerResponse
    {
        public string status { get; set; }
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerCatalogItemAndQuantityPair
    {
        /// <summary>
         /// The unique listing id of the catalog item to purchase
         /// </summary>
        public string catalog_listing_id { get; set; }
        /// <summary>
         /// The quantity of the specified item to purchase
         /// </summary>
        public int quantity { get; set; }
    }

    /// <summary>
     /// 
     /// </summary>
    public class LootLockerPurchaseCatalogItemRequest
    {
        /// <summary>
         /// The id of the wallet to be used for the purchase
         /// </summary>
        public string wallet_id { get; set; }
        /// <summary>
         /// A list of items to purchase
         /// </summary>
        public LootLockerCatalogItemAndQuantityPair[] items { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerRedeemAppleAppStorePurchaseForPlayerRequest
    {
        /// <summary>
        /// Whether or not to use the app store sandbox for this redemption
        /// </summary>
        public bool sandboxed { get; set; } = false;
        /// <summary>
        /// The id of the transaction successfully made towards the Apple App Store
        /// </summary>
        public string transaction_id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerRedeemAppleAppStorePurchaseForClassRequest : LootLockerRedeemAppleAppStorePurchaseForPlayerRequest
    {
        /// <summary>
        /// The id of the class to redeem this transaction for
        /// </summary>
#if LOOTLOCKER_USE_NEWTONSOFTJSON
        [JsonProperty("character_id")]
#else
        [Json(Name = "character_id")]
#endif
        public int class_id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerRedeemGooglePlayStorePurchaseForPlayerRequest
    {
        /// <summary>
        /// The id of the product that this redemption refers to
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// The token from the purchase successfully made towards the Google Play Store
        /// </summary>
        public string purchase_token { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerRedeemGooglePlayStorePurchaseForClassRequest : LootLockerRedeemGooglePlayStorePurchaseForPlayerRequest
    {
        /// <summary>
        /// The id of the class to redeem this purchase for
        /// </summary>
#if LOOTLOCKER_USE_NEWTONSOFTJSON
        [JsonProperty("character_id")]
#else
        [Json(Name = "character_id")]
#endif
        public int class_id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerBeginSteamPurchaseRedemptionRequest
    {
        /// <summary>
        /// Id of the Steam User that is making the purchase
        /// </summary>
        public string steam_id { get; set; }
        /// <summary>
        /// The currency to use for the purchase
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// The language to use for the purchase
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// The LootLocker Catalog Item Id for the item you wish to purchase
        /// </summary>
        public string catalog_item_id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerBeginSteamPurchaseRedemptionForClassRequest : LootLockerBeginSteamPurchaseRedemptionRequest
    {
        /// <summary>
        /// Id of the class to make the purchase for
        /// </summary>
        public int class_id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerBeginSteamPurchaseRedemptionResponse : LootLockerResponse
    {
        /// <summary>
        /// Was the purchase redemption process started successfully
        /// </summary>
        public bool isSuccess { get; set; }
        /// <summary>
        /// The id of the entitlement this purchase relates to
        /// </summary>
        public string entitlement_id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerQuerySteamPurchaseRedemptionStatusRequest
    {
        /// <summary>
        /// The id of the entitlement to check the status for
        /// </summary>
        public string entitlement_id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerQuerySteamPurchaseRedemptionStatusResponse : LootLockerResponse
    {
        /// <summary>
        /// The status of the steam purchase
        /// </summary>
        public SteamPurchaseRedemptionStatus status { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LootLockerFinalizeSteamPurchaseRedemptionRequest
    {
        /// <summary>
        /// The id of the entitlement to finalize the purchase for
        /// </summary>
        public string entitlement_id { get; set; }
    }
}

namespace LootLocker
{
    public partial class LootLockerAPIManager
    {
#region Legacy Purchasing
// TODO: Deprecate legacy purchasing
        public static void NormalPurchaseCall(LootLockerNormalPurchaseRequest[] data, Action<LootLockerPurchaseResponse> onComplete)
        {
            if(data == null)
            {
            	onComplete?.Invoke(LootLockerResponseFactory.InputUnserializableError<LootLockerPurchaseResponse>());
            	return;
            }

            string json = LootLockerJson.SerializeObject(data);

            EndPointClass endPoint = LootLockerEndPoints.normalPurchaseCall;

            LootLockerServerRequest.CallAPI(endPoint.endPoint, endPoint.httpMethod, json, (serverResponse) => { LootLockerResponse.Deserialize(onComplete, serverResponse); });
        }

        public static void RentalPurchaseCall(LootLockerRentalPurchaseRequest data, Action<LootLockerPurchaseResponse> onComplete)
        {
            if(data == null)
            {
            	onComplete?.Invoke(LootLockerResponseFactory.InputUnserializableError<LootLockerPurchaseResponse>());
            	return;
            }

            string json = LootLockerJson.SerializeObject(data);

            EndPointClass endPoint = LootLockerEndPoints.rentalPurchaseCall;

            LootLockerServerRequest.CallAPI(endPoint.endPoint, endPoint.httpMethod, json, (serverResponse) => { LootLockerResponse.Deserialize(onComplete, serverResponse); });
        }

        public static void IosPurchaseVerification(LootLockerIosPurchaseVerificationRequest[] data, Action<LootLockerPurchaseResponse> onComplete)
        {
            if(data == null)
            {
            	onComplete?.Invoke(LootLockerResponseFactory.InputUnserializableError<LootLockerPurchaseResponse>());
            	return;
            }

            string json = LootLockerJson.SerializeObject(data);

            EndPointClass endPoint = LootLockerEndPoints.iosPurchaseVerification;

            LootLockerServerRequest.CallAPI(endPoint.endPoint, endPoint.httpMethod, json, (serverResponse) => { LootLockerResponse.Deserialize(onComplete, serverResponse); });
        }

        public static void AndroidPurchaseVerification(LootLockerAndroidPurchaseVerificationRequest[] data, Action<LootLockerPurchaseResponse> onComplete)
        {
            if(data == null)
            {
            	onComplete?.Invoke(LootLockerResponseFactory.InputUnserializableError<LootLockerPurchaseResponse>());
            	return;
            }

            string json = LootLockerJson.SerializeObject(data);

            EndPointClass endPoint = LootLockerEndPoints.androidPurchaseVerification;

            LootLockerServerRequest.CallAPI(endPoint.endPoint, endPoint.httpMethod, json, (serverResponse) => { LootLockerResponse.Deserialize(onComplete, serverResponse); });
        }

        public static void PollOrderStatus(LootLockerGetRequest lootLockerGetRequest, Action<LootLockerPurchaseOrderStatus> onComplete)
        {
            EndPointClass endPoint = LootLockerEndPoints.pollingOrderStatus;

            string getVariable = string.Format(endPoint.endPoint, lootLockerGetRequest.getRequests[0]);

            LootLockerServerRequest.CallAPI(getVariable, endPoint.httpMethod, "", (serverResponse) => { LootLockerResponse.Deserialize(onComplete, serverResponse); });
        }

        public static void ActivateRentalAsset(LootLockerGetRequest lootLockerGetRequest, Action<LootLockerActivateRentalAssetResponse> onComplete)
        {
            EndPointClass endPoint = LootLockerEndPoints.activatingARentalAsset;

            string getVariable = string.Format(endPoint.endPoint, lootLockerGetRequest.getRequests[0]);

            LootLockerServerRequest.CallAPI(getVariable, endPoint.httpMethod, "", (serverResponse) => { LootLockerResponse.Deserialize(onComplete, serverResponse); });
        }
#endregion
    }
}