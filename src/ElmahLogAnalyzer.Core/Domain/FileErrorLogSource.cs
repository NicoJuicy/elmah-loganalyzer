﻿using System;
using System.Collections.Generic;
using System.Linq;
using ElmahLogAnalyzer.Core.Infrastructure.FileSystem;
using ElmahLogAnalyzer.Core.Infrastructure.Logging;
using ElmahLogAnalyzer.Core.Infrastructure.Settings;

namespace ElmahLogAnalyzer.Core.Domain
{
	public class FileErrorLogSource : IErrorLogSource
	{
		private const string FileFilterPattern = "error-*.xml";

		private readonly IFileSystemHelper _fileSystemHelper;
		private readonly IErrorLogFileParser _parser;
		private readonly ISettingsManager _settingsManager;
		private readonly ILog _log;

		public FileErrorLogSource(string connection, IFileSystemHelper fileSystemHelper, IErrorLogFileParser parser, ISettingsManager settingsManager, ILog log)
		{
			Connection = connection;
			_fileSystemHelper = fileSystemHelper;
			_parser = parser;
			_settingsManager = settingsManager;
			_log = log;
		}

		public string Connection { get; private set; }
		
		public List<ErrorLog> GetLogs()
		{
			if (!_fileSystemHelper.DirectoryExists(Connection))
			{
				_log.Error(string.Format("Connection: {0} does not exist", Connection));
				throw new ApplicationException(string.Format("The directory: {0} was not found", Connection));
			}

			var files = GetErrorLogFilesFromDirectory(Connection);
			return ParseFiles(files);
		}

		private IEnumerable<string> GetErrorLogFilesFromDirectory(string directory)
		{
			var files = _fileSystemHelper.GetFilesFromDirectory(directory, FileFilterPattern);

			if (_settingsManager.ShouldGetAllLogs)
			{
				return files;
			}

			return files.OrderByDescending(x => x).Take(_settingsManager.GetMaxNumberOfLogs());
		}

		private string GetContentFor(string file)
		{
			return _fileSystemHelper.GetFileContent(file);
		}

		private List<ErrorLog> ParseFiles(IEnumerable<string> files)
		{
			var result = new List<ErrorLog>();
			foreach (var file in files)
			{
				_log.Debug(string.Format("Parsing file: {0}", file));

				var content = GetContentFor(file);
				var errorLog = _parser.Parse(content);

				if (errorLog == null)
				{
					_log.Error(string.Format("Failed to parse file: {0}", file));
					continue;
				}

				result.Add(errorLog);
			}

			return result;
		}
	}
}
