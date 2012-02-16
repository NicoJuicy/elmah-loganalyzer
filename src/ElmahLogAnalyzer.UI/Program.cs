﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElmahLogAnalyzer.Core.Common;
using ElmahLogAnalyzer.Core.Domain;
using ElmahLogAnalyzer.Core.Infrastructure.Dependencies;
using ElmahLogAnalyzer.Core.Infrastructure.Settings;
using ElmahLogAnalyzer.Core.Presentation;
using ElmahLogAnalyzer.UI.Forms;
using ElmahLogAnalyzer.UI.Views;

namespace ElmahLogAnalyzer.UI
{
	public static class Program
	{
		private static ISettingsManager _settingsManager;
		private static Container _container;
		
		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			_settingsManager = ServiceLocator.Resolve<ISettingsManager>();
			
			_container = ServiceLocator.Resolve<Container>();
			_container.SetWelcomeState();
			_container.DisplaySettings(_settingsManager);

			RegisterApplicationCommands();
			
			Application.Run(_container);
		}

		private static void RegisterApplicationCommands()
		{
			_container.OnApplicationCommand += (sender, args) =>
			{
			    switch (args.Command)
			    {
			        case ApplicationCommands.ConnectToDirectory:
			            ConnectToDirectory();
			            break;

			        case ApplicationCommands.ConnectToWebServer:
			            ConnectToWebServer();
			            break;

			        case ApplicationCommands.ConnectToDatabase:
			            ConnectToDatabase();
			            break;

			        case ApplicationCommands.Disconnect:
			            _container.SetWelcomeState();
			            break;

			        case ApplicationCommands.DisplaySearchView:
			            {
			                var presenter = ServiceLocator.Resolve<SearchPresenter>();
			                _container.DisplayView(presenter.View as UserControl);
			                break;
			            }

			        case ApplicationCommands.DisplayReportsView:
			            {
			                var presenter = ServiceLocator.Resolve<ReportPresenter>();
			                _container.DisplayView(presenter.View as UserControl);
			                break;
			            }

			        case ApplicationCommands.DislayExportDialog:
			            {
			                var presenter = ServiceLocator.Resolve<ExportPresenter>();
			                _container.DisplayDialog(presenter.View as Form);
			                break;
			            }

			        case ApplicationCommands.DisplaySettingsDialog:
			            DisplaySettings();
			            break;

			        case ApplicationCommands.DisplayAboutDialog:
			            {
			                var about = ServiceLocator.Resolve<AboutForm>();
			                _container.DisplayDialog(about);
			                break;
			            }

			        case ApplicationCommands.Exit:
			            Application.Exit();
			            break;
			    }
			};
		}

		private static void ConnectToDirectory()
		{
			var dialog = new FolderBrowserDialog
			{
				Description = "Select a folder with ELMAH log files",
				SelectedPath = _settingsManager.GetDefaultLogsDirectory()
			};

			var result = dialog.ShowDialog(_container);

			if (result == DialogResult.OK)
			{
				InitializeNewErrorLogSource(ErrorLogSourcesEnum.Files, dialog.SelectedPath, null);
			}
		}

		private static void ConnectToWebServer()
		{
			var presenter = ServiceLocator.Resolve<ConnectToWebServerPresenter>();
			var view = presenter.View as Form;
			var result = _container.DisplayDialog(view);

			if (result == DialogResult.OK)
			{
				InitializeNewErrorLogSource(ErrorLogSourcesEnum.Files, string.Empty, presenter.Connnection);
			}
		}
		
		private static void ConnectToDatabase()
		{
			var view = ServiceLocator.Resolve<ConnectToDatabaseViewForm>();
			var result = _container.DisplayDialog(view);

			if (result == DialogResult.OK)
			{
				var settings = (IConnectToDatabaseView)view;
				var connectionstring = ConnectionStringBuilder.Build(settings);

				InitializeNewErrorLogSource(settings.Source, connectionstring, null);
			}
		}

		private static void DisplaySettings()
		{
			var presenter = ServiceLocator.Resolve<SettingsPresenter>();
			var result = _container.DisplayDialog(presenter.View as Form);

			if (result == DialogResult.OK)
			{
				_container.DisplaySettings(_settingsManager);
			}
		}

		private static void InitializeNewErrorLogSource(ErrorLogSourcesEnum source, string connection, NetworkConnection networkConnection)
		{
			_container.SetLoadingState();

			DataSourceScopeController.SetNewSource(source, connection);
			
			//// need to know the download directory before resolving 
			//// and before downloading
			//// set the nw-connection in the constructor !!
			DownloadLogs(networkConnection);

			var downloadLogsTask = new Task(() => { return; });

			var repository = ServiceLocator.Resolve<IErrorLogRepository>();
			var viewPresenter = ServiceLocator.Resolve<SearchPresenter>();

			var initRepositoryTask = downloadLogsTask.ContinueWith(previousTask =>
			{
				if (previousTask.Exception != null)
				{
					_container.InvokeEx(m => m.DisplayView(new ErrorView(previousTask.Exception)));
					_container.InvokeEx(m => m.SetInitialState());
					return;
				}

				repository.Initialize();
			});

			var updateUiTask = initRepositoryTask.ContinueWith(previousTask =>
			{
				if (previousTask.Exception != null)
				{
					_container.InvokeEx(m => m.DisplayView(new ErrorView(previousTask.Exception)));
					_container.InvokeEx(m => m.SetInitialState());
					return;
				}

				_container.InvokeEx(m => m.SetReadyForWorkState());
				_container.InvokeEx(m => m.DisplayStatus("Connection: " + DataSourceScopeController.Connection));
				_container.InvokeEx(m => m.DisplayView(viewPresenter.View as UserControl));
			});
			
			downloadLogsTask.Start();
		}
		
		private static void DownloadLogs(NetworkConnection networkConnection)
		{
			if (networkConnection == null)
			{
				return;
			}
			
			var downloader = ServiceLocator.Resolve<ErrorLogDownloader>();
			downloader.Download(networkConnection);

			DataSourceScopeController.SetNewSource(ErrorLogSourcesEnum.Files, downloader.DownloadDirectory);
		}
	}
}
