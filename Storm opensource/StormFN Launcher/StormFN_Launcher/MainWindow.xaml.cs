using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using StormFN_Launcher.Epic;
using StormFN_Launcher.Utilities;

namespace StormFN_Launcher
{
	// Token: 0x02000004 RID: 4
	public partial class MainWindow : Window
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021DC File Offset: 0x000003DC
		public static string foldername
		{
			get
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uriBuilder = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uriBuilder.Path);
				return Path.GetDirectoryName(path) + "\\";
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000221C File Offset: 0x0000041C
		private void Give_AntiCheat_Love()
		{
			using (PowerShell powerShell = PowerShell.Create())
			{
				string str = "system*";
				powerShell.AddScript("Set-Service 'BEService' -StartupType Disabled" + str);
				powerShell.AddScript("Set-Service 'EasyAntiCheat' -StartupType Disabled" + str);
				Config_file.Default.AC_bypass = true;
				Config_file.Default.Save();
				Collection<PSObject> collection = powerShell.Invoke();
				foreach (PSObject psobject in collection)
				{
					bool flag = psobject != null;
					if (flag)
					{
						this.msg("An Error occured \n" + psobject.Properties["Status"].Value.ToString() + " - ");
					}
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000230C File Offset: 0x0000050C
		private void Save_settings(object sender, EventArgs e)
		{
			Config_file.Default.Path = this.FN_Path.Text;
			Config_file.Default.Save();
			Application.Current.Shutdown();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000233C File Offset: 0x0000053C
		private void MainWindow_Load(object sender, EventArgs e)
		{
			foreach (Process process in Process.GetProcessesByName("EZFNLauncher"))
			{
				process.Kill();
			}
			this.DeleteCms();
			bool flag = this.FN_Path.Text == "Fortnite Path" || this.FN_Path.Text == "" || string.IsNullOrEmpty(this.FN_Path.Text);
			if (flag)
			{
				TextBox fn_Path = this.FN_Path;
				MainWindow.Installation installation = MainWindow.GetEpicInstallLocations().FirstOrDefault((MainWindow.Installation i) => i.AppName == "Fortnite");
				fn_Path.Text = ((installation != null) ? installation.InstallLocation : null);
			}
			else
			{
				this.FN_Path.Text = Config_file.Default.Path;
			}
			bool flag2 = !Config_file.Default.AC_bypass;
			if (flag2)
			{
				this.Give_AntiCheat_Love();
			}
			bool show = Config_file.Default.Show;
			if (show)
			{
				Config_file.Default.Show = false;
				Config_file.Default.Save();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002460 File Offset: 0x00000660
		public void ShowLi(bool yesorno)
		{
			bool flag = !yesorno;
			if (flag)
			{
				this.Logged_in_as.Visibility = Visibility.Hidden;
				this.DisplayName.Visibility = Visibility.Hidden;
				this.LoginButton.Content = "Login";
			}
			else
			{
				this.Logged_in_as.Visibility = Visibility.Visible;
				this.DisplayName.Visibility = Visibility.Visible;
				this.LoginButton.Content = "Launch";
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024D2 File Offset: 0x000006D2
		public void msg(string text)
		{
			MessageBox.Show(text.ToString(), "Storm Launcher");
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024E8 File Offset: 0x000006E8
		private void Login_click(object sender, RoutedEventArgs e)
		{
			bool flag = this.LoginButton.Content.ToString() == "Login";
			if (flag)
			{
				Config_file.Default.Path = this.FN_Path.Text;
				Config_file.Default.Save();
				string devicecode = Auth.GetDevicecode(Auth.GetDevicecodetoken());
				string[] array = devicecode.Split(new char[]
				{
					','
				}, 2);
				bool flag2 = devicecode.Contains("error");
				if (!flag2)
				{
					this.username = array[1];
					this.DisplayName.Content = (string)(array[1] ?? "");
					this.ShowLi(true);
					this.token = array[0];
					this.LoginButton.Content = "Launch";
				}
			}
			else
			{
				bool flag3 = this.LoginButton.Content.ToString() == "Launch";
				if (flag3)
				{
					Config_file.Default.Path = this.FN_Path.Text;
					Config_file.Default.Save();
					this.exchange = Auth.GetExchange(this.token);
					string text = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
					string text2 = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
					string text3 = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");
					bool flag4 = !File.Exists(text);
					if (flag4)
					{
						this.msg("\"" + text + "\" wasn't found, stop deleting game files!");
						this.ShowLi(false);
					}
					else
					{
						bool flag5 = !File.Exists(text2);
						if (flag5)
						{
							this.msg("\"" + text2 + "\" wasn't found, stop deleting game files!");
							this.ShowLi(false);
						}
						else
						{
							bool flag6 = !File.Exists(text3);
							if (flag6)
							{
								this.msg("\"" + text3 + "\" wasn't found, stop deleting game files!");
								this.ShowLi(false);
							}
							else
							{
								Config_file.Default.Path = this.FN_Path.Text;
								Config_file.Default.Save();
								this.exchange = Auth.GetExchange(this.token);
								string arguments = "-AUTH_LOGIN=unused -AUTH_PASSWORD=" + this.exchange + " -AUTH_TYPE=exchangecode -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=5dh74c635862g575778132fb -skippatchcheck";
								Process process = new Process
								{
									StartInfo = new ProcessStartInfo(text, arguments)
									{
										UseShellExecute = false,
										RedirectStandardOutput = false,
										CreateNoWindow = true
									}
								};
								Process process2 = new Process();
								process2.StartInfo.FileName = text3;
								process2.Start();
								foreach (object obj in process2.Threads)
								{
									ProcessThread processThread = (ProcessThread)obj;
									Win32.SuspendThread(Win32.OpenThread(2, false, processThread.Id));
								}
								Process process3 = new Process();
								process3.StartInfo.FileName = text2;
								process3.StartInfo.Arguments = "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=5dh74c635862g575778132fb -skippatchcheck";
								process3.Start();
								foreach (object obj2 in process3.Threads)
								{
									ProcessThread processThread2 = (ProcessThread)obj2;
									Win32.SuspendThread(Win32.OpenThread(2, false, processThread2.Id));
								}
								process.Start();
								Thread.Sleep(2000);
								base.Hide();
								Thread.Sleep(6000);
								try
								{
									File.Delete(MainWindow.tempPath + "/Injector.exe");
								}
								catch
								{
								}
								try
								{
									this.webClient.DownloadFile("https://github.com/Elproxxjunoxx/test/raw/main/Injector.exe", MainWindow.tempPath + "/Injector.exe");
								}
								catch
								{
									MessageBox.Show("Failed to download injector, please report this to an support member, try disabling antivirus before doing this.");
									this.ShowLi(false);
									return;
								}
								process.WaitForInputIdle();
								new Process
								{
									StartInfo = 
									{
										Arguments = string.Format("\"{0}\" \"{1}\"", process.Id, MainWindow.plataniumdllpath),
										CreateNoWindow = true,
										UseShellExecute = false,
										FileName = MainWindow.tempPath + "/Injector.exe"
									}
								}.Start();
								process.WaitForExit();
								try
								{
									process2.Close();
									process3.Close();
								}
								catch
								{
								}
								base.Show();
								this.ShowLi(false);
								this.LoginButton.Content = "Login";
							}
						}
					}
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000029E0 File Offset: 0x00000BE0
		public static List<MainWindow.Installation> GetEpicInstallLocations()
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Epic\\UnrealEngineLauncher\\LauncherInstalled.dat");
			bool flag = !Directory.Exists(Path.GetDirectoryName(path)) || !File.Exists(path);
			List<MainWindow.Installation> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = JsonConvert.DeserializeObject<MainWindow.EpicInstallLocations>(File.ReadAllText(path)).InstallationList;
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A38 File Offset: 0x00000C38
		private void Select_fn_path_button_Click(object sender, EventArgs e)
		{
			string text = this.FN_Path.Text;
			CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			};
			bool flag = commonOpenFileDialog.ShowDialog() == 1;
			if (flag)
			{
				this.FN_Path.Text = commonOpenFileDialog.FileName;
				Config_file.Default.Path = this.FN_Path.Text;
				Config_file.Default.Save();
			}
			else
			{
				this.FN_Path.Text = text;
				Config_file.Default.Path = this.FN_Path.Text;
				Config_file.Default.Save();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private void DeleteCms()
		{
			try
			{
				string path = Environment.GetEnvironmentVariable("LocalAppData") + "\\FortniteGame\\Saved\\PersistentDownloadDir";
				string path2 = Environment.GetEnvironmentVariable("LocalAppData") + "\\FortniteGame\\Saved\\webcache";
				Directory.Delete(path, true);
				Directory.Delete(path2, true);
			}
			catch
			{
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B38 File Offset: 0x00000D38
		private void Console_Checked(object sender, RoutedEventArgs e)
		{
			MainWindow.disableConsole = false;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B41 File Offset: 0x00000D41
		private void Console_UnChecked(object sender, RoutedEventArgs e)
		{
			MainWindow.disableConsole = true;
		}

		// Token: 0x04000003 RID: 3
		public static bool disableConsole = false;

		// Token: 0x04000004 RID: 4
		private string token;

		// Token: 0x04000005 RID: 5
		private string exchange;

		// Token: 0x04000006 RID: 6
		private string username;

		// Token: 0x04000007 RID: 7
		public static string tempPath = Path.GetTempPath();

		// Token: 0x04000008 RID: 8
		private readonly WebClient webClient = new WebClient();

		// Token: 0x04000009 RID: 9
		public static string plataniumdllpath = MainWindow.foldername + "Platanium.dll";

		// Token: 0x02000005 RID: 5
		public class EpicInstallLocations
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000022 RID: 34 RVA: 0x00002CB9 File Offset: 0x00000EB9
			// (set) Token: 0x06000023 RID: 35 RVA: 0x00002CC1 File Offset: 0x00000EC1
			[JsonProperty("InstallationList")]
			public List<MainWindow.Installation> InstallationList { get; set; }
		}

		// Token: 0x02000006 RID: 6
		public class Installation
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000025 RID: 37 RVA: 0x00002CD3 File Offset: 0x00000ED3
			// (set) Token: 0x06000026 RID: 38 RVA: 0x00002CDB File Offset: 0x00000EDB
			[JsonProperty("InstallLocation")]
			public string InstallLocation { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000027 RID: 39 RVA: 0x00002CE4 File Offset: 0x00000EE4
			// (set) Token: 0x06000028 RID: 40 RVA: 0x00002CEC File Offset: 0x00000EEC
			[JsonProperty("AppName")]
			public string AppName { get; set; }
		}
	}
}
