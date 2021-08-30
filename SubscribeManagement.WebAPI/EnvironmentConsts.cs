using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI
{
    public static class EnvironmentConsts
    {
        private static string _githubClientId;
        /// <summary>
        /// Github Id
        /// </summary>
        public static string GITHUB_CLIENT_ID
        {
            get
            {
                if (_githubClientId == null)
                {
                    _githubClientId = Environment.GetEnvironmentVariable(nameof(GITHUB_CLIENT_ID)) ?? throw new ArgumentNullException(nameof(GITHUB_CLIENT_ID), $"请传入GithubId");
                }
                return _githubClientId;
            }
        }
        private static string _githubClientSecret;

        /// <summary>
        /// Github 私钥
        /// </summary>
        public static string GITHUB_CLIENT_SECRET
        {
            get
            {
                if (_githubClientSecret == null)
                {
                    _githubClientSecret = Environment.GetEnvironmentVariable(nameof(GITHUB_CLIENT_SECRET)) ?? throw new ArgumentNullException(nameof(GITHUB_CLIENT_SECRET), $"请传入Github私钥");
                }
                return _githubClientSecret;
            }
        }
        private static HashSet<string> _adminUserNames;
        public static HashSet<string> ADMIN_USER_NAMES
        {
            get
            {
                if (_adminUserNames == null)
                {
                    var adminUserNamesText = Environment.GetEnvironmentVariable(nameof(ADMIN_USER_NAMES)) ?? throw new ArgumentNullException(nameof(ADMIN_USER_NAMES), $"请传入Github管理员用户名");
                    _adminUserNames = new HashSet<string>(adminUserNamesText.Replace(',', ';').Split(";"));
                }
                return _adminUserNames;
            }
        }
    }
}
