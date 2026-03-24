using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewHub.Common
{
    public static class ExceptionMessages
    {
        public const string RoleSeedingExceptionMessage = "There was an error while trying to seed the role {0}. Check the inner exception for details" ;
        public const string AdminUserNotFoundExceptionMessage = "User with email '{0}' was not found.";
        public const string AdminRoleAssignmentExceptionMessage = "Failed to assign Administrator role to user with email '{0}'.";
    }
}
