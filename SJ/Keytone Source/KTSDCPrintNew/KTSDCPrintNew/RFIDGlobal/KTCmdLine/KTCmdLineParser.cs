///////////////////////////////////////////////////////////////////////////////////////////
namespace KTone.RFIDGlobal.KTCMDLine
{
	using System;
	using System.Collections;
    using System.Runtime.Serialization;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using NLog;
    using System.Xml; 


	public class CmdLineParserBase 
	{

	
		private bool continueOnError = false ; 

		
		private string[] optionPrefix = {"/", "-"}; 

        
		
		private IDictionary availableOptions = new Hashtable(); 

	
		private IDictionary userSelectedOptions = new Hashtable(); 

		
		private ArrayList arguments = new ArrayList();
 
	
		private char[] optionSeparator = new char[]{':','='};

		public bool ContinueOnError
		{
			get
			{
				return this.continueOnError;
			}
			set
			{
				this.continueOnError = value;
			}
		}

       
		public string[] OptionPrefix
		{
			get
			{
				return this.optionPrefix;
			}
			set
			{
				// Validate value
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}

				this.optionPrefix = value;
                
				// Sort prefixes so longest prefix is used
				Array.Sort(this.optionPrefix);
				Array.Reverse(this.optionPrefix);
			}
		}

	
		public ICollection AvailableOptions
		{
			get
			{
				return availableOptions.Values;
			}
		}

		public IDictionary UserSelectedOptions
		{
			get
			{
				return userSelectedOptions;
			}
		}

		public char[] OptionSeparator
		{
			get 
			{
				return optionSeparator;
			}
			set 
			{
				// No validation required
				optionSeparator = value;
			}
		}

		
		public string[] Arguments
		{
			get
			{
				return (string[])arguments.ToArray(typeof(string));
			}
		}

	
		public void AddOption(CmdLineOption sw) 
		{
			// Validate arguments
			if (sw == null)
			{
				throw new ArgumentNullException("sw");
			}

			availableOptions.Add(sw.Name, sw);
		}

		
		public void AddOptions(params string[] Options) 
		{
			// Validate parameters
			if (Options == null)
			{
				throw new ArgumentNullException("sw");
			}

			// Add Options
			foreach (string sw in Options)
			{
				this.AddOption(new CmdLineOption(sw));
			}
		}

		
		public  CmdLineParserBase() 
		{
			// Nothing to do
		}


		public  CmdLineParserBase(bool continueOnError ) 
		{
			this.continueOnError = continueOnError ; 
		}
	
	
		public  CmdLineParserBase(ICollection Options) 
		{
			// Validate parameters
			if (Options == null)
			{
				throw new ArgumentNullException("Options");
			}
        
			// Add Options
			foreach(CmdLineOption sw in Options)
			{
				this.AddOption(sw);
			}
		}

		
		public CmdLineParserBase(ICollection Options, bool continueOnError ) 
		{
			// Validate parameters
			if (Options == null)
			{
				throw new ArgumentNullException("Options");
			}

			// Add Options
			foreach(CmdLineOption sw in Options)
			{
				this.AddOption(sw);
			}

			this.continueOnError = continueOnError;
		}

		
        public virtual void Parse(string[] args) 
		{
			// Validate arguments
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}

			// Local variables
			CmdLineOption sw = null;
			string argument = null;
			string prefix = null;

			for (int curr = 0; curr < args.Length; ++curr)
			{
				argument = args[curr];
				
				prefix = this.FindPrefix(argument);
				if (prefix != null)
				{
					
					argument = argument.Remove(0,prefix.Length);
			

				
					sw = this.GetOption(argument);                    
                    
				
					if (sw != null)
					{
					
						if(argument.Length == sw.Name.Length)
						{
							argument = "";
						}
						else
						{
							argument = argument.Remove(0, sw.Name.Length+1);
						}
                    
						try
						{
							sw.Validate(argument);
						}
						catch (MismatchedOptionException e)
						{
							if (this.continueOnError == false )
							{
								throw e;
							}
						}
                        
					
						if (this.userSelectedOptions[sw.Name] != null )                           
						{
							throw new MismatchedOptionException
								("Multiple option found", sw.Name);
						}
                        
						this.userSelectedOptions[sw.Name] = sw;
					}
					
					else if (this.continueOnError == false )
					{
						throw new MalformedOptionException
							("Attempted to parse invalid option", argument);
					}
				}
				
				else
				{
					
					this.arguments.Add(argument);
				}    
			}
		
		}

		public void RemoveOption(String sw)
		{
			if(!this.availableOptions.Contains(sw))
			{
				throw new ArgumentException(sw + " is not a valid option", "sw");
			}
			this.availableOptions.Remove(sw);
		}

		
		private bool IsOption(string arg) 
		{
			// Check each prefix to see in arg starts with it
			return (this.FindPrefix(arg) != null);
		}

		public CmdLineOption LookupOption(string name ) 
		{
			if ( userSelectedOptions.Contains(name ) ) 
				return (CmdLineOption) userSelectedOptions[name] ; 
			else 
				return null ; 
		}
		
		private string FindPrefix(string arg)
		{
			// Look through Options
			foreach (string prefix in this.optionPrefix)
			{
				// Find the prefix that argument starts with
				if (arg.StartsWith(prefix))
				{
					return prefix;
				}
			}

			// No prefix
			return null;
		}

	
		private CmdLineOption GetOption(string arg) 
		{
			// local variables
			CmdLineOption sw;
			string swText;
			string swLower;
			int iDivider = arg.IndexOfAny(this.optionSeparator);

			// Get option
			if(iDivider < 0)
			{
			
				swText = arg.Clone() as string;
			}
			else
			{   
			
				swText = arg.Substring(0, iDivider);
			}

			swLower = swText.ToLower();

			foreach (string name in this.availableOptions.Keys)
			{
				sw = (CmdLineOption)availableOptions[name];

				if ((!sw.IgnoreCase && swText.Equals(sw.Name) ||
					(sw.IgnoreCase && swLower.Equals(sw.Name.ToLower()))))
				{
					return sw;
				}
			}

			return null;
		}

	}

	public class KTCmdLineParser : CmdLineParserBase
	{
        static Hashtable s_levels = new Hashtable(); 
        private static string s_LogFileValPart1 = @"<?xml version=""1.0"" ?>
                            <nlog xmlns=""http://www.nlog-project.org/schemas/NLog.xsd""
                                  xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""  autoReload=""true"" >

	                            <targets async=""true"">

		                            <target name=""file"" xsi:type=""File""
				                            layout=""[${threadid}][${date}][${callsite:fileName=true:methodName=false:className=false}][${logger}:${stacktrace:topFrames=1}()] ${level}:${message}:${exception:format=ToString,ShortType,StackTrace,Method,Message,Type:seperator=\r\n}""
				                            fileName=""${basedir}/logs/"; 
        private static string s_LogFileValPart2 = @".txt""
				                            archiveFileName=""${basedir}/logs/" ; 
        private static string s_LogFileValPart3 =
                                        @".{#####}.txt""
				                            archiveAboveSize=""10000000""
				                            archiveNumbering=""Sequence""
				                            concurrentWrites=""true""
				                            keepFileOpen=""true""
				                            maxArchiveFiles=""20""
				                            encoding=""iso-8859-2"" />
		                        </targets>
	                            <rules>
		                            <logger name=""*"" minlevel=""" ;
        private static string s_LogFileValPart4 =  @""" writeTo=""file"" />
	                            </rules>
                            </nlog>"; 
        static  KTCmdLineParser()
        {
            s_levels.Add(NLog.LogLevel.Debug.UppercaseName, NLog.LogLevel.Debug);
            s_levels.Add(NLog.LogLevel.Trace.UppercaseName, NLog.LogLevel.Trace);
            s_levels.Add(NLog.LogLevel.Error.UppercaseName, NLog.LogLevel.Error);
            s_levels.Add(NLog.LogLevel.Info.UppercaseName, NLog.LogLevel.Info);
            s_levels.Add(NLog.LogLevel.Fatal.UppercaseName, NLog.LogLevel.Fatal);
            s_levels.Add(NLog.LogLevel.Warn.UppercaseName, NLog.LogLevel.Warn);
        }
		public KTCmdLineParser(): base()
		{
			InitCtor() ; 
		}
		public KTCmdLineParser(bool continueOnError ) : base ( continueOnError) 
		{
			InitCtor() ; 
		}

		private void InitCtor()
		{
			NoArgCmdLineOption swFatal = new NoArgCmdLineOption(NLog.LogLevel.Fatal.UppercaseName, "Turn  logging On to Fatal Level Only.") ;
            this.AddOption(swFatal);
            NoArgCmdLineOption swError = new NoArgCmdLineOption(NLog.LogLevel.Error.UppercaseName, "Turn logging On to Error and above Level.");
            this.AddOption(swError);
            NoArgCmdLineOption swWarn = new NoArgCmdLineOption(NLog.LogLevel.Warn.UppercaseName, "Turn logging On to Warn and above Level.");
            this.AddOption(swWarn);
            NoArgCmdLineOption swInfo = new NoArgCmdLineOption(NLog.LogLevel.Info.UppercaseName, "Turn logging On to Info and above Level.");
            this.AddOption(swInfo);
            NoArgCmdLineOption swDebug = new NoArgCmdLineOption(NLog.LogLevel.Debug.UppercaseName, "Turn logging On to Debug and above Level.");
            this.AddOption(swDebug);
            NoArgCmdLineOption swTrace = new NoArgCmdLineOption(NLog.LogLevel.Trace.UppercaseName, "Turn logging On to Trace and above Level.");
            this.AddOption(swTrace); 

			SingleArgCmdLineOption sw3 = new	SingleArgCmdLineOption("Level", "Level of the logging (Trace, Debug, warn, info, error, fatal)") ; 
			
			this.AddOption(sw3) ;
           
		}
        
        public override void Parse(string[] args)
        {
            base.Parse(args);

            #region Level
            NLog.LogLevel loggingLevel = NLog.LogLevel.Info; 
            SingleArgCmdLineOption sw1 = (SingleArgCmdLineOption)LookupOption("Level") ; 
            if (  sw1 != null ) 
            {
                if (s_levels[(string)sw1.Field.ToUpper()] != null)
                    loggingLevel =(NLog.LogLevel) s_levels[(string)sw1.Field.ToUpper()];   
            }
            NoArgCmdLineOption sw2; 
            if ( (sw2 =  (NoArgCmdLineOption)LookupOption(NLog.LogLevel.Fatal.UppercaseName)) != null )    
                loggingLevel = NLog.LogLevel.Fatal;
            if ((sw2 = (NoArgCmdLineOption)LookupOption(NLog.LogLevel.Error.UppercaseName)) != null)
                loggingLevel = NLog.LogLevel.Error;
            if ((sw2 = (NoArgCmdLineOption)LookupOption(NLog.LogLevel.Warn.UppercaseName)) != null)
                loggingLevel = NLog.LogLevel.Warn;
            if ((sw2 = (NoArgCmdLineOption)LookupOption(NLog.LogLevel.Info.UppercaseName)) != null)
                loggingLevel = NLog.LogLevel.Info;
            if ((sw2 = (NoArgCmdLineOption)LookupOption(NLog.LogLevel.Debug.UppercaseName)) != null)
                loggingLevel = NLog.LogLevel.Debug;
            if ((sw2 = (NoArgCmdLineOption)LookupOption(NLog.LogLevel.Trace.UppercaseName)) != null)
                loggingLevel = NLog.LogLevel.Trace;
            #endregion Level

            #region FileName
            string nlogFileName = string.Empty;
            SingleArgCmdLineOption swNLogFileName = (SingleArgCmdLineOption)LookupOption("NLogFileName");
            if (swNLogFileName != null)
                nlogFileName = (string)swNLogFileName.Field.ToUpper();
            #endregion FileName

            
            #region Size
            string archiveAboveSize = string.Empty;
            SingleArgCmdLineOption swSize = (SingleArgCmdLineOption)LookupOption("archiveAboveSize");
            if (swSize != null)
                archiveAboveSize = (string)swSize.Field.ToUpper();

            #endregion Size

            #region maxArchiveFiles
            string maxArchiveFiles = string.Empty;
            SingleArgCmdLineOption swMaxArchiveFiles = (SingleArgCmdLineOption)LookupOption("maxArchiveFiles");
            if (swMaxArchiveFiles != null)
                maxArchiveFiles = (string)swMaxArchiveFiles.Field.ToUpper();

            #endregion maxArchiveFiles

            CreateNLog(loggingLevel, nlogFileName, archiveAboveSize, maxArchiveFiles); // Create file with 
        }

        private void CreateNLog(NLog.LogLevel defaultLevel, string nlogFileName, string archiveAboveSize,
            string maxArchiveFiles)
        {
            Process prCurrent = Process.GetCurrentProcess();

            string processName = Process.GetCurrentProcess().ProcessName;
            string processNameWithoutExe = processName;
            if (!processName.EndsWith(".exe", true, null))
                processName += ".exe";

            if (nlogFileName == string.Empty)
                nlogFileName = processName + ".Nlog";
            if (!File.Exists(nlogFileName))
            {
                FileInfo fi = null;
                StreamWriter sw = null;
                try
                {
                    fi = new FileInfo(nlogFileName);
                    sw = fi.CreateText();
                    sw.Write(s_LogFileValPart1); sw.Write(processNameWithoutExe + @"/" + processNameWithoutExe);
                    sw.Write(s_LogFileValPart2); sw.Write(processNameWithoutExe + @"/" + processNameWithoutExe);
                    sw.Write(s_LogFileValPart3); sw.Write(defaultLevel.Name);
                    sw.Write(s_LogFileValPart4);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (sw != null)
                        sw.Close();
                    fi = null;
                    sw = null;
                }
            }


            bool modified = false;
            XmlDocument doc = new XmlDocument();
            XmlNodeList nodeList = null;
            XmlNode root = null;
            XmlNamespaceManager nsmgr = null;
            try
            {
                // Update the logger node and change the log level 
                doc.Load(nlogFileName);
                nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("foo", "http://www.nlog-project.org/schemas/NLog.xsd");
                root = doc.DocumentElement;
            }
            catch (Exception) { }

            #region Level
            try
            {
                nodeList = root.SelectNodes("/foo:nlog/foo:rules/foo:logger", nsmgr);

                // Change the price on the books.
                foreach (XmlNode node in nodeList)
                {
                    XmlAttribute at = node.Attributes["minlevel"];
                    at.Value = defaultLevel.Name;
                    modified = true;
                }
            }
            catch (Exception) { }
            #endregion Level

            #region archiveAboveSize
            if (archiveAboveSize != string.Empty)
            {
                try
                {
                    // Update the logger node and change the log level 
                    root = doc.DocumentElement;
                    nodeList = root.SelectNodes("/foo:nlog/foo:targets/foo:target", nsmgr);


                    foreach (XmlNode node in nodeList)
                    {
                        XmlAttribute at = node.Attributes["archiveAboveSize"];
                        at.Value = archiveAboveSize;
                        modified = true;
                    }
                }
                catch (Exception) { }
            }
            #endregion archiveAboveSize

            #region maxArchiveFiles
            if (maxArchiveFiles != string.Empty)
            {
                try
                {
                    // Update the logger node and change the log level 
                    root = doc.DocumentElement;
                    nodeList = root.SelectNodes("/foo:nlog/foo:targets/foo:target", nsmgr);


                    foreach (XmlNode node in nodeList)
                    {
                        XmlAttribute at = node.Attributes["maxArchiveFiles"];
                        at.Value = maxArchiveFiles;
                        modified = true;
                    }
                }
                catch (Exception) { }
            }
            #endregion maxArchiveFiles

            try
            {
                if (modified)
                    doc.Save(nlogFileName);
            }
            catch (Exception)
            {
            }
        }
	}
	
	// The CmdLIneOption Class 
	    public enum ARGTYPE 
    {
        /// <summary>
        /// For Options the require zero parameter
        /// </summary>
        ZERO_ARG = 1,
        
        /// <summary>
        /// For Options the require one  parameter
        /// </summary>
        SINGLE_ARG = 2,
        
        /// <summary>
        /// For Options the require variable # of arguments ( zero or more ) 
        /// </summary>
        MULTIPLE_ARG = 3
    }

    /// <summary>
    /// Represents a command line option
    /// </summary>
    public class CmdLineOption
    {

		/// <summary>
		/// Maximum # of arguments in a option 
		/// </summary>
		public static int  MAX_ARGCOUNT = 20 ; 

		private char optionDivider = ',';

        /// <summary>
        /// Ignore the case the Options or not.
        /// </summary>
        private bool ignoreCase = true; 

        /// <summary>
        /// How many arguments option can have, zero or more by default
        /// </summary>
        protected  ARGTYPE argType = ARGTYPE.ZERO_ARG;

        /// <summary>
        /// Represents the option key
        /// </summary>
        private string name = null;

        /// <summary>
        /// Represents the ARGCOUNT for the option
        /// </summary>
        private int  argCount  = 0; 

        /// <summary>
        /// Represents the description of the option
        /// </summary>
        private string description = ""; 

        /// <summary>
        /// Represents the arguments of the option
        /// </summary>
        protected string[]  result = null; 

        

        /// <summary>
        /// get the option key
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// How many arguments option can have
        /// </summary>
        public int MaxArgument 
        {
            get 
            {
				if ( argType == ARGTYPE.ZERO_ARG ) 
					return 0 ; 
				else if ( argType == ARGTYPE.SINGLE_ARG ) 
					return 1 ; 
				else 
					return MAX_ARGCOUNT ; 
               
            }
 
        }

		/// <summary>
		/// delimiter between option and its arguments
		/// </summary>
		public char OptionDivider
		{
			get 
			{
				return optionDivider;
			}
			set 
			{
				// No validation required
				optionDivider = value;
			}
		}

        

        /// <summary>
        /// get the description of a option
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
        }

        /// <summary>
        /// get or set the ignoreCase field
        /// </summary>
        public bool IgnoreCase
        {
            get
            {
                return ignoreCase;
            }
            set
            {
                // No validation required
                ignoreCase = value;
            }
        }

  

		/// <summary>
		/// create a option, with option key, prompt ,description and validator
		/// </summary>
		/// <param name="name">the option key</param>
		/// <param name="prompt">the option prompty</param>
		/// <param name="description">the option description</param>
		/// <exception cref="ArgumentNullException">
		/// if <c>name</c> or <c>prompt</c> or <c>description</c> is null.
		/// </exception>
		public  CmdLineOption(string name)
		{
			// Validate arguments
			if(name == null)
			{
				throw new ArgumentNullException("name");
			}

			// Set values
			this.name = name; 
         
		}
        /// <summary>
        /// create a option, with option key, prompt ,description and validator
        /// </summary>
        /// <param name="name">the option key</param>
        /// <param name="prompt">the option prompty</param>
        /// <param name="description">the option description</param>
        /// <exception cref="ArgumentNullException">
        /// if <c>name</c> or <c>prompt</c> or <c>description</c> is null.
        /// </exception>
        public  CmdLineOption(string name,  string description)
        {
            // Validate arguments
            if(name == null)
            {
                throw new ArgumentNullException("name");
            }
            if(description == null)
            {
                throw new ArgumentNullException("description");
            }

            // Set values
            this.name = name; 
            this.description = description;
         
        }

        

      
        public void Validate(string arg)
        {            
            this.result = this.Parse(arg);

            // Validate length
			if ( 
					(( argType == ARGTYPE.ZERO_ARG ) && ( this.result != null ) ) 
					|| 
					(( argType == ARGTYPE.SINGLE_ARG ) && ( this.result.Length != 1 ) )
					||
					(( argType == ARGTYPE.MULTIPLE_ARG ) && ( this.result.Length > MAX_ARGCOUNT ) ) 
				)
            {
                throw new MalformedOptionException(
                    "Malformed  arguments Expected # of Fields = " + this.argCount );
            }           
        }

      
        public string[] Parse(string arg)
        {
			if (arg == null || arg.Length == 0)
			{
				return null;
			}

			// Local variables
			string[] ret;

			ret = arg.Split(optionDivider);

			if(ret == null)
			{
				ret = new string[1];
				ret[0] = arg;
			}

			return ret;
		
           
        }

    }

	public class NoArgCmdLineOption : CmdLineOption
	{
		public NoArgCmdLineOption(string name ) 
			: base(name) 
		{
			argType  = ARGTYPE.ZERO_ARG ; 

		}
		public NoArgCmdLineOption(string name,string desc ) 
			: base(name, desc) 
		{
			argType  = ARGTYPE.ZERO_ARG ; 

		}

	}
	public class SingleArgCmdLineOption : CmdLineOption
	{
		public SingleArgCmdLineOption(string name ) 
			: base(name) 
		{
			argType  = ARGTYPE.SINGLE_ARG ; 

		}
		public SingleArgCmdLineOption(string name,string desc ) 
			: base(name, desc) 
		{
			argType  = ARGTYPE.SINGLE_ARG ; 

		}
		/// <summary>
		/// get the result of a option argument
		/// </summary>
		public string Field
		{
			get
			{
				return result[0];
			}
		}
	}

	public class MultiArgCmdLineOption : CmdLineOption
	{
		public MultiArgCmdLineOption(string name ) 
			: base(name) 
		{
			argType = ARGTYPE.MULTIPLE_ARG ; 

		}
		public MultiArgCmdLineOption(string name,string desc ) 
			: base(name, desc) 
		{
			argType = ARGTYPE.MULTIPLE_ARG ; 

		}
		/// <summary>
		/// get the result of a option argument
		/// </summary>
		public string[] Fields
		{
			get
			{
				return result;
			}
		}
	}
	
	public class UnknownOptionException : ArgumentException
	{
		
		public UnknownOptionException():base(){}

		public UnknownOptionException(string message):base(message){}
		
		protected UnknownOptionException
			(SerializationInfo info, StreamingContext context)
			:base(info, context){}
	
		public UnknownOptionException(string message, Exception innerException)
			:base(message, innerException){}
		
		public UnknownOptionException(string message, string paramName)
			:base(message, paramName){}

		public UnknownOptionException
			(string message, string paramName, Exception innerException)
			:base(message, paramName, innerException){}
	}
    
		   
	public class MalformedOptionException : ArgumentException
	{
		
		public MalformedOptionException():base(){}

		public MalformedOptionException(string message):base(message){}
		
		protected MalformedOptionException
			(SerializationInfo info, StreamingContext context)
			:base(info, context){}
	
		public MalformedOptionException(string message, Exception innerException)
			:base(message, innerException){}
		
		public MalformedOptionException(string message, string paramName)
			:base(message, paramName){}

		public MalformedOptionException
			(string message, string paramName, Exception innerException)
			:base(message, paramName, innerException){}
	}

	public class MismatchedOptionException : ArgumentException
	{
		
		public MismatchedOptionException():base(){}

		public MismatchedOptionException(string message):base(message){}
		
		protected MismatchedOptionException
			(SerializationInfo info, StreamingContext context)
			:base(info, context){}
	
		public MismatchedOptionException(string message, Exception innerException)
			:base(message, innerException){}
		
		public MismatchedOptionException(string message, string paramName)
			:base(message, paramName){}

		public MismatchedOptionException
			(string message, string paramName, Exception innerException)
			:base(message, paramName, innerException){}
	}
}