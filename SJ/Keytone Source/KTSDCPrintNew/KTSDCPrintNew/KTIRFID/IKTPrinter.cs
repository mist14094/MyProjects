using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Defines methods to print RFID labels.
    /// </summary>
    public interface IKTPrinter
    {
        event EventHandler<KTPrinterEventArgs> KTPrintertEvent;
        /// <summary>
        /// Prints RFID labels.
        /// </summary>
        /// <param name="printDataXml">Xml string containing the data to be printed</param>
        /// <param name="numOfLabels">number of labels to be printed</param>
        /// <param name="numOfCopies">number of copies of each label, to be printed</param>
        /// <param name="incrementSrNo">flag that indicates whether to increment the serial no</param>
        /// <param name="errorMsg">error message if there is any print failure</param>
        /// <param name="operatorName">operatorName</param>
        /// <param name="machineName">machineName</param>
        /// <returns>true if print is executed successfully, otherwise returns false. Check errorMsg parameter for error details.</returns>
        bool Print(string printDataXml, int numOfLabels, int numOfCopies, bool incrementSrNo, out string errorMsg, string operatorName, string machineName);
        bool Print(Dictionary<string, string> getValues, int numOfCopies, out string errorMsg);


        // Verify
        // Kill

        /// <summary>
        /// Returns fields defined in the label
        /// </summary>
        /// <returns></returns>
        List<string> GetLabelFields();

        /// <summary>
        /// Returns the label category
        /// </summary>
        //LabelCategory LabelCategory
        //{
        //    get;
        //}

        /// <summary>
        /// Returns the label name
        /// </summary>
        string Label
        {
            get;
        }

        /// <summary>
        /// Returns the input file name
        /// </summary>
        string InputFileName
        {
            get;
        }

        /// <summary>
        /// Returns Selected printer
        /// </summary>
        string SelectedPrinter
        {
            get;
        }

        int TimeOut
        {
            get;
        }
    }

   

   
}
