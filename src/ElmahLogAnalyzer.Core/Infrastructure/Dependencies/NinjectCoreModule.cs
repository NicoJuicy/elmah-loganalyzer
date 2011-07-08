﻿using ElmahLogAnalyzer.Core.Domain;
using ElmahLogAnalyzer.Core.Domain.Export;
using ElmahLogAnalyzer.Core.Infrastructure.Cache;
using ElmahLogAnalyzer.Core.Infrastructure.FileSystem;
using ElmahLogAnalyzer.Core.Infrastructure.Logging;
using ElmahLogAnalyzer.Core.Infrastructure.Settings;
using ElmahLogAnalyzer.Core.Infrastructure.Web;
using ElmahLogAnalyzer.Core.Integrations.HttpUserAgentSearch;
using ElmahLogAnalyzer.Core.Presentation;
using Ninject.Activation;
using Ninject.Modules;
using NLog;

namespace ElmahLogAnalyzer.Core.Infrastructure.Dependencies
{
	public class NinjectCoreModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IErrorLogRepository>().To<ErrorLogRepository>().InSingletonScope();
			Bind<ISettingsManager>().To<SettingsManager>();
			Bind<IErrorLogSource>().To<FileErrorLogSource>();
			Bind<IErrorLogFileParser>().To<ErrorLogFileParser>();
			Bind<IReportGenerator>().To<ReportGenerator>();
			Bind<IErrorLogExporter>().To<ErrorLogExporter>();
			Bind<SearchPresenter>().To<SearchPresenter>();
			Bind<ReportPresenter>().To<ReportPresenter>();
			Bind<SettingsPresenter>().To<SettingsPresenter>();
			Bind<SelectServerPresenter>().To<SelectServerPresenter>();
			Bind<ExportPresenter>().To<ExportPresenter>();
			Bind<ICacheHelper>().To<CacheHelper>();
			Bind<IWebRequestHelper>().To<WebRequestHelper>();
			Bind<IClientInformationResolver>().To<ClientInformationResolver>();
			Bind<IUrlNavigationLauncher>().To<UrlNavigationLauncher>();
			Bind<IErrorLogDownloader>().To<ErrorLogDownloader>();
			Bind<IUrlPing>().To<UrlPing>();
			Bind<ICsvParser>().To<CsvParser>();
			Bind<IHttpUserAgentSearchLauncherFactory>().To<HttpUserAgentSearchLauncherFactory>();
			Bind<IFileSystemHelper>().To<FileSystemHelper>();
			Bind<ILog>().ToMethod(GetLogger);
		}

		private static ILog GetLogger(IContext context)
		{
			var loggerName = "Default";

			if (context.Request.Target != null)
			{
				loggerName = context.Request.Target.Member.DeclaringType.FullName;
			}

			var logger = LogManager.GetLogger(loggerName);

			return new Log(logger);
		}
	}
}