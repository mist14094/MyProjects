using System;

namespace KTone.RFIDGlobal.ImportData
{
	
	/// <summary>
	/// This exception is used when UploadBlock() called before StartUpLoad() is called.
	/// </summary>
	public class NoStartUploadException : System.ApplicationException 
	{
		public NoStartUploadException() : base()
	{
		//
		// TODO: Add constructor logic here
		//
	}


		public NoStartUploadException(string message) : base(message)
	{
	}


		public NoStartUploadException(string message, Exception innerException)
			:base (message,innerException )
	{
	}
	}
	/// <summary>
	/// This exception is used when Directory does not exist.
	/// </summary>
	public class DirectoryDoesNotExistException : System.ApplicationException 
	{
		public DirectoryDoesNotExistException() : base()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public DirectoryDoesNotExistException(string message) : base(message)
		{
		}


		public DirectoryDoesNotExistException(string message, Exception innerException)
			:base (message,innerException )
		{
		}
	}
	/// <summary>
	/// This exception is used when UploadBlock() called before StartUpLoad() is called.
	/// </summary>
	public class FileUploadSessionExpireException : System.ApplicationException 
	{
		public FileUploadSessionExpireException() : base()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public FileUploadSessionExpireException(string message) : base(message)
		{
		}


		public FileUploadSessionExpireException(string message, Exception innerException)
			:base (message,innerException )
		{
		}
	}
	/// <summary>
	/// This exception is used when Block Size is more than Specified block size
	/// </summary>
	public class BlockSizeTooLargeException : System.ApplicationException 
	{
		public BlockSizeTooLargeException() : base()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public BlockSizeTooLargeException(string message) : base(message)
		{
		}


		public BlockSizeTooLargeException(string message, Exception innerException)
			:base (message,innerException )
		{
		}
	}
	/// <summary>
    /// This exception is used for errors occurred while reading Logs file
	/// </summary>
	public class LogReadException : System.ApplicationException 
	{
		public LogReadException() : base()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public LogReadException(string message) : base(message)
		{
		}


		public LogReadException(string message, Exception innerException)
			:base (message,innerException )
		{
		}
	}
}
