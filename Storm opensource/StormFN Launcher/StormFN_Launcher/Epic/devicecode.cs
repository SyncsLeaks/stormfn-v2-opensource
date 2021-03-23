using System;
using System.Text.Json.Serialization;

namespace StormFN_Launcher.Epic
{
	// Token: 0x0200000D RID: 13
	public class devicecode
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002E47 File Offset: 0x00001047
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002E4F File Offset: 0x0000104F
		[JsonPropertyName("user_code")]
		public int user_code { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002E58 File Offset: 0x00001058
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002E60 File Offset: 0x00001060
		[JsonPropertyName("device_code")]
		public string device_code { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002E69 File Offset: 0x00001069
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002E71 File Offset: 0x00001071
		[JsonPropertyName("verification_uri")]
		public string verification_uri { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002E7A File Offset: 0x0000107A
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002E82 File Offset: 0x00001082
		[JsonPropertyName("verification_uri_complete")]
		public string verification_uri_complete { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002E8B File Offset: 0x0000108B
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002E93 File Offset: 0x00001093
		[JsonPropertyName("prompt")]
		public string prompt { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002E9C File Offset: 0x0000109C
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002EA4 File Offset: 0x000010A4
		[JsonPropertyName("expires_in")]
		public string expires_in { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002EAD File Offset: 0x000010AD
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002EB5 File Offset: 0x000010B5
		[JsonPropertyName("interval")]
		public string interval { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002EBE File Offset: 0x000010BE
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002EC6 File Offset: 0x000010C6
		[JsonPropertyName("client_id")]
		public string client_id { get; set; }
	}
}
