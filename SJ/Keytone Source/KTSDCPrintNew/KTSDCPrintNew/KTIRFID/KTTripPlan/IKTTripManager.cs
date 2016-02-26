using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTone.Core.KTIRFID
{
 

    public interface IKTTripManager
    {
        #region Methods
        /// <summary>
        /// Gets all active Trip Plan defined in system for paticular dataOwnerId
        /// </summary>
        /// <param name="dataOwnerId"></param>
        /// <returns></returns>
        KTTripPlanDetails[] GetAllTripPlans();

        /// <summary>
        /// Gets all Asset involve in trip
        /// </summary>
        /// <param name="dataOwnerId"></param>
        /// <returns></returns>
        TripAsset[] GetAllActiveAsset();


        /// <summary>
        /// Gets all Asset involve in trip
        /// </summary>
        /// <param name="dataOwnerId"></param>
        /// <returns></returns>
        TripAsset[] GetAllActiveAsset(int tripPlanId);

        /// <summary>
        /// Gets the trip detail for particular asset
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        TripAsset GetAssetTripDetail(long assetId);

        /// <summary>
        ///  Gets the trip detail for particular asset
        /// </summary>
        /// <param name="externalTagId"></param>
        /// <returns></returns>
        TripAsset GetAssetTripDetail(string externalTagId);

     
        #endregion Methods

        #region Events

        event EventHandler<TripPlanEventArgs> TripMovement;

        event EventHandler<TripPlanEventArgs> TripStarted;

        event EventHandler<TripPlanEventArgs> TripCompleted;

        event EventHandler<TripPlanEventArgs> TripViolations;

        #endregion
    }
}
