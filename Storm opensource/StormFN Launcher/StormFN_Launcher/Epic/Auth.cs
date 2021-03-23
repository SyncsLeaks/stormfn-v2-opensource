using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using RestSharp;

namespace StormFN_Launcher.Epic
{
	// Token: 0x0200000F RID: 15
	internal class Auth
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002F04 File Offset: 0x00001104
		public static string GetDevicecodetoken()
		{
			RestClient restClient = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/token");
			RestRequest restRequest = new RestRequest(1);
			restRequest.AddHeader("Authorization", "Basic NTIyOWRjZDNhYzM4NDUyMDhiNDk2NjQ5MDkyZjI1MWI6ZTNiZDJkM2UtYmY4Yy00ODU3LTllN2QtZjNkOTQ3ZDIyMGM3");
			restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			restRequest.AddParameter("grant_type", "client_credentials");
			RestRequest restRequest2 = restRequest;
			string[] array = restClient.Execute(restRequest2).Content.Split(new char[]
			{
				':'
			}, 26);
			string result;
			try
			{
				result = array[1].ToString().Split(new char[]
				{
					','
				}, 2)[0].ToString().Split(new char[]
				{
					'"'
				}, 2)[1].ToString().Split(new char[]
				{
					'"'
				}, 2)[0].ToString();
			}
			catch
			{
				MessageBox.Show("Please make sure that you are connected to the internet.");
				Process.GetCurrentProcess().Kill();
				result = "error";
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003000 File Offset: 0x00001200
		public static string GetDevicecode(string auth)
		{
			RestClient restClient = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/deviceAuthorization");
			RestRequest restRequest = new RestRequest(1);
			restRequest.AddHeader("Authorization", "Bearer " + auth);
			restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			RestRequest restRequest2 = restRequest;
			string[] array = restClient.Execute(restRequest2).Content.Split(new char[]
			{
				','
			}, 8);
			string[] array2 = array[3].ToString().Split(new char[]
			{
				'"'
			}, 4)[3].ToString().Split(new char[]
			{
				'"'
			}, 2);
			string[] array3 = array[1].ToString().Split(new char[]
			{
				'"'
			}, 4)[3].ToString().Split(new char[]
			{
				'"'
			}, 2);
			Process.Start(array2[0]);
			string content;
			for (;;)
			{
				RestClient restClient2 = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/token");
				RestRequest restRequest3 = new RestRequest(1);
				restRequest3.AddHeader("Authorization", "Basic NTIyOWRjZDNhYzM4NDUyMDhiNDk2NjQ5MDkyZjI1MWI6ZTNiZDJkM2UtYmY4Yy00ODU3LTllN2QtZjNkOTQ3ZDIyMGM3");
				restRequest3.AddHeader("Content-Type", "application/x-www-form-urlencoded");
				restRequest3.AddParameter("grant_type", "device_code");
				restRequest3.AddParameter("device_code", array3[0].ToString());
				RestRequest restRequest4 = restRequest3;
				content = restClient2.Execute(restRequest4).Content;
				bool flag = !content.Contains("access_token");
				if (!flag)
				{
					break;
				}
				content.Contains("errors.com.epicgames.not_found");
				Thread.Sleep(150);
			}
			string[] array4 = content.Split(new char[]
			{
				':'
			}, 26);
			return array4[1].ToString().Split(new char[]
			{
				','
			}, 2)[0].ToString().Split(new char[]
			{
				'"'
			}, 2)[1].ToString().Split(new char[]
			{
				'"'
			}, 2)[0].ToString() + "," + array4[16].ToString().Split(new char[]
			{
				','
			}, 2)[0];
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003224 File Offset: 0x00001424
		public static string GetToken(string authCode)
		{
			Console.WriteLine("Token wird angefragt");
			RestClient restClient = new RestClient("https://account-public-service-prod.ol.epicgames.com/account/api/oauth/token");
			RestRequest restRequest = new RestRequest(1);
			restRequest.AddHeader("Authorization", "basic ZWM2ODRiOGM2ODdmNDc5ZmFkZWEzY2IyYWQ4M2Y1YzY6ZTFmMzFjMjExZjI4NDEzMTg2MjYyZDM3YTEzZmM4NGQ=");
			restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			restRequest.AddParameter("grant_type", "authorization_code");
			restRequest.AddParameter("code", authCode);
			RestRequest restRequest2 = restRequest;
			string content = restClient.Execute(restRequest2).Content;
			bool flag = content.Contains("access_token");
			string result;
			if (flag)
			{
				string[] array = content.Split(new char[]
				{
					':'
				}, 26);
				string str = array[17].ToString().Split(new char[]
				{
					','
				}, 2)[0];
				result = array[1].ToString().Split(new char[]
				{
					','
				}, 2)[0].ToString().Split(new char[]
				{
					'"'
				}, 2)[1].ToString().Split(new char[]
				{
					'"'
				}, 2)[0].ToString() + "," + str;
			}
			else
			{
				bool flag2 = content.Contains("It is possible that it was no longer valid");
				if (flag2)
				{
					MessageBox.Show("An Error occured, pls try again at a later point.");
					Process.Start("https://www.epicgames.com/id/logout?redirectUrl=https%3A//www.epicgames.com/id/login%3FredirectUrl%3Dhttps%253A%252F%252Fwww.epicgames.com%252Fid%252Fapi%252Fredirect%253FclientId%253D3446cd72694c4a4485d81b77adbb2141%2526responseType%253Dcode");
					result = "error";
				}
				else
				{
					MessageBox.Show(content);
					result = "error";
				}
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000338C File Offset: 0x0000158C
		public static string GetExchange(string token)
		{
			RestClient restClient = new RestClient("https://account-public-service-prod.ol.epicgames.com/account/api/oauth/exchange");
			RestRequest restRequest = new RestRequest(0);
			restRequest.AddHeader("Authorization", "bearer " + token);
			RestRequest restRequest2 = restRequest;
			string content = restClient.Execute(restRequest2).Content;
			Console.WriteLine(content);
			bool flag = content.Contains("errors.com.epicgames.common.oauth.invalid_token");
			string result;
			if (flag)
			{
				result = "error";
			}
			else
			{
				result = content.Split(new char[]
				{
					','
				}, 4)[1].ToString().Split(new char[]
				{
					','
				}, 2)[0].ToString().Split(new char[]
				{
					'"'
				}, 2)[1].ToString().Split(new char[]
				{
					'"'
				}, 2)[1].ToString().Split(new char[]
				{
					'"'
				}, 2)[1].ToString().Split(new char[]
				{
					'"'
				}, 2)[0].ToString();
			}
			return result;
		}
	}
}
