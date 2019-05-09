using System;

namespace newPanel
{
	// Token: 0x02000005 RID: 5
	internal class Settings
	{
		// Token: 0x0400001C RID: 28
		public static string app_folder_name = "Glimpse";

		// Token: 0x0400001D RID: 29
		public static string app_root_path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Settings.app_folder_name;

		// Token: 0x0400001E RID: 30
		public static string dns_upload_command_file_name_path = Settings.app_root_path + "\\uploadFileName";

		// Token: 0x0400001F RID: 31
		public static string dns_agent_path = Settings.app_root_path + "\\dns";

		// Token: 0x04000020 RID: 32
		public static int dnsFilesNameSize = 4;
	}
}
