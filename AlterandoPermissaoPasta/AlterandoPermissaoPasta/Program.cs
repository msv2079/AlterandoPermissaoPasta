using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace AlterandoPermissaoPasta
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				WindowsIdentity user = WindowsIdentity.GetCurrent();
				WindowsPrincipal principal = new WindowsPrincipal(user);

				if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
				{
					throw new Exception("O sistema deve ser executado como administrador!");
				}

				DirectoryInfo dInfo = new DirectoryInfo(@"C:\inetpub\wwwroot\Teste");
				DirectorySecurity dSecurity = dInfo.GetAccessControl();
				dSecurity.AddAccessRule(new FileSystemAccessRule("IIS_IUSRS", FileSystemRights.FullControl, AccessControlType.Allow));
				dSecurity.AddAccessRule(new FileSystemAccessRule("TODOS", FileSystemRights.FullControl, AccessControlType.Allow));
				dInfo.SetAccessControl(dSecurity);

				Console.WriteLine("Permissões de pasta alterada com sucesso!");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}
	}
}
