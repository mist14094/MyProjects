using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using KTone.Core.KTIRFID;
using System.Drawing.Imaging;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Interface for image processing
    /// </summary>
    public interface IKTImageProcessor
    {
        #region Events
        #endregion Events

        #region Methods
        /// <summary>
        /// Uploads image in the form of memory Stream
        /// </summary>
        /// <param name="imageStream">memory stream</param>
        /// <param name="imageFormat">in which format image will save.e.g - .png,.jpg </param>
        /// <returns>flag</returns>
        bool Upload(MemoryStream imageStream, string imageFormat);
        /// <summary>
        /// Uploads the image from given path
        /// </summary>
        /// <param name="filePath">path from where image is to be uploaded</param>
        /// <returns>flag</returns>
        bool Upload(string filePath);
        /// <summary>
        /// Process image and extract serial no. from image.
        /// </summary>
        /// <param name="fieldName">field name, e.g Serial number</param>
        ///<param name="productId"></param>
        ///<param name="skuId"></param>
        /// <out param name="serialNo"></param>
        /// <out param name="score"></param>
        /// <out param name="imageReadStatus"></param>
        /// <param name="generateSerailNoForUnrecognaizedProduct"></param>
        /// <out param name="msg"></param>
       /// <returns>field value</returns>
        void ProcessImage(FieldName fieldName, 
                          int productId,
                          int skuId,
                          bool generateSerailNoForUnrecognaizedProduct,  
                          out string serialNo, 
                          out double score, 
                          out ImageReadStatus imageReadStatus, 
                          out string msg);
        /// <summary>
        /// Saves image present in memory.
        /// </summary>
        /// <returns>Relative path of image</returns>
        string SaveImage();
        /// <summary>
        /// Clears current image memory stream.
        /// </summary>
        /// <returns>flag</returns>
        bool Clear();

        /// <summary>
        /// To show configuration info on test window
        /// </summary>
        /// <param name="productAlgoMapping"></param>
        /// <param name="algoToolConfigurationSettings"></param>
        /// <param name="useCognex"></param>
        void GetDataForTest(out Dictionary<string, string> productAlgoMapping, out Dictionary<string, List<ImageProcessingCognexToolDetails>> algoToolConfigurationSettings, out  bool useCognex);

        #endregion Methods

        #region Attributes
        /// <summary>
        /// Station status.
        /// </summary>
        ImageProcessorStatus ImageProcessorStatus { get; }
        /// <summary>
        /// Gets Current Memory Stream.
        /// </summary>
        MemoryStream CurrentImageMemoryStream { get; }
        /// <summary>
        /// Gets or Sets the ClearImage TimeOut.
        /// </summary>
        int ClearImageTimeOut { get; set; }

        ImageFormat ImageFormatExtension { get; set; }
        #endregion Attributes
    }
    /// <summary>
    /// Image Processor Status: station can be in one of 2 states, IDLE OR PROCESSING
    /// </summary>

    public enum ImageProcessorStatus
    {
        /// <summary>
        /// ImageProcessor IS IDLE.
        /// </summary>
        IDLE,
        /// <summary>
        /// ImageProcessor IS IN PROCESSING.
        /// </summary>
        PROCESSING,
    }
}
