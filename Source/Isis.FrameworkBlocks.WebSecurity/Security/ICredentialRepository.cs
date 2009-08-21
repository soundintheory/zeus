﻿using System.Collections.Generic;

namespace Isis.Web.Security
{
	public interface ICredentialRepository
	{
		void CreateUser(string username, string password, string[] roles);
		IEnumerable<string> GetAllRoles();
		IEnumerable<IUser> GetAllUsers();
		IUser GetUser(string username);
	}
}