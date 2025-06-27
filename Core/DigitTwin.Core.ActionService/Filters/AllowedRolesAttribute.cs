using System;
using DigitTwin.Lib.Abstractions; // Для UserTypeEnum
using System.Linq;

namespace DigitTwin.Core.ActionService
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AllowedRolesAttribute : Attribute
    {
        public string[] Roles { get; }
        public AllowedRolesAttribute(params UserTypeEnum[] roles)
        {
            Roles = roles.Select(r => r.ToString()).ToArray();
        }
    }
} 