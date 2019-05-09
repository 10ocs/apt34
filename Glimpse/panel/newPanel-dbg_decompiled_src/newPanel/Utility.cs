using System;
using System.IO;
using System.Threading;

namespace newPanel
{
	// Token: 0x02000006 RID: 6
	internal class Utility
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003064 File Offset: 0x00001264
		public static void logc(string str)
		{
			Console.WriteLine(str);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000306E File Offset: 0x0000126E
		public static void logf()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003074 File Offset: 0x00001274
		public static void append_file(string text, string resultFileAddress)
		{
			Utility.create_path(resultFileAddress.Remove(resultFileAddress.LastIndexOf("\\")));
			for (;;)
			{
				try
				{
					object obj = Utility.lockThis;
					lock (obj)
					{
						using (StreamWriter streamWriter = File.AppendText(resultFileAddress))
						{
							streamWriter.WriteLine(text);
							break;
						}
					}
				}
				catch (Exception ex)
				{
					Utility.log_errors("getting class name and function name >> (file writing race) " + ex.Message);
				}
				Thread.Sleep(100);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003128 File Offset: 0x00001328
		public static void write_file(string text, string resultFileAddress)
		{
			Utility.create_path(resultFileAddress.Remove(resultFileAddress.LastIndexOf("\\")));
			for (;;)
			{
				try
				{
					File.WriteAllText(resultFileAddress, text);
					break;
				}
				catch (Exception ex)
				{
					Utility.log_errors("getting class name and function name >> (file writing race) " + ex.Message);
				}
				Thread.Sleep(100);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003194 File Offset: 0x00001394
		public static bool copy_entire_path(string source_pacth, string distination_path)
		{
			bool result = false;
			bool flag = Directory.Exists(source_pacth);
			if (flag)
			{
				try
				{
					foreach (string text in Directory.GetDirectories(source_pacth, "*", SearchOption.AllDirectories))
					{
						Directory.CreateDirectory(text.Replace(source_pacth, distination_path));
					}
					foreach (string text2 in Directory.GetFiles(source_pacth, "*.*", SearchOption.AllDirectories))
					{
						File.Copy(text2, text2.Replace(source_pacth, distination_path), true);
					}
					result = true;
				}
				catch
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003240 File Offset: 0x00001440
		public static void delete_entire_path(string path)
		{
			foreach (string path2 in Directory.GetFiles(path))
			{
				File.Delete(path2);
			}
			foreach (string path3 in Directory.GetDirectories(path))
			{
				Utility.delete_entire_path(path3);
			}
			Directory.Delete(path);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000032A4 File Offset: 0x000014A4
		public static void move_folder(string src, string dis)
		{
			try
			{
				bool flag = Directory.Exists(dis);
				if (flag)
				{
					Directory.Move(src, dis);
				}
				else
				{
					Utility.create_path(dis);
					Directory.Move(src, dis);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003300 File Offset: 0x00001500
		public static void create_path(string path)
		{
			try
			{
				bool flag = !Directory.Exists(path);
				if (flag)
				{
					Directory.CreateDirectory(path);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000334C File Offset: 0x0000154C
		public static int command_counter(string address)
		{
			bool flag = File.Exists(address);
			int result;
			if (flag)
			{
				int num = int.Parse(Utility.file_reader(address));
				bool flag2 = num > 98;
				if (flag2)
				{
					num = 10;
				}
				Utility.write_file(string.Concat(num + 1), address);
				result = num;
			}
			else
			{
				Utility.write_file("10", address);
				result = 10;
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000033A8 File Offset: 0x000015A8
		public static int command_counter_without_random(string address)
		{
			bool flag = File.Exists(address);
			int result;
			if (flag)
			{
				int num = int.Parse(Utility.file_reader(address));
				bool flag2 = num % 100 > 25;
				if (flag2)
				{
					num += 84;
				}
				bool flag3 = num > 9999;
				if (flag3)
				{
					num = 1010;
				}
				Utility.write_file(string.Concat(num + 1), address);
				result = num;
			}
			else
			{
				Utility.write_file("1012", address);
				result = 1011;
			}
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003424 File Offset: 0x00001624
		public static string file_reader(string address)
		{
			bool flag = File.Exists(address);
			string result;
			if (flag)
			{
				string text = "-1";
				bool flag2 = !Utility.IsFileLocked(new FileInfo(address));
				if (flag2)
				{
					text = File.ReadAllText(address);
				}
				result = text;
			}
			else
			{
				result = "-1";
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000346C File Offset: 0x0000166C
		protected static bool IsFileLocked(FileInfo file)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
			}
			catch (IOException)
			{
				return true;
			}
			finally
			{
				bool flag = fileStream != null;
				if (flag)
				{
					fileStream.Close();
				}
			}
			return false;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000034C8 File Offset: 0x000016C8
		public static void log_errors(string str)
		{
			try
			{
				using (StreamWriter streamWriter = File.AppendText("errors.txt"))
				{
					streamWriter.WriteLine(str + "\n");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception!!! > " + ex.Message);
			}
		}

		// Token: 0x04000021 RID: 33
		private static object lockThis = new object();
	}
}
