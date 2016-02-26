using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// 
    /// </summary>
    public interface IKTAlienReader
    {
        #region ByteFunctions
        //Get Functionas returning Byte Value.
        /// <summary>
        /// Get the External Output pin values.
        /// </summary>
        /// <returns>Byte flag indicating Output Lines in Use for external output</returns>
        byte GetExternalOutput();
        /// <summary>
        /// Get the External Input pin values.
        /// </summary>
        /// <returns>Byte flag indicating Output Lines in Use for external input</returns>
        byte GetExternalInput();
        /// <summary>
        /// Specify the value of the output pins when the auto mode evaluation returns a False condition.
        /// </summary>
        /// <returns>Byte flag indicating  Output Lines in Use for auto false output</returns>
        byte GetAutoFalseOutput();
        /// <summary>
        /// Specify the value of the output pins when the auto mode evaluation returns a True condition.
        /// </summary>
        /// <returns>Byte flag indicating  Output Lines in Use for auto true output</returns>
        byte GetAutoTrueOutput();
        /// <summary>
        /// Specify the value of the output pins while in work mode.
        /// </summary>
        /// <returns>Byte flag indicating  Output Lines in Use for auto work output</returns>
        byte GetAutoWorkOutput();
        /// <summary>
        /// Get a flag indicating which tag types to look for – may improve reader performance.
        /// </summary>
        /// <returns>Byte flag indicating tag type</returns>
        byte GetTagType();
        /// <summary>
        /// Get the Q value – may improve reader performance.
        /// </summary>
        /// <returns>Byte indicating Q value</returns>
        byte GetQValue();
        /// <summary>
        /// Read the Tag data In the users Memory section.
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="startingWordNo"></param>
        /// <param name="noOfWords"></param>
        /// <returns></returns>
        byte[] ReadRTagData(int bankId, int startingWordNo, int noOfWords);
        /// <summary>
        /// write the Data on the Tags Users Memory.
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="startingWordNo"></param>
        /// <param name="dataToWrite"></param>
        /// <returns></returns>
        bool WriteTagData(int bankId, int startingWordNo, string tagData);

        /// <summary>
        /// Get the External Output pin values.
        /// </summary>
        /// <param name="dioLine1">Byte to Get output line 0 ON/OFF</param>
        /// <param name="dioLine2">Byte to Get output line 1 ON/OFF</param>
        /// <param name="dioLine3">Byte to Get output line 2 ON/OFF</param>
        /// <param name="dioLine4">Byte to Get output line 3 ON/OFF</param>
        void GetExternalOutput(out bool dioLine1, out bool dioLine2, out bool dioLine3, out bool dioLine4);
        /// <summary>
        /// Get the External Input pin values.
        /// </summary>
        /// <param name="inline1">Byte to Get input line 0 ON/OFF</param>
        /// <param name="inline2">Byte to Get input line 1 ON/OFF</param>
        /// <param name="inline3">Byte to Get input line 2 ON/OFF</param>
        /// <param name="inline4">Byte to Get input line 3 ON/OFF</param>
        void GetExternalInput(out bool inline1, out bool inline2, out bool inline3, out bool inline4);
        /// <summary>
        /// Specify the value of the output pins when the auto mode evaluation returns a False condition.
        /// </summary>
        /// <param name="dioLine1">Byte to Get output line 0 ON/OFF</param>
        /// <param name="dioLine2">Byte to Get output line 1 ON/OFF</param>
        /// <param name="dioLine3">Byte to Get output line 2 ON/OFF</param>
        /// <param name="dioLine4">Byte to Get output line 3 ON/OFF</param>
        void GetAutoFalseOutput(out bool dioLine1, out bool dioLine2, out bool dioLine3, out bool dioLine4);
        /// <summary>
        /// Specify the value of the output pins when the auto mode evaluation returns a True cON/OFFdition.
        /// </summary>
        /// <param name="dioLine1">Byte to Get output line 0 ON/OFF</param>
        /// <param name="dioLine2">Byte to Get output line 1 ON/OFF</param>
        /// <param name="dioLine3">Byte to Get output line 2 ON/OFF</param>
        /// <param name="dioLine4">Byte to Get output line 3 ON/OFF</param>
        void GetAutoTrueOutput(out bool dioLine1, out bool dioLine2, out bool dioLine3, out bool dioLine4);
        /// <summary>
        /// Specify the value of the output pins while in work mode.
        /// </summary>
        /// <param name="dioLine1">Byte to Get output line 0 ON/OFF</param>
        /// <param name="dioLine2">Byte to Get output line 1 ON/OFF</param>
        /// <param name="dioLine3">Byte to Get output line 2 ON/OFF</param>
        /// <param name="dioLine4">Byte to Get output line 3 ON/OFF</param>
        void GetAutoWorkOutput(out bool dioLine1, out bool dioLine2, out bool dioLine3, out bool dioLine4);
        /// <summary>
        /// Get a flag indicating which tag types to look for – may improve reader performance.
        /// </summary>
        /// <param name="EPC1GEN1">Byte to Get EPC1 GEN1 tags</param>
        /// <param name="EPC1GEN2">Byte to Get EPC1 GEN2 tags</param>
        void GetTagType(out bool EPC1GEN1, out bool EPC1GEN2);
       
        //Set Functions Setting Byte Value.
        /// <summary>
        /// Set the External Output pin values.
        /// </summary>
        /// <param name="externalOutput">Byte flag indicating Output Lines in Use for external output</param>
        void SetExternalOutput(byte externalOutput);
        /// <summary>
        /// Set the External Input pin values.
        /// </summary>
        /// <param name="externalInput">Byte flag indicating Output Lines in Use for external input</param>
        void SetExternalInput(byte externalInput);
        /// <summary>
        /// Specify the value of the output pins when the auto mode evaluation returns a False condition.
        /// </summary>
        /// <param name="autoFalseOutput">Byte flag indicating Output Lines in Use for auto false output</param>
        void SetAutoFalseOutput(byte autoFalseOutput);
        /// <summary>
        /// Specify the value of the output pins when the auto mode evaluation returns a True cON/OFFdition.
        /// </summary>
        /// <param name="autoTrueOutput">Byte flag indicating Output Lines in Use for auto true output</param>
        void SetAutoTrueOutput(byte autoTrueOutput);
        /// <summary>
        /// Specify the value of the output pins while in work mode.
        /// </summary>
        /// <param name="autoWorkOutput">Byte flag indicating Output Lines in Use for auto work output</param>
        void SetAutoWorkOutput(byte autoWorkOutput);
        /// <summary>
        /// Set a flag indicating which tag types to look for – may improve reader performance.
        /// </summary>
        /// <param name="tagType">Byte flag indicating tag type to set</param>
        void SetTagType(byte tagType);
        /// <summary>
        ///  Set the External Output pin values.
        /// </summary>
        /// <param name="dioLine1">Byte to Set output line 0 ON/OFF</param>
        /// <param name="dioLine2">Byte to Set output line 1 ON/OFF</param>
        /// <param name="dioLine3">Byte to Set output line 2 ON/OFF</param>
        /// <param name="dioLine4">Byte to Set output line 3 ON/OFF</param>
        void SetExternalOutput(bool dioLine1, bool dioLine2, bool dioLine3, bool dioLine4);
        /// <summary>
        ///  Set the External Input pin values.
        /// </summary>
        /// <param name="inline1">Byte to Set input line 0 ON/OFF</param>
        /// <param name="inline2">Byte to Set input line 1 ON/OFF</param>
        /// <param name="inline3">Byte to Set input line 2 ON/OFF</param>
        /// <param name="inline4">Byte to Set input line 3 ON/OFF</param>
        
        void SetExternalInput(bool inline1, bool inline2, bool inline3, bool inline4);
        /// <summary>
        /// Specify the value of the output pins when the auto mode evaluation returns a False condition.
        /// </summary>
        /// <param name="dioLine1">Byte to Set output line 0 ON/OFF</param>
        /// <param name="dioLine2">Byte to Set output line 1 ON/OFF</param>
        /// <param name="dioLine3">Byte to Set output line 2 ON/OFF</param>
        /// <param name="dioLine4">Byte to Set output line 3 ON/OFF</param>
        void SetAutoFalseOutput(bool dioLine1, bool dioLine2, bool dioLine3, bool dioLine4);
        /// <summary>
        /// Specify the value of the output pins when the auto mode evaluation returns a True condition.
        /// </summary>
        /// <param name="dioLine1">Byte to Set output line 0 ON/OFF</param>
        /// <param name="dioLine2">Byte to Set output line 1 ON/OFF</param>
        /// <param name="dioLine3">Byte to Set output line 2 ON/OFF</param>
        /// <param name="dioLine4">Byte to Set output line 3 ON/OFF</param>
        void SetAutoTrueOutput(bool dioLine1, bool dioLine2, bool dioLine3, bool dioLine4);
        /// <summary>
        ///  Specify the value of the output pins while in work mode.
        /// </summary>
        /// <param name="dioLine1">Byte to Set output line 0 ON/OFF</param>
        /// <param name="dioLine2">Byte to Set output line 1 ON/OFF</param>
        /// <param name="dioLine3">Byte to Set output line 2 ON/OFF</param>
        /// <param name="dioLine4">Byte to Set output line 3 ON/OFF</param>
        void SetAutoWorkOutput(bool dioLine1, bool dioLine2, bool dioLine3, bool dioLine4);
        /// <summary>
        /// Set a flag indicating which tag types to look for – may improve reader performance.
        /// </summary>
        /// <param name="EPC1GEN1">Byte to Set EPC1 GEN1 tags</param>
        /// <param name="EPC1GEN2">Byte to Set EPC1 GEN2 tags</param>
        void SetTagType(bool EPC1GEN1, bool EPC1GEN2);

        /// <summary>
        /// Set a Q value for reader – may improve reader performance.
        /// </summary>
        /// <param name="qVal">Byte to Set Q Value if reader</param>
        void SetQValue(byte qVal);


        #endregion

        #region BooleanFunctions
        //Get Functionas returning Boolean Value.
        /// <summary>
        /// Switch auto mode on and off.
        /// </summary>
        /// <returns>Boolean flag indicating the auto mode status</returns>
        bool GetAutoMode();
        /// <summary>
        /// Turns on and off the additional header (and footer) lines in notification messages.
        /// </summary>
        /// <returns>Boolean flag indicating the notify header status</returns>
        bool GetNotifyHeader();
        /// <summary>
        /// Switch notify mode on and off.
        /// </summary>
        /// <returns>Boolean flag indicating the notify mode status</returns>
        bool GetNotifyMode();
        /// <summary>
        /// Specify whether to combine reads of a tag from different antennas into one TagList entry.
        /// </summary>
        /// <returns>Boolean flag indicating the taglist antenna combine status</returns>
        bool GetTagListAntennaCombine();
        /// <summary>
        /// Turn on or off inversion of the External Outputs. When inverted, setting an output high drives its voltage low.
        /// </summary>
        /// <returns>Boolean flag indicating the invert external output status</returns>
        bool GetInvertExternalOutput();
        /// <summary>
        /// Turn on or off inversion of the External Inputs. When inverted, driving an input voltage high indicates low.
        /// </summary>
        /// <returns>Boolean flag indicating the invert external input status</returns>
        bool GetInvertExternalInput();
        /// <summary>
        /// Turn DHCP on or off. If DHCP is on, the reader automatically configures itself for the network on powerup.
        /// </summary>
        /// <returns>Boolean flag indicating the dhcp status</returns>
        bool GetDHCP();
        /// <summary>
        /// Get the flag controlling network upgrades.
        /// </summary>
        /// <returns>Boolean flag indicating network upgrade status</returns>
        bool GetNetworkUpgrade();
        /// <summary>
        /// Turn I/O Streaming On and Off.
        /// </summary>
        /// <returns>Boolean flag indicating IOstream mode status</returns>
        bool GetIOStreamMode();
        /// <summary>
        /// Turn Tag Streaming On and Off.
        /// </summary>
        /// <returns>Boolean flag indicating the tag stream mode status</returns>
        bool GetTagStreamMode();
        /// <summary>
        /// Get whether to send a reader-identifying header when a Tag or I/O Stream opens a socket.
        /// </summary>
        /// <returns>Boolean flag indicating stream header status</returns>
        bool GetStreamHeader();

        //Set Functions Setting Boolean Value.

        /// <summary>
        /// Switch auto mode on and off.
        /// </summary>
        /// <param name="autoMode">Boolean flag indicating the auto mode status</param>
        void SetAutoMode(bool autoMode);
        /// <summary>
        /// Turns on and off the additional header (and footer) lines in notification messages.
        /// </summary>
        /// <param name="notifyHeader">Boolean flag indicating the notify header status</param>
        void SetNotifyHeader(bool notifyHeader);
        /// <summary>
        /// Switch notify mode on and off.
        /// </summary>
        /// <param name="notifyMode">Boolean flag indicating the notify mode status</param>
        void SetNotifyMode(bool notifyMode);
        /// <summary>
        /// Specify whether to combine reads of a tag from different antennas into one TagList entry.
        /// </summary>
        /// <param name="tagListAntennaCombine">Boolean flag indicating the taglist antenna combine status</param>
        void SetTagListAntennaCombine(bool tagListAntennaCombine);
        /// <summary>
        /// Turn on or off inversion of the External Outputs. When inverted, setting an output high drives its voltage low.
        /// </summary>
        /// <param name="invertExternalOutput">Boolean flag indicating the invert external output status</param>
        void SetInvertExternalOutput(bool invertExternalOutput);
        /// <summary>
        /// Turn on or off inversion of the External Inputs. When inverted, driving an input voltage high indicates low.
        /// </summary>
        /// <param name="invertExternalInput">Boolean flag indicating the invert external input status</param>
        void SetInvertExternalInput(bool invertExternalInput);
        /// <summary>
        /// Turn DHCP on or off. If DHCP is on, the reader automatically configures itself for the network on powerup.
        /// </summary>
        /// <param name="dhcp">Boolean flag indicating the dhcp status</param>
        void SetDHCP(bool dhcp);
        /// <summary>
        ///  Set the flag controlling network upgrades.
        /// </summary>
        /// <param name="networkUpgrade">Boolean flag indicating network upgrade status</param>
        void SetNetworkUpgrade(bool networkUpgrade);
        /// <summary>
        /// Turn I/O Streaming On and Off.
        /// </summary>
        /// <param name="ioStreamMode">Boolean flag indicating IOstream mode status</param>
        void SetIOStreamMode(bool ioStreamMode);
        /// <summary>
        /// Turn Tag Streaming On and Off.
        /// </summary>
        /// <param name="tagStreamMode">Boolean flag indicating the tag stream mode status</param>
        void SetTagStreamMode(bool tagStreamMode);
        /// <summary>
        /// Set whether to send a reader-identifying header when a Tag or I/O Stream opens a socket.
        /// </summary>
        /// <param name="streamHeader">Boolean flag indicating stream header status</param>
        void SetStreamHeader(bool streamHeader);

        #endregion BooleanFunctions

        #region IntegerFunctions
        //Get Functionas returning Integer Value.
        /// <summary>
        /// The amount of time before the reader closes a network socket after inbound communication ceases.
        /// </summary>
        /// <returns>Integer flag indicating Network Timeout in Seconds</returns>
        int GetNetworkTimeout();
        /// <summary>
        /// Get the network port # for the reader's command channel.
        /// </summary>
        /// <returns>Integer flag indicating network port for readers command channel</returns>
        int GetCommandPort();
        /// <summary>
        /// Get the time to wait after receiving a start trigger before starting AutoMode
        /// </summary>
        /// <returns>Integer flag indicating time to wait after receiving a start trigger</returns>
        int GetAutoStartPause();
        /// <summary>
        /// Get the time to wait after receiving a stop trigger before stopping the AutoAction.
        /// </summary>
        /// <returns>Integer flag indicating time to wait after receiving a stop trigger </returns>
        int GetAutoStopPause();
        /// <summary>
        /// Get the pause time after the auto mode evaluation returns a True condition
        /// </summary>
        /// <returns>Integer flag indicating pause time after the auto mode evaluation returns a True condition</returns>
        int GetAutoTruePause();
        /// <summary>
        /// Get the pause time after the auto mode evaluation returns a False condition.
        /// </summary>
        /// <returns>Integer flag indicating pause time after the auto mode evaluation returns a False condition.</returns>
        int GetAutoFalsePause();
        /// <summary>
        /// Get the amount of time the reader keeps it's notification TCP socket open without communication activity.
        /// </summary>
        /// <returns>Integer flag indicating amount of time the reader keeps it's notification TCP socket open</returns>
        int GetNotifyKeepAliveTime();
        /// <summary>
        /// Get the persistence time for tags in the TagList.
        /// </summary>
        /// <returns>Integer flag indicating persistence time for tags in the TagList.</returns>
        int GetPersistTime();
        /// <summary>
        /// Get the time interval for automatically pushing TagLists.
        /// </summary>
        /// <returns>Integer flag indicating time interval for automatically pushing TagLists.</returns>
        int GetNotifyTime();
        /// <summary>
        /// Get the amount of digital attenuation to apply to the emitted RF.
        /// </summary>
        /// <returns>Integer flag indicating amount of digital attenuation</returns>
        int GetRFAttenuation();
        /// <summary>
        /// Get the timer that will move the auto mode state from work mode to evaluate mode.
        /// </summary>
        /// <returns>Integer flag indicating timer that will move the auto mode state </returns>
        int GetAutoStopTimer();
        /// <summary>
        /// Returns the maximum addressible antenna port number (total number of antennas is MaxAntenna+1)
        /// </summary>
        /// <returns>Integer flag indicating maximum addressible antenna port number </returns>
        int GetMaxAntenna();
        /// <summary>
        /// Gives the AntennaId which is set for the Write Tag opration.
        /// </summary>
        /// <returns></returns>
        int GetProgramAntenna();
                       
        //Set Functions Setting Integer Value.
        /// <summary>
        /// The amount of time before the reader closes a network socket after inbound communication ceases.
        /// </summary>
        /// <param name="networkTimeout">Integer value(in Seconds) Setting amount of time before the reader closes a network socket</param>
        void SetNetworkTimeout(int networkTimeout);
        /// <summary>
        /// Get and Set the network port # for the reader's command channel.
        /// </summary>
        /// <param name="commandPort">Integer setting network port for readers command channel</param>
        void SetCommandPort(int commandPort);
        /// <summary>
        /// Set the time to wait after receiving a start trigger before starting AutoMode
        /// </summary>
        /// <param name="autoStartPause">Integer setting time to wait after receiving a start trigger</param>
        void SetAutoStartPause(int autoStartPause);
        /// <summary>
        /// Set the time to wait after receiving a stop trigger before stopping the AutoAction.
        /// </summary>
        /// <param name="autoStopPause">Integer setting time to wait after receiving a stop trigger</param>
        void SetAutoStopPause(int autoStopPause);
        /// <summary>
        /// Set the pause time after the auto mode evaluation returns a True condition
        /// </summary>
        /// <param name="autoTruePause">Integer setting pause time after the auto mode evaluation returns a True condition</param>
        void SetAutoTruePause(int autoTruePause);
        /// <summary>
        /// Set the pause time after the auto mode evaluation returns a False condition.
        /// </summary>
        /// <param name="autoFalsePause">Integer setting pause time after the auto mode evaluation returns a False condition.</param>
        void SetAutoFalsePause(int autoFalsePause);
        /// <summary>
        /// Set the amount of time the reader keeps it's notification TCP socket open without communication activity.
        /// </summary>
        /// <param name="notifyKeepAliveTime">Integer setting amount of time the reader keeps it's notification TCP socket open</param>
        void SetNotifyKeepAliveTime(int notifyKeepAliveTime);
        /// <summary>
        ///  Set the persistence time for tags in the TagList.
        /// </summary>
        /// <param name="persistTime">Integer setting persistence time for tags in the TagList.</param>
        void SetPersistTime(int persistTime);
        /// <summary>
        /// Set the time interval for automatically pushing TagLists.
        /// </summary>
        /// <param name="notifyTime">Integer setting time interval for automatically pushing TagLists.</param>
        void SetNotifyTime(int notifyTime);
        /// <summary>
        /// Set the amount of digital attenuation to apply to the emitted RF.
        /// </summary>
        /// <param name="rfAttenuation">Integer setting amount of digital attenuation</param>
        void SetRFAttenuation(int rfAttenuation);
        /// <summary>
        /// Set the timer that will move the auto mode state from work mode to evaluate mode.
        /// </summary>
        /// <param name="autoStopTimer">Integer setting timer that will move the auto mode state </param>
        void SetAutoStopTimer(int autoStopTimer);
        
        /// <summary>
        /// Sets the Antenna for Tag write opration.Onlt this antenna can re-write the Tag Id .
        /// </summary>
        /// <param name="antennaId"></param>
        void SetProgramAntenna(int antennaId);
        #endregion IntegerFunctions

        #region StringFunctions
        //Get Functions
        /// <summary>
        /// Get the antenna port sequence the reader should use.
        /// </summary>
        /// <returns>String flag indicating the antenna sequence reader should use</returns>
        string GetAntennaSequence();
        /// <summary>
        /// Get the current tag mask as an array of bytes.ALR-9800 also accepts "AcqMask".
        /// </summary>
        /// <returns>String flag indicating the mask for reader to reader specific tags</returns>
        string GetMask();
        /// <summary>
        /// Get whether notifications should include Tag data, I/O event data, or both.
        /// </summary>
        /// <returns>String flag indicating whether notifications should include Tag data, I/O event data, or both</returns>
        string GetNotifyInclude();
        /// <summary>
        /// Get the Reader Version after Time specified in Network Timeout
        /// </summary>
        /// <returns>string indication Reader version</returns>
        string GetReaderVersion();
        /// <summary>
        /// Get the Notify Address.
        /// </summary>
        /// <returns>string indicating Reader's Notify Address</returns>
        string GetNotifyAddress();

        /// <summary>
        /// Get the Notify Trigger.
        /// </summary>
        /// <returns>string indicating Reader's Notify Trigger</returns>
        string GetNotifyTrigger();
        /// <summary>
        /// returns the time set on the reader.
        /// </summary>
        /// <returns></returns>
        string GetTime();
        //Set
        /// <summary>
        /// Set the antenna port sequence the reader should use.
        /// </summary>
        /// <param name="antennaSequence">String parameter to set antenna sequence reader should use</param>
        void SetAntennaSequence(string antennaSequence);
        /// <summary>
        /// Set the current tag mask as an array of bytes.ALR-9800 also accepts "AcqMask".
        /// </summary>
        /// <param name="mask">String parameter to set mask for reader to read specific tags</param>
        void SetMask(string mask);
        /// <summary>
        /// Set whether notifications should include Tag data, I/O event data, or both.
        /// </summary>
        /// <param name="notifyInclude">String parameter to set notifications(Tag data, I/O event data, or both)</param>
        void SetNotifyInclude(string notifyInclude);

        /// <summary>
        /// Set the Notify Trigger.
        /// </summary>
        void SetNotifyTrigger(string notifyTrigger);

        /// <summary>
        /// Sets the timestamp on the reader
        /// </summary>
        void SetTime();

        /// <summary>
        /// Writes EPC Tag Id
        /// </summary>
        /// <param name="hexTagId"></param>
        void ProgramTag(string hexTagId);

        /// <summary>
        /// Kills the tag
        /// </summary>
        /// <param name="hexTagId">Tag id in hex format e.g. 31142BBB9C2FAF0804000000</param>
        /// ///<param name="hexKillPwd">Kill password in hex format e.g. 11223344 </param>
        void KillTag(string hexTagId, string hexKillPwd);

        /// <summary>
        /// Sets KillPwd for Class1Gen2 tags.It will be used by ProgramKillPwd command if we don't
        /// specify any killPwd in that command
        /// </summary>
        /// <param name="hexKillPassword">Kill password in hex format e.g. 11223344</param>
        void ProgG2KillPwd(string hexKillPassword);

        /// <summary>
        /// Sets killPwd for a tag. If killPwd is empty, it will use killPwd set by ProgG2KillPwd.
        /// </summary>
        /// <param name="hexKillPassword">Kill password in hex format e.g. 11223344</param>
        void ProgramKillPwd(string hexKillPassword);
        #endregion StringFunctions
        
        #region AutoModeReset
        /// <summary>
        /// Auto Mode Reset Command.
        /// </summary>
        void AutoModeReset();
        #endregion AutoModeReset

        #region Reboot

        /// <summary>
        /// Reboots the Reader.
        /// </summary>
        void RebootAlienReader();
        
        #endregion Reboot

        #region Save
        /// <summary>
        /// Saves Settings of Reader.
        /// </summary>
        void SaveReaderSettings();
        #endregion Save

        #region TagCommand
        /// <summary>
        /// Get the Tags List
        /// </summary>
        /// <returns></returns>
        IRFIDTag[] GetTagList();
        #endregion TagCommand
    }
}
