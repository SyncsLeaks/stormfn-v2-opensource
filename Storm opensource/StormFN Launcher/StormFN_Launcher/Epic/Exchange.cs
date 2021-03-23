using System;
using System.Text.Json.Serialization;

namespace StormFN_Launcher.Epic
{
	// Token: 0x0200000E RID: 14
	public class Exchange
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002ECF File Offset: 0x000010CF
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002ED7 File Offset: 0x000010D7
		[JsonPropertyName("expiresInSeconds")]
		public int ExpiresInSeconds { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002EE0 File Offset: 0x000010E0
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002EE8 File Offset: 0x000010E8
		[JsonPropertyName("code")]
		public string Code { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002EF1 File Offset: 0x000010F1
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002EF9 File Offset: 0x000010F9
		[JsonPropertyName("creatingClientId")]
		public string CreatingClientId { get; set; }
	}
}
