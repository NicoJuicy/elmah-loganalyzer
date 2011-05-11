﻿using Crepido.ElmahOfflineViewer.Core.Domain;
using Crepido.ElmahOfflineViewer.Core.Infrastructure;
using Crepido.ElmahOfflineViewer.Core.Presentation;
using Crepido.ElmahOfflineViewer.UI.Views;
using Ninject.Activation;
using Ninject.Modules;
using NLog;

namespace Crepido.ElmahOfflineViewer.UI
{
	public class NinjectUiModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISearchView>().To<SearchView>().InSingletonScope();
			Bind<IReportView>().To<ReportView>().InSingletonScope();

			Bind<ISettingsView>().To<SettingsForm>();
			
			Bind<IErrorLogRepository>().To<ErrorLogRepository>().InSingletonScope();
			Bind<ISettingsManager>().To<SettingsManager>();
			Bind<IErrorLogSource>().To<FileErrorLogSource>();
			Bind<IErrorLogFileParser>().To<ErrorLogFileParser>();
			Bind<IReportGenerator>().To<ReportGenerator>();
			Bind<SearchPresenter>().To<SearchPresenter>();
			Bind<ReportPresenter>().To<ReportPresenter>();
			Bind<SettingsPresenter>().To<SettingsPresenter>();
			Bind<ICacheHelper>().To<CacheHelper>();
			Bind<IWebRequestHelper>().To<WebRequestHelper>();
			Bind<IClientInformationResolver>().To<ClientInformationResolver>();

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
