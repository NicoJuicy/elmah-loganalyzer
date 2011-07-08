﻿using System;
using System.IO;
using ElmahLogAnalyzer.Core.Domain;
using ElmahLogAnalyzer.Core.Infrastructure.FileSystem;
using ElmahLogAnalyzer.Core.Infrastructure.Settings;
using ElmahLogAnalyzer.TestHelpers.Fakes;

namespace ElmahLogAnalyzer.IntegrationTests
{
	public abstract class IntegrationTestBase
	{
		protected string TestFilesDirectory
		{
			get { return Path.Combine(Directory.GetCurrentDirectory(), "_TestFiles"); }
		}

		protected string TestArea
		{
			get { return Path.Combine(Directory.GetCurrentDirectory(), "_TestArea"); }
		}

		protected string TestCvsFile
		{
			get { return Path.Combine(TestFilesDirectory, "errorlog.csv"); }
		}

		protected string ExistingUrl
		{
			get { return "http://www.google.com"; }
		}

		protected string NonExistantUrl
		{
			get { return "http://www.bluttanblä.com"; }
		}

		protected ErrorLogRepository CreateRepository()
		{
			var fileSystemHelper = new FileSystemHelper();
			var log = new FakeLog();
			var settings = new FakeSettingsManager();
			settings.SetMaxNumberOfLogs(-1);

			var parser = new ErrorLogFileParser(log, new ClientInformationResolver());
			var datasource = new FileErrorLogSource(fileSystemHelper, parser, settings, log);

			var repository = new ErrorLogRepository(datasource);
			return repository;
		}
	}
}